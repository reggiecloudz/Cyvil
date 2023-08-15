using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Models
{
    public class MessageInputModel
    {
        public MessageInputModel() {}
        
        public string Content { get; set; } = string.Empty;

        public long TypeId { get; set; }

        public string ManagerId { get; set; } = string.Empty;
    }
}