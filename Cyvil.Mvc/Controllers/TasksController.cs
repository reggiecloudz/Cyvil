using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Cyvil.Mvc.Data;
using Cyvil.Mvc.Domain;
using Cyvil.Mvc.Extensions;
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
                TaskId = item.Id,
                Name = item.Name,
                Status = item.Status.GetEnumDescription(),
                DeadlineDate = item.Deadline.ToString("MMM dd, yyyy"),
                DeadlineTime = item.Deadline.ToString("hh:mm tt")
            });
        }

        [Route("{id}/Start")]
        public async Task<JsonResult> Start(long? id)
        {
            if (id == null) 
            {
                return new JsonResult("Not found");
            }

            var actionItem = await _context.ActionItems.FirstOrDefaultAsync(x => x.Id == id);

            if (actionItem == null)
            {
                return new JsonResult("Not found");
            }

            actionItem.Status = ProgressStatus.InProgress;
            actionItem.StartDate = DateTime.Now;

            _context.Update(actionItem);
            _context.SaveChanges();

            return new JsonResult(new 
            {
                Message = "Operation sucessful!",
                Id = actionItem.Id,
                Status = actionItem.Status.GetEnumDescription()
            });
        }


        [Route("{id}/Restart")]
        public async Task<JsonResult> Restart(long? id)
        {
            if (id == null) 
            {
                return new JsonResult("Not found");
            }

            var actionItem = await _context.ActionItems.FirstOrDefaultAsync(x => x.Id == id);

            if (actionItem == null)
            {
                return new JsonResult("Not found");
            }

            actionItem.Status = ProgressStatus.InProgress;

            _context.Update(actionItem);
            _context.SaveChanges();

            return new JsonResult(new 
            {
                Message = "Operation sucessful!",
                Id = actionItem.Id,
                Status = actionItem.Status.GetEnumDescription()
            });
        }


        [Route("{id}/Postpone")]
        public async Task<JsonResult> Postpone(long? id)
        {
            if (id == null) 
            {
                return new JsonResult("Not found");
            }

            var actionItem = await _context.ActionItems.FirstOrDefaultAsync(x => x.Id == id);

            if (actionItem == null)
            {
                return new JsonResult("Not found");
            }

            actionItem.Status = ProgressStatus.Postponed;

            _context.Update(actionItem);
            _context.SaveChanges();

            return new JsonResult(new 
            {
                Message = "Operation sucessful!",
                Id = actionItem.Id,
                Status = actionItem.Status.GetEnumDescription()
            });
        }
        
        [Route("{id}/Complete")]
        public async Task<JsonResult> Complete(long? id)
        {
            if (id == null) 
            {
                return new JsonResult("Not found");
            }

            var actionItem = await _context.ActionItems.FirstOrDefaultAsync(x => x.Id == id);

            if (actionItem == null)
            {
                return new JsonResult("Not found");
            }

            actionItem.Status = ProgressStatus.Completed;
            actionItem.IsCompleted = true;
            actionItem.EndDate = DateTime.Now;

            _context.Update(actionItem);
            _context.SaveChanges();

            return new JsonResult(new 
            {
                Message = "Operation sucessful!",
                Status = actionItem.Status.GetEnumDescription()
            });
        }

        [Route("{id}/GetActionItem")]
        public async Task<JsonResult> GetActionItem(long? id)
        {
            if (id == null) 
            {
                return new JsonResult("Not found");
            }

            var actionItem = await _context.ActionItems.FirstOrDefaultAsync(x => x.Id == id);

            if (actionItem == null)
            {
                return new JsonResult("Not found");
            }

            return new JsonResult(new
            {
                Id = actionItem.Id,
                Name = actionItem.Name,
                Details = actionItem.Details,
                Deadline = actionItem.Deadline,
                DeadlineDate = actionItem.Deadline.ToString("MMM dd, yyyy"),
                DeadlineTime = actionItem.Deadline.ToString("hh:mm tt"),
                StatusValue = actionItem.Status,
                StatusText = actionItem.Status.GetEnumDescription()
            });
        }

        [Route("{id}/Update-Subtask")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> UpdateSubtask(long? id, ActionItemEditModel model)
        {
            if (id == null) 
            {
                return new JsonResult("Not found");
            }

            var actionItem = await _context.ActionItems.FirstOrDefaultAsync(x => x.Id == id);

            if (actionItem == null)
            {
                return new JsonResult("Not found");
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var json = JsonConvert.SerializeObject(errors);

                return Json(json);
            }

            actionItem.Name = model.ModelName;
            actionItem.Details = model.ModelDetails;
            actionItem.Deadline = model.ModelDeadline;
            actionItem.Status = model.ModelStatus;

            _context.Update(actionItem);
            _context.SaveChanges();

            return new JsonResult(new
            {
                Message = "Operation Successful.",
                Id = actionItem.Id,
                Name = actionItem.Name,
                Status = actionItem.Status.GetEnumDescription()
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