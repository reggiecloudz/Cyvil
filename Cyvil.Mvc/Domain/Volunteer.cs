using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Domain
{
    public class Volunteer : Entity
    {
        public string Position { get; set; } = string.Empty;
        
        public long ProjectId { get; set; }
        public virtual Project? Project { get; set; }

        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser? User { get; set; }
    }
}