using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace TodoApp.Domain
{
    public class DailyPlan: FullAuditedEntity<Guid>
    {
         public string Title { get; set; }
        public virtual ICollection<Section> Sections { get; set; }

    }
}
