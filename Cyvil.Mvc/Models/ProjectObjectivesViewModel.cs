using Cyvil.Mvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Models
{
    public class ProjectObjectivesViewModel
    {
        public Project? Project { get; set; }
        public ICollection<Objective> Objectives { get; set; } = new List<Objective>();
    }
}