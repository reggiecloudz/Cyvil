using Cyvil.Mvc.Domain;
using Cyvil.Mvc.Models;

namespace Cyvil.Mvc.Data.Services
{
    public interface IActionItemService
    {
        Task<List<SelectableUserModel>> GetSelectedUsersAsync(long actionItemId);
        Task<List<GroupedTasksModel>> GetSelectedTasksAsync(long teamId, string assigneeId);
        Task RemoveUnchecked(string[] assigneeIds, long itemId);
    }
}