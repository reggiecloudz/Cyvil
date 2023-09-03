using System.ComponentModel;

namespace Cyvil.Mvc.Domain
{
    public enum MeetingType
    {
        [Description("Public")]
        Public,
        [Description("Project")]
        Project,
        [Description("Position")]
        Position,
        [Description("Team")]
        Team,
        [Description("Task")]
        Task

    }
}