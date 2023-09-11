using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Models
{
    public class SelectableItemModel
    {
        public long Value { get; set; }
        public string Text { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public bool IsSelected { get; set; } = false;
    }
}