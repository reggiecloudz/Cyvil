using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Domain
{
    public class Interview : Entity
    {
        public long Id { get; set; }
        
        public DateTime InterviewDate { get; set; }

        [DataType(DataType.Time)]
        public string StartTime { get; set; } = string.Empty;

        [DataType(DataType.Time)]
        public string EndTime { get; set; } = string.Empty;

        public string InterviewerName { get; set; } = string.Empty;

        public InterviewStatus Status { get; set; } = InterviewStatus.TBD;

        public bool IsCancelled { get; set; } = false;

        [DataType(DataType.Text)]
        public string InitialMessage { get; set; } = string.Empty;

        [DataType(DataType.Text)]
        public string Feedback { get; set; } = string.Empty;

        public long ProjectId { get; set; }

        public long TimeslotId { get; set; }
        public virtual Timeslot? Timeslot { get; set; }

        public long ApplicantId { get; set; }
        public virtual Applicant? Applicant { get; set; }
    }
}