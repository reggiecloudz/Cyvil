using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Domain
{
    public class ProjectParticipant : Entity
    {
        public string Position { get; set; } = string.Empty;
        
        public long ProjectId { get; set; }
        public virtual Project? Project { get; set; }

        public string ParticipantId { get; set; } = string.Empty;
        public virtual ApplicationUser? Participant { get; set; }
    }
}