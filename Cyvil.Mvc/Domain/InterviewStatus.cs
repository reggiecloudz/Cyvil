using System.ComponentModel;

namespace Cyvil.Mvc.Domain
{
    public enum InterviewStatus
    {
        [Description("To be determined")]
        TBD,
        [Description("Withdrawn")]
        Withdrawn,
        [Description("Selected")]
        Selected,
        [Description("Not selected")]
        NotSelected
    }
}