using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Domain
{
    public class TeamMember : Entity
    {
        public string Role { get; set; } = string.Empty;

        public long TeamId { get; set; }
        public virtual Team? Team { get; set; }
        
        public string MemberId { get; set; } = string.Empty;
        public virtual ApplicationUser? Member { get; set; }

    }
}