using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.ViewComponents
{
    public class AssignVolunteerViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
        
    }
}