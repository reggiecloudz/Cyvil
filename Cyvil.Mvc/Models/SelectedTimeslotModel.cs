using Cyvil.Mvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Models
{
    public class SelectedTimeslotModel
    {
        public SelectedTimeslotModel() {}

        public long TimeslotId { get; set; }

        public List<Timeslot> Timeslots { get; set; } = new List<Timeslot>();
    }
}