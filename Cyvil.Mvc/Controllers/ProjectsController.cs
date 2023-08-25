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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Cyvil.Mvc.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<ProjectsController> _logger;

        public ProjectsController(ILogger<ProjectsController> logger, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var projects = await _context.Projects.Include(x => x.Cause).ToListAsync();
            return View(projects);
        }

        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create([Bind("Name,Goal,PhotoUpload,CauseId,CityId")] ProjectInputModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var json = JsonConvert.SerializeObject(errors);

                return Json(json);
            }

            var uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/projects");
            var photoName = Guid.NewGuid().ToString() + "-" + model.PhotoUpload!.FileName;
            var photoFilePath = Path.Combine(uploadsDir, photoName);
            var fs = new FileStream(photoFilePath, FileMode.Create);
            await model.PhotoUpload.CopyToAsync(fs);
            fs.Close();

            var project = new Project
            {
                Slug = FriendlyUrlHelper.GetFriendlyTitle(model.Name),
                Name = model.Name,
                Goal = model.Goal,
                Photo = photoName,
                ManagerId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value,
                CauseId = model.CauseId,
                CityId = model.CityId
            };

            await _context.AddAsync(project);
            await _context.SaveChangesAsync();

            _context.Proposals.Add(new Proposal { ProjectId = project.Id });
            _context.SaveChanges();

            return new JsonResult(new ProjectInputResponseModel
            {
                ProjectId = project.Id,
                Name = project.Name,
                Message = "Your project has been successfully created!"
            });
        }

        [Route("{id}/Details")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.City)
                .Include(p => p.Cause)
                .Include(p => p.Manager)
                .Include(p => p.Proposal)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (project == null)
            {
                return NotFound();
            }
            
            return View(project);
        }

        [AllowAnonymous]
        [Route("{id}/[action]")]
        public async Task<JsonResult> GetDetails(long? id)
        {
            if (id == null || _context.Projects == null)
            {
                return new JsonResult("Not found");
            }

            var project = await _context.Projects
                .Include(p => p.Manager)
                .Include(p => p.Cause)
                .Include(p => p.City)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (project == null)
            {
                return new JsonResult("Not found");
            }
            
            var model = new ProjectDetailsModalModel
            {
                ProjectId = project.Id,
                Name = project.Name,
                Photo = project.Photo,
                Goal = project.Goal,
                Manager = project.Manager!.FullName,
                Cause = project.Cause!.Name,
                City = project.City!.Name
            };
            return new JsonResult(model);
        }

        [Route("{id}/[action]")]
        public async Task<IActionResult> Positions(long? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Manager)
                .Include(p => p.Positions)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            ViewData["ProjectId"] = project.Id;

            return View(project);
        }

        [Route("{id}/[action]")]
        public async Task<IActionResult> Teams(long? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Manager)
                .Include(p => p.Teams)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }
        
        [Route("{id}/[action]")]
        public async Task<IActionResult> Tasks(long? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Manager)
                .Include(p => p.Teams)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            var actionItems = _context.ActionItems.Where(x => x.ProjectId == project.Id).ToList();
            ViewData["TeamId"] = new SelectList(project.Teams, "Id", "Name");
            var model = new ProjectTasksModel
            {
                Project = project,
                ActionItems = actionItems
            };
            return View(model);
        }
        
        [Route("[action]")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }

    
}