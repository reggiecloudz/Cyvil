using System.ComponentModel;

namespace Cyvil.Mvc.Domain
{
    public enum ProgressStatus
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