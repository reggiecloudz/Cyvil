using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Domain
{
    public class InterviewSchedule : Entity
    {
        public long Id { get; set; }

        public long PositionId { get; set; }
        public virtual Position? Position { get; set; }

        public virtual ICollection<Timeslot> Timeslots { get; set; } = new List<Timeslot>();
    }
}