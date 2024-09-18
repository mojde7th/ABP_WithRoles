using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using TodoApp.Domain;
using Volo.Abp.Domain.Entities;

namespace TodoApp.Domain
{

    public class PlanLayer:Entity<Guid>
    {
        public Guid Id {  get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public Guid DailyPlanId { get; set; }
        public DailyPlan DailyPlan { get; set; }
    }
}
