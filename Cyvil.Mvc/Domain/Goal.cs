using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Domain
{
    public class Goal : Entity
    {
        public long Id { get; set; }

        public string Content { get; set; } = string.Empty;

        public DateTime Deadline { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsCompleted { get; set; } = false;

        public long ProjectId { get; set; }
        public virtual Project? Project { get; set; }
    }
}