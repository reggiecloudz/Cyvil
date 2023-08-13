using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Cyvil.Mvc.Infrastructure.Validation;

namespace Cyvil.Mvc.Domain
{
    public class Applicant : Entity
    {
        public Applicant() {}
        
        public long Id { get; set; }

        public ApplicantStatus ApplicantStatus { get; set; } = ApplicantStatus.Applied;

        [DataType(DataType.Text)]
        public string About { get; set; } = string.Empty;

        public long ProjectId { get; set; }

        public virtual Interview? Interview { get; set; }

        public long PositionId { get; set; }
        public virtual Position? Position { get; set; }

        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser? User { get; set; }
    }
}