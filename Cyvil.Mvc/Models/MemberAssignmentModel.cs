#nullable disable

namespace Cyvil.Mvc.Models
{
    public class MemberAssignmentModel
    {
        public string AssigneeId { get; set; } = string.Empty;
        public long[] SelectedTasks { get; set; }
    }
}