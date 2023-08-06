using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Cyvil.Mvc.Data;
using Cyvil.Mvc.Domain;
using Cyvil.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Cyvil.Mvc.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        private readonly ILogger<TasksController> _logger;

        public TasksController(ILogger<TasksController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create([Bind("Id,Name,Details,Deadline,ProjectId,ObjectiveId")] ActionItem item)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var json = JsonConvert.SerializeObject(errors);

                return Json(json);
            }

            await _context.AddAsync(item);
            await _context.SaveChangesAsync();

            return new JsonResult(item);
        }

        [Route("Assign")]
        [HttpPost]
        public async Task<JsonResult> Assign([FromBody] SelectedUsers model)
        {

            foreach (var assignee in model.Selected)
            {
                var assignment = new Assignment { AssigneeId = assignee, ActionItemId = model.ItemId };
                _context.Add(assignment);
            }
            
            await _context.SaveChangesAsync();

            return new JsonResult("Assignments were successful.");
        }

        [Route("{id}/GetAssignees")]
        [HttpGet]
        public async Task<JsonResult> GetAssignees(long? id)
        {
            if (id == null || _context.ActionItems == null)
            {
                return new JsonResult("Not Found");
            }

            var actionItem = await _context.ActionItems
                .Include(p => p.Objective)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (actionItem == null)
            {
                return new JsonResult("Not Found");
            }

            var assignees = PopulateAssignedTaskData(actionItem);
            return new JsonResult(assignees);
        }

        [Route("[action]")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        private List<AssignedTask> PopulateAssignedTaskData(ActionItem item)
        {
            var teamMembers = _context.TeamMembers
                .Where(t => t.TeamId == item!.Objective!.TeamId)
                .Include(t => t.Member)
                .ToList();
            var assignments = _context.Assignments.Where(a => a.ActionItemId == item.Id).ToList();
            var assignees = new HashSet<string>(assignments.Select(t => t.AssigneeId));
            var assigned = new List<AssignedTask>();
            Console.WriteLine($"====={assignments.Count()}======");
            foreach (var per in assignees)
            {
                Console.WriteLine($"====={per}======");
            }
            foreach(var member in teamMembers)
            {
                assigned.Add(new AssignedTask
                {
                    UserId = member.Member!.Id,
                    Name = member.Member!.FullName,
                    Assigned = assignees.Contains(member.MemberId)
                });
            }
            return assigned;
        }
    }
}