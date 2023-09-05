using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cyvil.Mvc.Data;
using Cyvil.Mvc.Domain;
using Cyvil.Mvc.Infrastructure.Helpers;
using Cyvil.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Cyvil.Mvc.Controllers
{
    [Route("[controller]")]
    public class TeamsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TeamsController> _logger;

        public TeamsController(ILogger<TeamsController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Route("[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create([Bind("Id,Name,ProjectId")] Team team)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var json = JsonConvert.SerializeObject(errors);

                return Json(json);
            }
            team.ProjectManagerId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            await _context.AddAsync(team);
            await _context.SaveChangesAsync();

            return new JsonResult(team);
        }

        [Route("{id}/Details")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Teams == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (team == null)
            {
                return NotFound();
            }
            
            return View(team);
        }

        [Route("Add-Members")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> AddMembers([Bind("Role,MemberId,TeamId")] AddTeamMemberModel model)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var json = JsonConvert.SerializeObject(errors);

                return Json(json);
            }

            await _context.AddAsync(new TeamMember 
            { 
                TeamId = model.TeamId,
                MemberId = model.MemberId, 
                Role = model.Role 
            });

            await _context.SaveChangesAsync();

            var member = _context.Users.FirstOrDefault(x => x.Id == model.MemberId);            

            return new JsonResult(new
            {
                Message = "Operation Successful",
                FullName = member!.FullName,
                Avatar = member.ProfileImage,
                Role = model.Role
            });
        }

        [Route("[action]")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}