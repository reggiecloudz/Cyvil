using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cyvil.Mvc.Data;
using Cyvil.Mvc.Domain;
using Cyvil.Mvc.Extensions;
using Cyvil.Mvc.Infrastructure.Helpers;
using Cyvil.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Cyvil.Mvc.Controllers
{
    [Route("[controller]")]
    public class PositionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        private readonly ILogger<PositionsController> _logger;

        public PositionsController(ILogger<PositionsController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var positions = await _context.Positions
                .Include(p => p.Project)
                .ThenInclude(p => p!.City)
                    .ThenInclude(c => c!.State)
                .ToListAsync();
            return View(positions);
        }

        [Route("[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create([Bind("Id,Title,Details,PeopleNeeded,ProjectId")] Position position)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var json = JsonConvert.SerializeObject(errors);

                return Json(json);
            }

            position.Slug = FriendlyUrlHelper.GetFriendlyTitle(position.Title);
            await _context.AddAsync(position);
            await _context.SaveChangesAsync();
            _context.SaveChanges();

            return new JsonResult(new PositionInputResponseModel
            {
                PositionId = position.Id,
                Title = position.Title,
                PeopleNeeded = position.PeopleNeeded
            });
        }

        [Route("{id}/Details")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Positions == null)
            {
                return NotFound();
            }

            var position = await _context.Positions
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (position == null)
            {
                return NotFound();
            }

            return View(position);
        }

        //[AllowAnonymous]
        [Route("{id}/[action]")]
        public async Task<JsonResult> GetDetails(long? id)
        {
            if (id == null || _context.Positions == null)
            {
                return new JsonResult("Not found");
            }

            var position = await _context.Positions
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (position == null)
            {
                return new JsonResult("Not found");
            }
            
            var model = new PositionDetailsModalModel
            {
                PositionId = position.Id,
                ProjectId = position.ProjectId,
                Title = position.Title,
                Details = position.Details,
                ManagerId = position.Project!.ManagerId
            };
            return new JsonResult(model);
        }
    }
}