#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Models
{
    public class SelectedUsers
    {
        public long ItemId { get; set; }
        public string[] Selected { get; set; }
    }
}