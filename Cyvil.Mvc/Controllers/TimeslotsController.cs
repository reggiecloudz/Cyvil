using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Cyvil.Mvc.Data;
using Cyvil.Mvc.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Cyvil.Mvc.Controllers
{
    [Route("[controller]")]
    public class TimeslotsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TimeslotsController> _logger;

        public TimeslotsController(ILogger<TimeslotsController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Route("[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create([Bind("Id,Date,StartTime,EndTime,InterviewScheduleId")] Timeslot timeslot)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var json = JsonConvert.SerializeObject(errors);

                return Json(json);
            }
            
            await _context.AddAsync(timeslot);
            await _context.SaveChangesAsync();
            return new JsonResult(timeslot);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}