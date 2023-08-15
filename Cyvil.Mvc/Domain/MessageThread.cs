using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Domain
{
    public class MessageThread : Entity
    {
        public long Id { get; set; }

        public string Subject { get; set; } = string.Empty;

        public long? MessageThreadTypeId { get; set; } = null;
        public MessageThreadType MessageThreadType { get; set; } = MessageThreadType.General;


        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

        public virtual ICollection<MessageRecipient> Recipients { get; set; } = new List<MessageRecipient>();
    }
}