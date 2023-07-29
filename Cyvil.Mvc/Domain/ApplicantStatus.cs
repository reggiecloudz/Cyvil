using System.ComponentModel;

namespace Cyvil.Mvc.Domain
{
    public enum ApplicantStatus
    {
        [Description("Applied")]
        Applied,
        [Description("Withdrawn")]
        Withdrawn,
        [Description("Inactive")]
        Inactive,
        [Description("Selected")]
        Selected,
        [Description("Not Selected")]
        Rejected,
        [Description("Suspended")]
        Suspended,

    }
}