using System.ComponentModel;

namespace Cyvil.Mvc.Domain
{
    public enum ProgressStatus
    {
        [Description("Draft")]
        Draft,
        [Description("Pending")]
        Pending,
        [Description("Paused")]
        Paused,
        [Description("Ongoing")]
        Ongoing,
        [Description("Cancelled")]
        Cancelled,
        [Description("Completed")]
        Completed
    }
}