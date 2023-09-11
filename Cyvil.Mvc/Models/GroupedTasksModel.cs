using Cyvil.Mvc.Domain;

namespace Cyvil.Mvc.Models
{
    public class GroupedTasksModel
    {
        public ActionItem? Task { get; set; }
        public IEnumerable<SelectableItemModel> Subtasks { get; set; } = new List<SelectableItemModel>();
    }
}