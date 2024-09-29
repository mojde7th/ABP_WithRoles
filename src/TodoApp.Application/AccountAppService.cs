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
using TodoApp.Helpers;
using System.Runtime.Intrinsics.Arm;
using Volo.Abp.Domain.Entities;
using AutoMapper;

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
        private readonly IRepository<Section,Guid> _sectionRepository;
        private readonly TodoAppDbContext _todoAppDbContext;
        private readonly IServiceProvider serviceProvider1;
        private readonly IMapper _mapper;


        public AccountAppService(UserManager<IdentityUser>
            userManager, TodoAppDbContext tedoAppDbContext,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration,
            ILogger<AccountAppService> logger,
            IHttpContextAccessor contextAccessor,
           RoleManager<IdentityRole> roleManager, 
           IServiceProvider serviceProvider,
           IRepository<DailyPlan,Guid> dailyPlanRepository,
           IRepository<Task,Guid> taskRepository,
           IRepository<PlanLayer, Guid> layerRepository,
           IRepository<Section,Guid> sectionRepository,
           IMapper mapper
           )
        {
            _sectionRepository = sectionRepository;
            _todoAppDbContext= tedoAppDbContext;
            _serviceProvider =serviceProvider;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _logger = logger;
           _contextAccessor = contextAccessor;
          _roleManager = roleManager;
            _taskRepository = taskRepository;
            _layerRepository = layerRepository;
            _dailyPlanRepository = dailyPlanRepository;
            _mapper = mapper;
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
        public async Task<bool> CreateRoleAsync (string roleName)
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
        public async Task<bool> AssignRoleAsync (string username, string roleName)
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


      public async Task<DailyPlanDto> CreateDailyPlanAsync
            (CreateDailyPlanDto input)
        {
           var dailyPlan=_mapper.Map<DailyPlan>(input);

            await _dailyPlanRepository.InsertAsync(dailyPlan);
            await CurrentUnitOfWork.SaveChangesAsync();
            return _mapper.Map<DailyPlanDto>(dailyPlan);
        }


        public async Task<DailyPlanDto> GetDailyPlanByIdAsync
            (Guid id)
        {
            var dailyPlan=await _dailyPlanRepository
                .WithDetails(dp=>dp.Sections)
                .Include(dp=>dp.Sections)
                .ThenInclude(s=>s.Tasks)
                .Include(dp=>dp.Sections)
                .ThenInclude(s=>s.PlanLayers)
                .FirstOrDefaultAsync(dp=>dp.Id==id);
             var dailyPlanDto= _mapper.Map<DailyPlanDto>(dailyPlan);
       
                foreach(var section in dailyPlanDto.Sections)
            {
                section.TotalTaskDuration = section.Tasks.Sum
                    (t => t.Duration);
                section.TotalLayerDuration = section.PlanLayers.Sum
                    (l => l.Duration);
            }
                return dailyPlanDto;
        }



        //Get All Daily Plans
        public async Task<List<DailyPlanDto>> GetAllDailyPlansAsync()
        {
            var dailyPlans=await _dailyPlanRepository
                .WithDetails(dp=>dp.Sections)
                .Include(dp=>dp.Sections)
                .ThenInclude(s=>s.Tasks)
                .Include (dp=>dp.Sections)
                .ThenInclude (s=>s.PlanLayers)
                .ToListAsync();
            var dailyPlanDtos= _mapper.Map<List<DailyPlanDto>>(dailyPlans);
       foreach(var dailyPlan in dailyPlanDtos)
            {
                foreach(var section in dailyPlan.Sections)
                {
                    section.TotalTaskDuration=section.Tasks.Sum
                        (t=>t.Duration);
                    section.TotalLayerDuration=section.PlanLayers
                        .Sum(l=>l.Duration);
                }
            }
            return dailyPlanDtos;
        }


        //Add Section to Daily Plan
        public async Task<SectionDto> AddSectionToDailyPlanAsync
            (Guid dailyPlanId,CreateSectionDto input)
        {
            var section = new Section
            {
                Name = input.Name,
                DailyPlanId = dailyPlanId
            };
            await _sectionRepository.InsertAsync(section);
            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<Section,SectionDto>(section);

        }


        //Add Task To Section
        public async Task<TaskDto> AddTaskToSectionAsync
            (Guid sectionId,CreateTaskDto input)

        {
            var task = new Task
            {
                SectionId = sectionId,
                Name = input.Name,
                Duration = input.Duration
            };
            await _taskRepository.InsertAsync(task);
            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<Task,TaskDto>(task);
        }
        //Add Layer to Section
        public async Task<PlanLayerdto> AddLayerToSectionAsync
            (Guid sectionId, CreateLayerDto input)
        {
            var layer = new PlanLayer
            {
                SectionId = sectionId,
                Name = input.Name,
                Duration = input.Duration
            };
            await _layerRepository.InsertAsync(layer);
            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<PlanLayer,PlanLayerdto>(layer);
        }





    }


}

