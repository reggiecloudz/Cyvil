using Cyvil.Mvc.Data;
using Cyvil.Mvc.Data.Services;
using Cyvil.Mvc.Domain;
using Cyvil.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cyvil.Mvc.ViewComponents
{
    public class GetInviteesViewComponent : ViewComponent
    {
        private readonly IMeetingService _service;
        public GetInviteesViewComponent(IMeetingService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync(int eventType, long id)
        {
            var invitees = await _service.GetInvitees(eventType, id);
            return View(invitees);
        }
    }
}