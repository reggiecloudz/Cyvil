using Cyvil.Mvc.Data;
using Cyvil.Mvc.Data.Services;
using Cyvil.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cyvil.Mvc.ViewComponents
{
    public class SelectableActionItemViewComponent : ViewComponent
    {
        private readonly IActionItemService _service;

        public SelectableActionItemViewComponent(IActionItemService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync(long teamId, string assigneeId)
        {
            var groupedTasks = await _service.GetSelectedTasksAsync(teamId, assigneeId);
            return View(groupedTasks);
        }
    }
}