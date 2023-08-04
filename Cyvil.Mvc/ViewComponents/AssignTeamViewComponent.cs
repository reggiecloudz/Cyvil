using Cyvil.Mvc.Data;
using Cyvil.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.ViewComponents
{
    public class AssignTeamViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        
        public AssignTeamViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(long projectId)
        {
            var teams = await _context.Teams.Where(t => t.ProjectId == projectId).ToListAsync();
            return View(new AssignTeamModel
            {
                Teams = teams
            });
        }
        
    }
}