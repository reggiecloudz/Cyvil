using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Models
{
    public class PositionDetailsModalModel
    {
        public long ProjectId { get; set; }
        
        public long PositionId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Details { get; set; } = string.Empty;
    }
}