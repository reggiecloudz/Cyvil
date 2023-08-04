using Cyvil.Mvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Models
{
    public class AssignTeamModel
    {
        public AssignTeamModel(){}

        public long TeamId { get; set; }

        public List<Team> Teams { get; set; } = new List<Team>();
    }
}