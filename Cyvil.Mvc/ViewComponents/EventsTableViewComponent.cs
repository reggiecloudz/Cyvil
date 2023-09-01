using Cyvil.Mvc.Data;
using Cyvil.Mvc.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.ViewComponents
{
    public class EventsTableViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        
        public EventsTableViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string creatorId)
        {
            var events = await _context.Meetings
                .Where(a => a.CreatorId == creatorId)
                .ToListAsync();

            return View(events);
        }
    }
}