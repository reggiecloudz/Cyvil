using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Cyvil.Mvc.Infrastructure.Validation;

namespace Cyvil.Mvc.Domain
{
    public class ApplicationUser : IdentityUser<string>
    {
        public string FullName { get; set; } = string.Empty;

        public string Organization { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public bool UseOrganizationName { get; set; } = false;

        public bool IsMember { get; set; } = true;

        public bool OnCreativeHold { get; set;} = false;

        public string ProfileImage { get; set; } = "noimage.png";

        [NotMapped]
        [FileExtension]
        public IFormFile? ProfileImageUpload { get; set; }

        public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
        public virtual ICollection<ProjectParticipant> Participations { get; set; } = new List<ProjectParticipant>();
        public virtual ICollection<TeamMember> Teams { get; set; } = new List<TeamMember>();
        public virtual ICollection<Meeting> Events { get; set; } = new List<Meeting>();
        public virtual ICollection<Attendee> Meetings { get; set; } = new List<Attendee>();
        public virtual ICollection<Applicant> Applications { get; set; } = new List<Applicant>();
        public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
        // public virtual ICollection<UserNotification> Notifications { get; set; } = new List<UserNotification>();
        // public virtual ICollection<ChatUser> Chats { get; set; } = new List<ChatUser>();
    }
}