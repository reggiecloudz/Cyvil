using Cyvil.Mvc.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Models
{
    public class ActionItemEditModel
    {
        public ActionItemEditModel() {}

        public long ModelId { get; set; }
        
        public string ModelName { get; set; } = string.Empty;

        [DataType(DataType.Text)]
        public string ModelDetails { get; set; } = string.Empty;

        public ProgressStatus ModelStatus { get; set; }

        public DateTime ModelDeadline { get; set; }
    }
}