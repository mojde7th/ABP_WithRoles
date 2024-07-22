using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Contracts;

namespace TodoApp
{
    [Route("api/customaccount")]
    public class AccountController: ControllerBase
    {
        private readonly IAccountAppService 
            _accountAppService;
        public AccountController(IAccountAppService accountAppService)
        {
            _accountAppService = accountAppService;
        }
        [HttpGet("Login")]
        public IActionResult Login()
        {
            // Redirect to the React application's login page
            return Redirect("http://localhost:3000/login");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto input)
        {
            var token = await _accountAppService.LoginAsync(input);
            return Ok(new { token });
        }
    }
}
