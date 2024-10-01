using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Contracts;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using TodoApp.Application;
using TodoApp.Helpers;
namespace TodoApp
{
    public interface IAccountAppService:IApplicationService
    {
        Task<string> LoginAsync(LoginDto input);
        Task<bool> CreateRoleAsync(string roleName);
        Task<bool> AssignRoleAsync(string username, string roleName);
        Task<List<string>> GetAllUsernamesAsync();
        Task<List<string>> GetAllRolesAsync();

        
        Task<DailyPlanDto> CreateDailyPlanAsync (CreateDailyPlanDto input);
        Task<List<DailyPlanDto>> GetAllDailyPlansAsync();
        Task<DailyPlanDto> GetDailyPlanByIdAsync(Guid id);
        Task<SectionDto> AddSectionToDailyPlanAsync(Guid dailyPlanId, CreateSectionDto input);
        Task<TaskDto> AddTaskToSectionAsync(Guid sectionId,CreateTaskDto input);
        Task<PlanLayerdto> AddLayerToSectionAsync(Guid sectionId,CreateLayerDto input);



    }

        public class CreateDailyPlanDto
    {
        public string Title { get; set; }
    }
        public class CreateSectionDto
    {
        public string Name { get; set; }
    }
        public class CreateTaskDto { 
    public string Name { get; set; }
        public int Duration { get; set; }
    }
        public class CreateLayerDto {
        public string Name { get; set; }
        public int Duration { get; set; }
            }

        public class DailyPlanDto:EntityDto<Guid> 
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<SectionDto> Sections { get; set; }
    
    }
        public class SectionDto
    { 
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<TaskDto> Tasks { get; set; }
        public List<PlanLayerdto> PlanLayers { get; set; }
        public int TotalTaskDuration {  get; set; }
        public int TotalLayerDuration { get; set; }
        public int TotalDuration { get; set; }
        public int CumulativeTaskDuration { get; set; }
        public int CumulativeLayerDuration { get; set; }
        public int CumulativeTotalDuration { get; set; }
           
    }
        public class TaskDto {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
    }
        public class PlanLayerdto { 
        public Guid Id { get; set; }
    public string Name { get; set; }
        public int Duration { get; set; }
    }
    
}
