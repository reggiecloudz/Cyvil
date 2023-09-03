using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Domain
{
    public class Invitation : Entity
    {
        [Key]
        public long Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public string Response { get; set; } = string.Empty;

        public long MeetingId { get; set; }
        public virtual Meeting? Meeting { get; set; }

        public string FromUserId { get; set; } = string.Empty;

        [ForeignKey("FromUserId")]
        public virtual ApplicationUser? FromUser { get; set; }

        public string ToUserId { get; set; } = string.Empty;

        [ForeignKey("ToUserId")]
        public virtual ApplicationUser? ToUser { get; set; }
        
        public bool IsAccepted { get; set; }
    }
}