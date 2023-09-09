using Cyvil.Mvc.Data;
using Cyvil.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cyvil.Mvc.ViewComponents
{
    public class SelectableActionItemViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        
        public SelectableActionItemViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(long teamId)
        {
            var tasks = await _context.ActionItems
            .Where(x => x.TeamId == teamId)
            .Include(x => x.Subtasks)
            .ToListAsync();

            var groupedTasks = tasks
            .Where(c => c.ParentId == null)
            .Select(c => new GroupedTasksModel
            {
                Task = c,
                Subtasks = tasks.Where(sc => sc.ParentId == c.Id)
            });
            
            return View(groupedTasks);
        }
    }
}