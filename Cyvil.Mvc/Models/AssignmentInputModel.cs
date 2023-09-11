#nullable disable

namespace Cyvil.Mvc.Models
{
    public class AssignmentInputModel
    {
        public long ActionItemId { get; set; }
        public string[] SelectedUsers { get; set; } = Array.Empty<string>();
    }
}