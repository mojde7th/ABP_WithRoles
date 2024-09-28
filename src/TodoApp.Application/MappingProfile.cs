using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain;
using Task = TodoApp.Domain.Task;

namespace TodoApp.Application
{
    public class MappingProfile:Profile
    {
        public MappingProfile() {
            //Map From DailyPlan to dailyPlanDto and reverse
            CreateMap<DailyPlan, DailyPlanDto>()
                    .ForMember
                    (dest => dest.Sections,
                    opt => opt.MapFrom(src => src.Sections
                    ));
            //Map from Sections to sectionDto and reverse
            CreateMap<Section, SectionDto>()
                .ForMember(dest => dest.Tasks, opt => opt.MapFrom(src => src.Tasks))
                .ForMember(dest => dest.PlanLayers, opt => opt.MapFrom(src => src.PlanLayers))
                .ForMember(dest => dest.TotalTaskDuration, opt => opt.MapFrom(src => src.TotalTaskDuration))
                .ForMember(dest => dest.TotalLayerDuration, opt => opt.MapFrom(src => src.TotalLayerDuration))
                .ForMember(dest => dest.TotalDuration, opt => opt.Ignore());
            //Map from Task to TaskDto and reverse
            CreateMap<Task, TaskDto>();
            //Map from Layer to PlanLayerDto and reverse
            CreateMap<PlanLayer, PlanLayerdto>();
            //Reverse mappings for creating entities from 
            //Dtos (for creation and updates)
            CreateMap<CreateDailyPlanDto,DailyPlan>();
            CreateMap<CreateSectionDto, Section>();
            CreateMap<CreateTaskDto, Task>();
            CreateMap<CreateLayerDto, PlanLayer>();
        }
        
    }
}
