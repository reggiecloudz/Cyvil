using Cyvil.Mvc.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.ViewComponents
{
    public class SaveActionItemViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(long projectId, long objectiveId)
        {
            return View(new ActionItem
            {
                ProjectId = projectId,
                ObjectiveId = objectiveId
            });
        }
    }
}