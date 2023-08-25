using Cyvil.Mvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.ViewComponents
{
    public class ProjectTableViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        
        public ProjectTableViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string managerId)
        {
            var projects = await _context.Projects.Where(p => p.ManagerId == managerId).ToListAsync();
            return View(projects);
        }
    }
}