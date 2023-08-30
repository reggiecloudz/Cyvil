using System.ComponentModel;

namespace Cyvil.Mvc.Domain
{
    public enum ProgressStatus
    {
        [Description("Draft")]
        Draft,
        [Description("Pending")]
        Pending,
        [Description("Postponed")]
        Postponed,
        [Description("In progress")]
        InProgress,
        [Description("Cancelled")]
        Cancelled,
        [Description("Completed")]
        Completed
    }
}