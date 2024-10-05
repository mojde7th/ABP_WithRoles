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

        Task<DailyPlanDto> CreateDailyPlanWithSectionAsync(createDailyPlanWithSectionsDto input);
        Task<DailyPlanDto> CreateDailyPlanAsync (CreateDailyPlanDto input);
        Task<List<DailyPlanDto>> GetAllDailyPlansAsync();
        Task<DailyPlanDto> GetDailyPlanByIdAsync(Guid id);
        Task<SectionDto> AddSectionToDailyPlanAsync(Guid dailyPlanId, CreateSectionDto input);
        Task<TaskDto> AddTaskToSectionAsync(Guid sectionId,CreateTaskDto input);
        Task<PlanLayerdto> AddLayerToSectionAsync(Guid sectionId,CreateLayerDto input);
        Task<DailyPlanDto> CreateFullDailyPlanAsync(CreateFullDailyplanDto input);


    }

        public class CreateDailyPlanDto
    {
        public string Title { get; set; }
    }
        public class CreateSectionDto
    {
        public string Name { get; set; }
        public List<CreateTaskDto> Tasks { get; set; }
        public List<CreateLayerDto> PlanLayers { get; set; }
    }
        public class CreateTaskDto {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int Duration { get; set; }
    }
        public class CreateLayerDto {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int Duration { get; set; }
            }
         public class CreateSectionDto2
    {
        public string Name { get; set; }
        public List<CreateTaskDto> Tasks { get; set; }
        public List<CreateLayerDto> PlanLayes { get; set; }

    }
        public class createDailyPlanWithSectionsDto
    {
        public string Title { get; set; }
        public List<CreateSectionDto2> sections { get; set; }

    }

        public class CreateFullDailyplanDto
        {
            public string Title { get; set; }
            public List<CreateSectionDto> Sections { get; set; }
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
