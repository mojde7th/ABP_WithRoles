using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Controllers
{
    [Route("api/dailyplan")]
    [ApiController]
    public class DailyPlanController : ControllerBase
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

        [HttpGet]
        public async Task<IActionResult> GetDailyPlansAsync()
        {
            var result = await _accountAppService.GetAllDailyPlansAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDailyPlanByIdAsync(Guid id)
        {
            var result = await _accountAppService.GetDailyPlanByIdAsync(id);
            return Ok(result);
        }

        //Add Section to Daily plan
        [HttpPost("{dailyPlanId}/sections")]
        public async Task<IActionResult> AddSectionToDailyPlanAsync
            (Guid dailyPlanId, [FromBody] CreateSectionDto input)
        {
            var result= await _accountAppService.AddSectionToDailyPlanAsync(dailyPlanId, input);
       return Ok(result);
        }

        //Add Task To Section
        [HttpPost("{sectionId}/tasks")]
        public async Task<IActionResult> AddTaskToSectionAsync
            (Guid sectionId, [FromBody] CreateTaskDto input)
        {
            var result=await _accountAppService.AddTaskToSectionAsync(sectionId, input);
            return Ok(result);
        }
        [HttpPost("{sectionId}/layers")]
        public async Task<IActionResult> AddLayerToSectionAsync
            (Guid sectionId, [FromBody] CreateLayerDto input)
        {
            var result=await _accountAppService.AddLayerToSectionAsync(sectionId, input);   
            return Ok(result);
        }
    }
}
