using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Models
{
    public class ProjectDetailsModalModel
    {
        public long ProjectId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Goal { get; set; } = string.Empty; 

        public string Photo { get; set; } = string.Empty;

        public string Manager { get; set; } = string.Empty;

        public string Cause { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;
    }
}