using Cyvil.Mvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.ViewComponents
{
    public class ApplicantsListViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        
        public ApplicantsListViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(long positionId)
        {
            var applicants = await _context.Applicants
                .Where(a => a.PositionId == positionId)
                .Include(a => a.User)
                .ToListAsync();

            return View(applicants);
        }
    }
}