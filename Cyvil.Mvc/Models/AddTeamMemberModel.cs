using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Models
{
    public class AddTeamMemberModel
    {
        public AddTeamMemberModel() {}

        public string Role { get; set; } = string.Empty;
        
        public string MemberId { get; set; } = string.Empty;

        public List<SelectableVolunteer> Volunteers { get; set; } = new List<SelectableVolunteer>();

    }
}