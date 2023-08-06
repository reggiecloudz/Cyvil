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
    public class ObjectivesController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        private readonly ILogger<ObjectivesController> _logger;

        public ObjectivesController(ILogger<ObjectivesController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create([Bind("Id,Name,Details,Deadline,ProjectId,GoalId")] Objective objective)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var json = JsonConvert.SerializeObject(errors);

                return Json(json);
            }

            await _context.AddAsync(objective);
            await _context.SaveChangesAsync();

            return new JsonResult(objective);
        }

        [Route("{id}/Assign-Team")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AssignTeam([Bind("TeamId")] AssignTeamModel model, long? id)
        {
            
            if (id == null || _context.Objectives == null)
            {
                return new JsonResult("Not Found");
            }

            var objective = await _context.Objectives
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (objective == null)
            {
                return new JsonResult("Not Found");
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var json = JsonConvert.SerializeObject(errors);

                return Json(json);
            }

            objective.TeamId = model.TeamId;
            _context.Update(objective);
            _context.SaveChanges();

            var teamName = _context.Teams.Where(t => t.Id == model.TeamId).Select(t => t.Name).FirstOrDefault();
            return new JsonResult(teamName);
        }

        [Route("{id}/Details")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Objectives == null)
            {
                return NotFound();
            }

            var objective = await _context.Objectives
                .Include(p => p.Goal)
                .Include(p => p.Team)
                .Include(o => o.ActionItems)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (objective == null)
            {
                return NotFound();
            }
            
            return View(objective);
        }

        [Route("[action]")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}