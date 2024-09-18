using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Controllers
{
    [Route("api/dailyplan")]
    [ApiController]
    public class DailyPlanController:ControllerBase
    {
        private readonly IAccountAppService _accountAppService;
        public DailyPlanController(IAccountAppService accountAppService)
        {
            _accountAppService = accountAppService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateDailyplanAsync
            ([FromBody] CreateDailyPlanDto input)
        {
            var result = await _accountAppService
                .CreateDailyPlanAsync(input);
            return Ok(result);
        }
        
    }
}
