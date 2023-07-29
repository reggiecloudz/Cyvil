using Cyvil.Mvc.Infrastructure.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Models
{
    public class ProjectInputModel
    {
        public ProjectInputModel() {}
        
        public string Name { get; set; } = string.Empty;

        public string Goal { get; set; } = string.Empty;

        [FileExtension]
        public IFormFile? PhotoUpload { get; set; }

        public long CityId { get; set; }

        public long CauseId { get; set; }
    }
}