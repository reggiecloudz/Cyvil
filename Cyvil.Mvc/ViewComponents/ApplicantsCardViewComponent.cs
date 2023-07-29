using Cyvil.Mvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.ViewComponents
{
    public class ApplicantsCardViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        
        public ApplicantsCardViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(long projectId)
        {
            var applicants = await _context.Applicants
                .Where(x => x.ProjectId == projectId)
                .Include(a => a.Position)
                .Include(a => a.User)
                .ToListAsync();
            return View(applicants);
        }
    }
}