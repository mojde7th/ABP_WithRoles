using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Contracts;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace TodoApp
{
    public interface IAccountAppService:IApplicationService
    {
        Task<string> LoginAsync(LoginDto input);
       Task<bool> CreateRoleAsync(string roleName);
        Task<bool> AssignRoleAsync(string username, string roleName);
    Task<List<string>> GetAllUsernamesAsync();
        
        Task<List<string>> GetAllRolesAsync();
        Task<DailyPlanDto>
            CreateDailyPlanAsync
            (CreateDailyPlanDto input);
    }

    public class CreateDailyPlanDto
    {
        public string Title { get; set; }
        public List<CreateTaskDto> Tasks { get; set; }
        public List<createLayerDto> Layers { get; set; }

    }
    public class CreateTaskDto { 
        public Guid Id { get; set; }
    public string Name { get; set; }
        public int Duration { get; set; }
    }
    public class createLayerDto {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
            }
    public class DailyPlanDto:EntityDto<Guid> 
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<TaskDto> Tasks { get; set; }
        public List<PlanLayerdto> Layers { get; set; }
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
