using Cyvil.Mvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.ViewComponents
{
    public class SubtasksListViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        
        public SubtasksListViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(long parentId)
        {
            var applicants = await _context.ActionItems
                .Where(a => a.ParentId == parentId)
                .ToListAsync();

            return View(applicants);
        }
    }
}