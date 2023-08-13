using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Domain
{
    public class Attendee : Entity
    {
        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser? User { get; set; }

        public long MeetingId { get; set; }
        public virtual Meeting? Meeting { get; set; }
    }
}