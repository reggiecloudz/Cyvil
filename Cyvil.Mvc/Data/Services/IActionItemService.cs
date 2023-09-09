using Cyvil.Mvc.Models;

namespace Cyvil.Mvc.Data.Services
{
    public interface IActionItemService
    {
        Task<List<SelectableUserModel>> GetSelectedUsersAsync(long actionItemId);
    }
}