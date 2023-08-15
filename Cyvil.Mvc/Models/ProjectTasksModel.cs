using Cyvil.Mvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Models
{
    public class ProjectTasksModel
    {
        public Project? Project { get; set; }
        public List<ActionItem> ActionItems { get; set; } = new ();
    }
}