using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace TodoApp.Domain
{
    public class Section:
        FullAuditedEntity<Guid>
    {
        public string Name { get; set; }
        public Guid DailyPlanId { get; set; }
        public DailyPlan DailyPlan { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<PlanLayer> PlanLayers { get; set; }

        public int TotalTaskDuration { get; set; }
        public int TotalLayerDuration { get; set; }
        public int TotalDuration => TotalTaskDuration +
            TotalLayerDuration;
     }
}
