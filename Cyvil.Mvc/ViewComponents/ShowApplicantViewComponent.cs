using Cyvil.Mvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.ViewComponents
{
    public class ShowApplicantViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
          
        }
        
    }
}