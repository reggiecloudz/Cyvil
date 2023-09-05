#nullable disable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cyvil.Mvc.Data;
using Cyvil.Mvc.Domain;
using Cyvil.Mvc.Extensions;
using Cyvil.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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

        [Route("[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create([Bind("Id,About,PositionId,ProjectId")] Applicant applicant)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var json = JsonConvert.SerializeObject(errors);

                return Json(json);
            }
            applicant.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await _context.AddAsync(applicant);
            await _context.SaveChangesAsync();

            return new JsonResult("Operation Successful");
        }

        [Route("{id}/Interview")]
        public async Task<JsonResult> Interview(long? id)
        {
            if (id == null)
            {
                return new JsonResult("Not found");
            }

            var applicant = await _context.Applicants.Include(x => x.Position).FirstOrDefaultAsync(x => x.Id == id);

            if (applicant == null)
            {
                return new JsonResult("Not found");
            }

            applicant.ApplicantStatus = ApplicantStatus.Interview;
            _context.Update(applicant);
            _context.SaveChanges();

            return new JsonResult(new 
            {
                Message = "Operation Successful",
                Status = applicant.ApplicantStatus.GetEnumDescription()
            });
        }

        [Route("{id}/Select")]
        public async Task<JsonResult> Select(long? id)
        {
            if (id == null)
            {
                return new JsonResult("Not found");
            }

            var applicant = await _context.Applicants.Include(x => x.Position).FirstOrDefaultAsync(x => x.Id == id);

            if (applicant == null)
            {
                return new JsonResult("Not found");
            }

            applicant.ApplicantStatus = ApplicantStatus.Selected;
            applicant.Position!.PeopleNeeded = applicant.Position!.PeopleNeeded - 1;
            applicant.Position!.PositionsFilled = applicant.Position!.PositionsFilled + 1;
            _context.Update(applicant);
            _context.SaveChanges();

            _context.Add(new ProjectParticipant
            {
                Position = applicant.Position!.Title,
                ProjectId = applicant.ProjectId,
                ParticipantId = applicant.UserId
            });
            _context.SaveChanges();

            return new JsonResult(new 
            {
                Message = "Operation Successful",
                PeopleNeeded = applicant.Position!.PeopleNeeded,
                PositionsFilled = applicant.Position!.PositionsFilled,
                Status = applicant.ApplicantStatus.GetEnumDescription(),
                ApplicantCount = await _context.Applicants
                    .Where(a => a.PositionId == applicant.PositionId && a.ApplicantStatus == ApplicantStatus.Applied)
                    .Select(a => a.Id)
                    .LongCountAsync()
            });
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}