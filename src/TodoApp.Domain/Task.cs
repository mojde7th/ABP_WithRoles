using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace TodoApp.Domain
{
    public class Task:FullAuditedEntity<Guid>
    {
    
    public string Name { get; set; }
        public int Duration { get; set; }
        public Guid SectionId { get; set; }
        public Section Section { get; set; }
    }
      

    }

