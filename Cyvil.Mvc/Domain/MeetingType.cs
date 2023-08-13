using System.ComponentModel;

namespace Cyvil.Mvc.Domain
{
    public enum MeetingType
    {
        [Description("Project")]
        Project,
        [Description("Action Item")]
        ActionItem,
        [Description("Team")]
        Team,
        [Description("Interview")]
        Interview
    }
}