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

        public ProgressStatus Status { get; set; } = ProgressStatus.Draft;

        public long? ParentId { get; set; } = null;
        public virtual ActionItem? Parent { get; set; }

        public long ProjectId { get; set; }

        public long TeamId { get; set; }
        public virtual Team? Team { get; set; }

        public virtual ICollection<ActionItem> Subtasks { get; set; } = new List<ActionItem>();

        public int DaysPassed()
        {
            return (DateTime.Now.Date - StartDate.Date).Days;
        }
    }
}