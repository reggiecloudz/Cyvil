using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Cyvil.Mvc.Data;
using Cyvil.Mvc.Domain;
using Cyvil.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cyvil.Mvc.Controllers
{
    [Route("[controller]")]
    public class InterviewsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<InterviewsController> _logger;

        public InterviewsController(ILogger<InterviewsController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Route("{id}/[action]")]
        public async Task<JsonResult> Setup(long? id)
        {
            if (id == null || _context.Applicants == null)
            {
                return new JsonResult("Not found");
            }

            var app = await _context.Applicants
                .Include(a => a.User)
                .Include(a => a.Position)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (app == null)
            {
                return new JsonResult("Not found");
            }

            app.ApplicantStatus = ApplicantStatus.Interview;
            _context.Update(app);
            _context.Add(new Interview
            {
                ApplicantId = app.Id,
                InterviewerName = User.FindFirst("FullName")!.Value,
                ProjectId = app.ProjectId
            });
            _context.SaveChanges();

            return new JsonResult("Successfully operation");
        }

        [Route("{id}/[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Schedule(long? id, SelectedTimeslotModel model)
        {
            if (id == null || _context.Applicants == null)
            {
                return new JsonResult("Not found");
            }

            var app = await _context.Applicants
                .Include(a => a.User)
                .Include(a => a.Position)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (app == null)
            {
                return new JsonResult("Not found");
            }

            var interview = _context.Interviews.FirstOrDefault(i => i.ApplicantId == app.Id);

            interview!.TimeslotId = model.TimeslotId;

            _context.Update(interview);
            _context.SaveChanges();

            return new JsonResult("Successful operation");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}