using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cyvil.Mvc.Data;
using Cyvil.Mvc.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cyvil.Mvc.Controllers
{
    [Authorize(Roles = "Member")]
    [Route("[controller]")]
    public class MembersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MembersController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public MembersController(ILogger<MembersController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        [Route("[action]")]
        public async Task<IActionResult> Dashboard()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return View(user);
        }

        [Route("{id}/Projects")]
        public async Task<IActionResult> Projects(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(x => x.Projects)
                .FirstOrDefaultAsync(x => x.Id == id);
            
            if (user == null)
            {
                return NotFound();
            }

            ViewData["CauseId"] = new SelectList(_context.Causes, "Id", "Name");
            ViewData["States"] = new SelectList(_context.States, "Id", "Name");

            return View(user);
        }

        [Route("{id}/Events")]
        public async Task<IActionResult> Events(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(x => x.Meetings)
                .Include(x => x.Events)
                .FirstOrDefaultAsync(x => x.Id == id);
            
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}