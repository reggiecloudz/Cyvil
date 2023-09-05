using System.ComponentModel;

namespace Cyvil.Mvc.Domain
{
    public enum MeetingAttendanceStatus
    {
        [Description("Pending")]
        Pending,
        [Description("Attending")]
        Attending,
        [Description("Not Going")]
        NotGoing
    }
}