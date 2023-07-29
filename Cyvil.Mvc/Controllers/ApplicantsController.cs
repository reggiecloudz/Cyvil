#nullable disable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Cyvil.Mvc.Data;
using Cyvil.Mvc.Domain;
using Cyvil.Mvc.Extensions;
using Cyvil.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cyvil.Mvc.Controllers
{
    [Route("[controller]")]
    public class ApplicantsController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        private readonly ILogger<ApplicantsController> _logger;

        public ApplicantsController(ILogger<ApplicantsController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Route("{id}/[action]")]
        public async Task<JsonResult> Show(long? id)
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
            
            var model = new ShowApplicantViewModel
            {
                ApplicantStatus = app.ApplicantStatus.GetEnumDescription(),
                UserId = app.UserId,
                UserFullName = app.User.FullName,
                UserPhoto = app.User.ProfileImage,
                PositionId = app.PositionId,
                PositionTitle = app.Position.Title,
                ProjectPhoto = _context.Projects.Where(p => p.Id == app.ProjectId).Select(p => p.Photo).FirstOrDefault()
            };
            return new JsonResult(model);
        }

        [Route("{id}/[action]")]
        public async Task<JsonResult> Select(long? id)
        {
            if (id == null || _context.Applicants == null)
            {
                return new JsonResult("Not found");
            }

            var app = await _context.Applicants
                .Include(a => a.Position)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (app == null)
            {
                return new JsonResult("Not found");
            }

            if(app.ApplicantStatus != ApplicantStatus.Selected)
            {
                app.ApplicantStatus = ApplicantStatus.Selected;
                app.Position!.PeopleNeeded = app.Position.PeopleNeeded - 1;
                app.Position!.PositionsFilled = app.Position.PositionsFilled + 1;
                _context.Applicants.Update(app);

                _context.Volunteers.Add(new Volunteer
                {
                    ProjectId = app.ProjectId,
                    UserId = app.UserId,
                    Position = app.Position.Title,
                });
                _context.SaveChanges();
                return new JsonResult(app.ApplicantStatus.GetEnumDescription());
            }
            return new JsonResult("Well the bottom's fell out");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}