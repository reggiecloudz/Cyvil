using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Domain
{
    public class ActionItem : Entity
    {
        public ActionItem() {}
        
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        [DataType(DataType.Text)]
        public string Details { get; set; } = string.Empty;

        public DateTime Deadline { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsCompleted { get; set; } = false;

        public long ProjectId { get; set; }

        public long ObjectiveId { get; set; }
        public virtual Objective? Objective { get; set; }

        public virtual ICollection<Assignment> Assignees { get; set; } = new List<Assignment>();
    }
}