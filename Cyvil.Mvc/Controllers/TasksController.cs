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

        [Route("{id}/Details")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.ActionItems == null)
            {
                return NotFound();
            }

            var actionItem = await _context.ActionItems
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (actionItem == null)
            {
                return NotFound();
            }
            
            return View(actionItem);
        }


        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create([Bind("Id,Name,Details,Deadline,ProjectId,TeamId")] ActionItem item)
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

        [Route("New-Subtask")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> NewSubtask([Bind("Id,Name,Details,Deadline,ProjectId,TeamId,ParentId")] ActionItem item)
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

            return new JsonResult(new 
            {
                Message = "Operation Successful",
                Subtask = item.Name
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