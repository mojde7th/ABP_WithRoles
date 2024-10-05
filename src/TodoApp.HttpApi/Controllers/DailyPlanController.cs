using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain;
using Volo.Abp.Domain.Repositories;

namespace TodoApp.Controllers
{
    [Route("api/dailyplan")]
    [ApiController]
    public class DailyPlanController : ControllerBase
    {
        
        private readonly IAccountAppService _accountAppService;
        private readonly IRepository<Section, Guid> _sectionRepository;
        private readonly IMapper _mapper;
        
        public DailyPlanController(IAccountAppService accountAppService,
            IRepository<Section, Guid> sectionRepository, IMapper
            mapper
            )
        {
            _accountAppService = accountAppService;
            _sectionRepository = sectionRepository;
            _mapper = mapper;
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
        [HttpGet("{id}/sections/{sectionId}")]
        public async Task<IActionResult>
            GetSectionByIdAsync(Guid id, Guid sectionId)
        {
            var section = await _sectionRepository
                .WithDetails(s => s.Tasks, s => s.PlanLayers)
                .FirstOrDefaultAsync(s => s.Id == sectionId &&
                s.DailyPlanId == id);
            return Ok(_mapper.Map<SectionDto>(section));
        }

        [HttpPost("create-with-sections")]
        public async Task<IActionResult> CreateDailyPlanWithSectionsAsync
            ([FromBody]  createDailyPlanWithSectionsDto input)
        {
            var result= await _accountAppService.CreateDailyPlanWithSectionAsync
                (input);
            return Ok(result);
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllDailyPlans2()
        {
            var dailyPlans=await _accountAppService.GetAllDailyPlansAsync();
            return Ok(dailyPlans);
        }
        [HttpPost("createFull")]
        public async Task<IActionResult> CreateFullDailyPlanAsync
            ([FromBody] CreateFullDailyplanDto input)
        {
            var result=await _accountAppService.CreateFullDailyPlanAsync(input);
            return Ok(result);
        }
    }
}
