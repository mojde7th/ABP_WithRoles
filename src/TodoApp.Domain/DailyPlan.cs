using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace TodoApp.Domain
{
    public class DailyPlan: Entity<Guid>
    {
        public Guid Id {  get; set; }
        public string Title { get; set; }
        public ICollection<Task> Tasks { get; set; }
        = new List<Task>();
        public ICollection<PlanLayer> Layers { get; set; }
        =new List<PlanLayer>();

    }
}
