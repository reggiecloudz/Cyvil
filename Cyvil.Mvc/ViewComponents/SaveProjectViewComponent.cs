using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cyvil.Mvc.Data;
using Cyvil.Mvc.Domain;
using Cyvil.Mvc.Models;

namespace Cyvil.Mvc.ViewComponents
{
    public class SaveProjectViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public SaveProjectViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            return View(new ProjectInputModel());
        }
        
    }
}