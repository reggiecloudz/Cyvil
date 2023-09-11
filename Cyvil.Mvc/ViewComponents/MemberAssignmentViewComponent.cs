using Cyvil.Mvc.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cyvil.Mvc.ViewComponents
{
    public class MemberAssignmentViewComponent : ViewComponent
    {
        private readonly IActionItemService _service;

        public MemberAssignmentViewComponent(IActionItemService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync(long actionItemId)
        {
            var teamMembers = await _service.GetSelectedUsersAsync(actionItemId);
            return View(teamMembers);
        }
    }
}