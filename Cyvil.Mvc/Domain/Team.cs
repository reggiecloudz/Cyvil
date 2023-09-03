using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Domain
{
    public class Team : Entity
    {
        public Team() {}
        
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string ProjectManagerId { get; set; } = string.Empty;

        public long ProjectId { get; set; }
        public virtual Project? Project { get; set; }

        public virtual ICollection<TeamMember> Members { get; set; } = new List<TeamMember>();
        public virtual ICollection<ActionItem> ActionItems { get; set; } = new List<ActionItem>();
    }
}