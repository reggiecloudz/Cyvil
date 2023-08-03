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
    public class SaveObjectiveViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        
        public SaveObjectiveViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(long projectId, long goalId)
        {
            return View(new Objective
            {
                ProjectId = projectId,
                GoalId = goalId
            });
        }
    }
}