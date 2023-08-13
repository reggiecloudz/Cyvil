using Cyvil.Mvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Models
{
    public class MemberApplicationsViewModel
    {
        public ApplicationUser? User { get; set; }
        public List<Applicant> Applications { get; set; } = new ();
    }
}