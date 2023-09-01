using System.ComponentModel;

namespace Cyvil.Mvc.Domain
{
    public enum MeetingType
    {
        [Description("Private")]
        Private,
        [Description("Public")]
        Public,
        [Description("Group")]
        Group,
        [Description("Project")]
        Project,
        [Description("Team")]
        Team,
        [Description("Task")]
        Task

    }
}