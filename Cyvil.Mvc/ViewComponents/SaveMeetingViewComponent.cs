using Cyvil.Mvc.Data;
using Cyvil.Mvc.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Cyvil.Mvc.ViewComponents
{
    public class SaveMeetingViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View(new Meeting());
        }
    }
    
}