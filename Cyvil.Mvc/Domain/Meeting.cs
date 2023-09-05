using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Domain
{
    public class Meeting : Entity
    {
        public Meeting() {}
        
        public long Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string Location { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Range(0, long.MaxValue)]
        public int Capacity { get; set; } = 1;

        public long? MeetingTypeId { get; set; } = null;
        public MeetingType MeetingType { get; set; } = MeetingType.Public;

        public string CreatorId { get; set; } = string.Empty;
        public virtual ApplicationUser? Creator { get; set; }

        public virtual ICollection<Attendee> Attendees { get; set; } = new List<Attendee>();
    }
}