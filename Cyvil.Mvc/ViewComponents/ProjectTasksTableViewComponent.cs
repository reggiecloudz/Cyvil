using Cyvil.Mvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.ViewComponents
{
    public class ProjectTasksTableViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        
        public ProjectTasksTableViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(long projectId)
        {
            var tasks = await _context.ActionItems
                .Where(a => a.ProjectId == projectId && a.ParentId == null)
                .ToListAsync();

            return View(tasks);
        }
    }
}