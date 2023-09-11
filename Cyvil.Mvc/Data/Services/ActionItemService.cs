using Cyvil.Mvc.Domain;
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

        public async Task<List<GroupedTasksModel>> GetSelectedTasksAsync(long teamId, string assigneeId)
        {
            var tasks = await _context.ActionItems
                .Where(x => x.TeamId == teamId)
                .Include(x => x.Subtasks)
                .ToListAsync();

            var assignments = await _context.Assignments
                .Where(x => x.AssigneeId == assigneeId)
                .Select(x => x.ActionItemId)
                .ToListAsync();

            var groupedTasks = tasks
                .Where(c => c.ParentId == null)
                .Select(c => new GroupedTasksModel
                {
                    Task = c,
                    Subtasks = tasks
                        .Where(sc => sc.ParentId == c.Id)
                        .Select(s => new SelectableItemModel
                        {
                            Value = s.Id,
                            Text = s.Name,
                            IsSelected = assignments.Contains(s.Id)
                        }).ToList()
                });

            return groupedTasks.ToList();
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
                    Photo = item.Member!.ProfileImage,
                    Selected = assignments.Contains(item.MemberId)
                });
            }
            
            return selectableUsers;
        }

        public async Task RemoveUnchecked(string[] assigneeIds, long itemId)
        {
            var item = _context.ActionItems.Include(i => i.Assignments).FirstOrDefault(x => x.Id == itemId);
            var assignments = new HashSet<string>(item!.Assignments.Select(u => u.AssigneeId));
            var userIdsList = assigneeIds.ToList();

            foreach (var assignment in assignments)
            {
                if (!userIdsList.Contains(assignment))
                {
                    var getAssignment = await _context.Assignments
                        .Where(x => x.ActionItemId == item.Id && x.AssigneeId == assignment)
                        .FirstOrDefaultAsync();
                    
                    if (getAssignment != null)
                    {
                        _context.Assignments.Remove(getAssignment);
                        _context.SaveChanges();
                    }
                }
            }
        }
    }
}