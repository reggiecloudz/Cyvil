using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Domain
{
    public class Message : Entity
    {
        public long Id { get; set; }

        public string Content { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;
        public ApplicationUser? User { get; set; }

        public long MessageThreadId { get; set; }
        public virtual MessageThread? MessageThread { get; set; }
    }
}