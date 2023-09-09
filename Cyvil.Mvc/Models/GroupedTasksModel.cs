using Cyvil.Mvc.Domain;

namespace Cyvil.Mvc.Models
{
    public class GroupedTasksModel
    {
        public ActionItem? Task { get; set; }
        public IEnumerable<ActionItem> Subtasks { get; set; } = new List<ActionItem>();
    }
}