using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Contracts;
using Volo.Abp.Account;
using Volo.Abp.Identity;
using IdentityUser = Volo.Abp.Identity.IdentityUser;
using IdentityRole = Volo.Abp.Identity.IdentityRole;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories;
using TodoApp.Domain;
using Task = TodoApp.Domain.Task;
using Volo.Abp.Application.Services;

namespace TodoApp.Application
{
    public class AccountAppService:ApplicationService, IAccountAppService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountAppService> _logger;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly IRepository<DailyPlan, Guid> _dailyPlanRepository;
        private readonly IRepository<Task,Guid> _taskRepository;
        private readonly IRepository<PlanLayer,Guid> _layerRepository;

        
        public AccountAppService(UserManager<IdentityUser>
            userManager, 
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration,
            ILogger<AccountAppService> logger,
            IHttpContextAccessor contextAccessor,
           RoleManager<IdentityRole> roleManager, 
           IServiceProvider serviceProvider,
           IRepository<DailyPlan,Guid> dailyPlanRepository,
           IRepository<Task,Guid> taskRepository,
           IRepository<PlanLayer, Guid> layerRepository
           )
        {
            _serviceProvider=serviceProvider;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _logger = logger;
           _contextAccessor = contextAccessor;
          _roleManager = roleManager;
            _taskRepository = taskRepository;
            _layerRepository = layerRepository;
            _dailyPlanRepository = dailyPlanRepository;
        }

        public async Task<string> LoginAsync(LoginDto input)
        {
            var context = _contextAccessor.HttpContext;
            _logger.LogInformation("Log in User:{Username}",
                input.Username);
            var user= await _userManager.FindByNameAsync
                (input.Username);
            if (user == null)
            {
                _logger.LogWarning("User not Found {Username}"
                    , input.Username);
                throw new UnauthorizedAccessException
                    ("Invalid Login Moj");
            }
            var result = await _signInManager.PasswordSignInAsync
                (input.Username, input.Password, false, false);
            if (result.Succeeded) { 
            _logger.LogInformation("login successful Mojsuccess",
                input.Username);
                var token=GenerateMyJWt(user);
                var options = new CookieOptions
                {
                    Domain = "localhost",
                    Expires = DateTime.Now.AddDays(-1),
                    Secure = true,
                    HttpOnly = true
                };
                
              context.Response.Cookies.Append(".AspNetCore.Identity.Application", "", options);
                return token;
                
            }
            _logger.LogWarning("Invalid Login",input.Username);
            throw new UnauthorizedAccessException("Invalid Login");
        }

        private string GenerateMyJWt(IdentityUser user) {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,
                user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.
                GetBytes(_configuration["Jwt:Key"]));
            var creds=new SigningCredentials(key,SecurityAlgorithms
                .HmacSha256);
            var token=new JwtSecurityToken(
                
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims:claims,
                expires:DateTime.Now.AddMinutes(1),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<bool> CreateRoleAsync
            (string roleName)
        {
            var roleExists = await _roleManager.
                RoleExistsAsync(roleName);
            if (!roleExists)
            {
                var newRole = new IdentityRole(Guid.NewGuid(), roleName, null);
                var roleResult = await _roleManager.CreateAsync
                    (newRole);

                return roleResult.Succeeded;
            }
            return false;
        }

        public async Task<bool> AssignRoleAsync
            (string username, string roleName)
        {
            var user = await _userManager.
                FindByNameAsync(username);
            var t = 0;
            if (user != null)
            {
                var result = await _userManager.AddToRoleAsync
                        (user, roleName);
                return result.Succeeded;
            }
            return false;
        }

        public async Task<List<string>> GetAllUsernamesAsync()
        {
            try
            {
                // Use a scope to resolve DbContext directly from the service provider
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<TodoAppDbContext>();

                // Query the users directly from the DbContext
                var users = await dbContext.Users.ToListAsync();
                return users.Select(x => x.UserName).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching usernames.");
                return new List<string>();
            }
        }



        public async Task<List<String>> GetAllRolesAsync()
        {
            try {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<TodoAppDbContext>();
                var roles=await dbContext.Roles.ToListAsync();
                return roles.Select(x=>x.Name).ToList();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An Error Occurred while fetching roles.");
            return new List<string>();
            }
        }

        public async Task<DailyPlanDto> CreateDailyPlanAsync(CreateDailyPlanDto input)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<TodoAppDbContext>();

                // 1. Detach all tracked entities in the DbContext to avoid any conflicts
                foreach (var entry in dbContext.ChangeTracker.Entries().ToList())
                {
                    dbContext.Entry(entry.Entity).State = EntityState.Detached;
                }

                // 2. Create the DailyPlan and ensure GUIDs are correctly generated
                var dailyPlan = new DailyPlan
                {
                    Id = Guid.NewGuid(),
                    Title = input.Title,
                    Tasks = input.Tasks.Select(t => new Task
                    {
                        Id = t.Id == Guid.Empty ? Guid.NewGuid() : t.Id,
                        Name = t.Name,
                        Duration = t.Duration
                    }).ToList(),

                    Layers = input.Layers.Select(l => new PlanLayer
                    {
                        // Ensure a valid GUID is assigned. If it's zero, we generate a new one.
                        Id = l.Id == Guid.Empty ? Guid.NewGuid() : l.Id,
                        Name = l.Name,
                        Duration = l.Duration
                    }).ToList()
                };

                // 3. Add the DailyPlan to the DbContext
                await dbContext.DailyPlans.AddAsync(dailyPlan);
                await dbContext.SaveChangesAsync();

                // 4. Prepare the DTO to return
                var dailyPlanDto = new DailyPlanDto
                {
                    Id = dailyPlan.Id,
                    Title = dailyPlan.Title,
                    Tasks = dailyPlan.Tasks.Select(t => new TaskDto
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Duration = t.Duration
                    }).ToList(),
                    Layers = dailyPlan.Layers.Select(l => new PlanLayerdto
                    {
                        Id = l.Id,
                        Name = l.Name,
                        Duration = l.Duration
                    }).ToList()
                };

                return dailyPlanDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a daily plan.");
                throw;
            }
        }

    }


}

