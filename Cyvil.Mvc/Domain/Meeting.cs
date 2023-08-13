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
        public int MaxAttendees { get; set; }

        public bool IsPublic { get; set; } = false;

        public bool IsPrivate { get; set; } = false;

        public long? MeetingTypeId { get; set; } = null;
        public MeetingType MeetingType { get; set; } = MeetingType.Project;

        public long ProjectId { get; set; }
        public virtual Project? Project { get; set; }

        public virtual ICollection<Attendee> Attendees { get; set; } = new List<Attendee>();
    }
}