using Cyvil.Mvc.Data;
using Cyvil.Mvc.Domain;
using Cyvil.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Cyvil.Mvc.Controllers
{
    [Route("[controller]")]
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create([Bind("Id,Name,Description,Capacity,StartDate,EndDate,Location,MeetingType,MeetingTypeId")] Meeting meeting)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var json = JsonConvert.SerializeObject(errors);

                return Json(json);
            }
            meeting.CreatorId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            await _context.AddAsync(meeting);
            await _context.SaveChangesAsync();

            return new JsonResult(new 
            {
                Message   = "Operation Successful",
                Id        = meeting.Id,
                Name      = meeting.Name,
                StartDate = meeting.StartDate.ToString("MMM dd, yyyy hh:mm tt"),
                EndDate   = meeting.EndDate.ToString("MMM dd, yyyy hh:mm tt"),
                Capacity  = meeting.Capacity,
                Location  = meeting.Location,
                Type      = meeting.MeetingType.GetEnumDescription()
            });
        }

        [Route("{id}/Details")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meetings.FirstOrDefaultAsync(m => m.Id == id);

            if (meeting == null)
            {
                return NotFound();
            }

            return View(meeting);
        }
        
        [Route("{eventType}/GetEventTypeObjects")]
        [HttpGet]
        public async Task<JsonResult> GetEventTypeObjects(int eventType)
        {
            switch (eventType)
            {
                case 1:
                    var projects = await _context.Projects
                        .Where(p => p.ManagerId == User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
                        .ToListAsync();
                    return new JsonResult(projects);
                case 2:
                    var positions = await _context.Positions
                        .Where(p => p.ProjectManagerId == User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
                        .ToListAsync();
                    return new JsonResult(positions);
                case 3:
                    var teams = await _context.Teams
                        .Where(t => t.ProjectManagerId == User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
                        .ToListAsync();
                    return new JsonResult(teams);
                case 4:
                    var actionItems = await _context.ActionItems
                        .Where(t => t.ProjectManagerId == User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
                        .ToListAsync();
                    return new JsonResult(actionItems);
                default:
                    return new JsonResult("Public");
            }
        }
    }
}