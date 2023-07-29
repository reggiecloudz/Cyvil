using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Domain
{
    public class Assignment : Entity
    {
        public long ActionItemId { get; set; }
        public virtual ActionItem? ActionItem { get; set; }
        
        public string AssigneeId { get; set; } = string.Empty;
        public virtual ApplicationUser? Assignee { get; set; }
    }
}