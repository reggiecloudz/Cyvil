using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Cyvil.Mvc.Models;
using Microsoft.AspNetCore.Identity;
using Cyvil.Mvc.Domain;
using System.Security.Claims;

namespace Cyvil.Mvc.Controllers;

[Route("[controller]")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SignInManager<ApplicationUser> _signInManager;

    private readonly UserManager<ApplicationUser> _userManager;

    public HomeController(ILogger<HomeController> logger, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [Route("~/")]
    public async Task<IActionResult> Index()
    {
        
        if (_signInManager.IsSignedIn(HttpContext.User))
        {
            var user = await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            
            if (await _userManager.IsInRoleAsync(user, "Member"))
            {
                return RedirectToAction(nameof(MembersController.Dashboard), "Members");
            }
            else
            {
                return View();
            }
        }
        return View();
    }

    [Route("[action]")]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
