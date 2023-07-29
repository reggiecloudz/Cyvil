using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Models
{
    public class PositionInputResponseModel
    {
        public long PositionId { get; set; }

        public string Title { get; set; } = string.Empty;

        public uint PeopleNeeded { get; set; }
    }
}