using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Domain
{
    public class Proposal : Entity
    {
        public long Id { get; set; }

        public int VolunteersNeeded { get; set; } = 0;

        [DataType(DataType.Text)]
        public string Problem { get; set; } = string.Empty;

        [DataType(DataType.Text)]
        public string Beneficiaries { get; set; } = string.Empty;

        [DataType(DataType.Text)]
        public string Importance { get; set; } = string.Empty;

        [DataType(DataType.Text)]
        public string Solution { get; set; } = string.Empty;

        [DataType(DataType.Text)]
        public string Execution { get; set; } = string.Empty;

        public long ProjectId { get; set; }
        public virtual Project? Project { get; set; }
    }
}