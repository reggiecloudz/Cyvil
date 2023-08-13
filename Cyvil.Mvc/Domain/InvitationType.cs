using System.ComponentModel;

namespace Cyvil.Mvc.Domain
{
    public enum InvitationType
    {
        [Description("Connection")]
        Connection,
        [Description("Meeting")]
        Meeting,
        [Description("Poll")]
        Poll,
        [Description("Project")]
        Project,
    }
}