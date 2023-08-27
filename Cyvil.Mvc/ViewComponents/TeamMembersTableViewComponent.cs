using Cyvil.Mvc.Data;
using Cyvil.Mvc.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.ViewComponents
{
    public class TeamMembersTableViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        
        public TeamMembersTableViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(long teamId)
        {
            var members = await _context.TeamMembers
                .Where(a => a.TeamId == teamId)
                .Include(a => a.Member)
                .ToListAsync();

            return View(members);
        }
    }
}