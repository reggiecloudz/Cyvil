using Cyvil.Mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace Cyvil.Mvc.Data.Services
{
    public class ActionItemService : IActionItemService
    {
        private readonly ApplicationDbContext _context;

        public ActionItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SelectableUserModel>> GetSelectedUsersAsync(long actionItemId)
        {
            var actionItem = await _context.ActionItems
                .Include(x => x.Team)
                    .ThenInclude(x => x!.Members)
                        .ThenInclude(x => x.Member)
                .FirstOrDefaultAsync(a => a.Id == actionItemId);

            var team = actionItem!.Team;

            var assignments = await _context.Assignments
                .Where(x => x.ActionItemId == actionItem.Id)
                .Select(x => x.AssigneeId)
                .ToListAsync();

            var selectableUsers = new List<SelectableUserModel>();

            foreach (var item in team!.Members)
            {
                selectableUsers.Add(new SelectableUserModel
                {
                    Id = item.MemberId,
                    Name = item.Member!.FullName,
                    Role = item.Role,
                    Selected = assignments.Contains(item.MemberId)
                });
            }
            
            return selectableUsers;
        }
    }
}