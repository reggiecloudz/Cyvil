using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Models
{
    public class ShowApplicantViewModel
    {
        public ShowApplicantViewModel() {}

        public string ApplicantStatus { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;
        public string UserFullName { get; set; } = string.Empty;
        public string UserPhoto { get; set; } = string.Empty;

        public string PositionTitle { get; set; } = string.Empty;
        public long PositionId { get; set; }

        public string ProjectPhoto { get; set; } = string.Empty;


    }
}