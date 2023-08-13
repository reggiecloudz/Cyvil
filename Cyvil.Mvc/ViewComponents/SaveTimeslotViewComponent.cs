using Cyvil.Mvc.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.ViewComponents
{
    public class SaveTimeslotViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(long interviewScheduleId)
        {
            return View(new Timeslot
            {
                InterviewScheduleId = interviewScheduleId
            });
        }
    }
}