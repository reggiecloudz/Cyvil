using System.ComponentModel;

namespace Cyvil.Mvc.Domain
{
    public enum ApplicantStatus
    {
        [Description("Applied")]
        Applied,
        [Description("Withdrawn")]
        Withdrawn,
        [Description("Interview")]
        Interview,
        [Description("Closed")]
        Closed
    }
}