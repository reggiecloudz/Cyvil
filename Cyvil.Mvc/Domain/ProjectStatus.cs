using System.ComponentModel;

namespace Cyvil.Mvc.Domain
{
    public enum ProjectStatus
    {
        [Description("Draft")]
        Draft,
        [Description("Inactive")]
        Inactive,
        [Description("Ongoing")]
        Ongoing,
        [Description("Cancelled")]
        Cancelled,
        [Description("Completed")]
        Completed
    }
}