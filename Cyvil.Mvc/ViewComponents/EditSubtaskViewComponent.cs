using Cyvil.Mvc.Domain;
using Cyvil.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.ViewComponents
{
    public class EditSubtaskViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(new ActionItemEditModel());
        }
    }
}