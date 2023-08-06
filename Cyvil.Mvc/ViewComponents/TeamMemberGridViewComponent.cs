using Cyvil.Mvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.ViewComponents
{
    public class TeamMemberGridViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        
        public TeamMemberGridViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(long teamId)
        {
            var teamMembers = await _context.TeamMembers
                .Where(t => t.TeamId == teamId)
                .Include(t => t.Member)
                .ToListAsync();
            
            return View(teamMembers);
        }
        
    }
}