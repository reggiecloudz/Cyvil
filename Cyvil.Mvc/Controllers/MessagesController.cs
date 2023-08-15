using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cyvil.Mvc.Data;
using Cyvil.Mvc.Domain;
using Cyvil.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Cyvil.Mvc.Controllers
{
    [Route("[controller]")]
    public class MessagesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MessagesController> _logger;

        public MessagesController(ILogger<MessagesController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create([Bind("Content,TypeId,ManagerId")] MessageInputModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var json = JsonConvert.SerializeObject(errors);

                return Json(json);
            }

            var thread = new MessageThread
            {
                Subject = $"{User.FindFirst("FullName")!.Value} in regards to {_context.Positions.Where(p => p.Id == model.TypeId).Select(p => p.Title).FirstOrDefault()}",
                MessageThreadType = MessageThreadType.Position,
                MessageThreadTypeId = model.TypeId
            };
            thread.Messages.Add(new Message { Content = model.Content, UserId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value });
            thread.Recipients.Add(new MessageRecipient { UserId = model.ManagerId });
            thread.Recipients.Add(new MessageRecipient { UserId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value });
            await _context.AddAsync(thread);
            // _context.SaveChanges();
            await _context.SaveChangesAsync();
            
            return new JsonResult("Success");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}