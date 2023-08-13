using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Domain
{
    public class MessageRecipient : Entity
    {
        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser? User { get; set; }

        public long MessageThreadId { get; set; }
        public virtual MessageThread? MessageThread { get; set; }
    }
}