using Cyvil.Mvc.Infrastructure.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Domain
{
    public class Project : Entity
    {
        public Project() {}
        
        public long Id { get; set; }

        public string Slug { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Goal { get; set; } = string.Empty;

        public ProgressStatus Status { get; set; } = ProgressStatus.Draft;

        public string Photo { get; set; } = "noimage.png";

        [NotMapped]
        [FileExtension]
        public IFormFile? PhotoUpload { get; set; }

        public DateTime Deadline { get; set; }

        public DateTime PublishDate { get; set; }

        public virtual Proposal? Proposal { get; set; }

        public string ManagerId { get; set; } = string.Empty;
        public virtual ApplicationUser? Manager { get; set; }

        public long CauseId { get; set; }
        public virtual Cause? Cause { get; set; }

        public long CityId { get; set; }
        public virtual City? City { get; set; }

        public virtual ICollection<Position> Positions { get; set; } = new List<Position>();
        public virtual ICollection<ProjectParticipant> Participants { get; set; } = new List<ProjectParticipant>();
        public virtual ICollection<Team> Teams { get; set; } = new List<Team>();

        public int DaysPassed()
        {
            return (DateTime.Now.Date - PublishDate.Date).Days;
        }
    }
}