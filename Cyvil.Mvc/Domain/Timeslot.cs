using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Domain
{
    public class Timeslot : Entity
    {
        public Timeslot() { }
        
        public long Id { get; set; }
        
        public DateTime Date { get; set; }
        
        public string StartTime { get; set; } = string.Empty;

        public string EndTime { get; set; } = string.Empty;

        public bool IsBooked { get; set; } = false;

        public virtual Interview? Interview { get; set; }

        public long InterviewScheduleId { get; set; }
        public virtual InterviewSchedule? InterviewSchedule { get; set; }
    }
}