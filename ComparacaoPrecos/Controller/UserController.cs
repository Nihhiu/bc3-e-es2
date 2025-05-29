using Microsoft.AspNetCore.Mvc;
using ComparacaoPrecos.Service;
using ComparacaoPrecos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ComparacaoPrecos.Controllers;

[Route("user")]
[Authorize(Roles = "Admin")]
public class UserController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly UserService _userService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserController(UserService userService,
                          UserManager<ApplicationUser> userManager,
                          RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _userService = userService;
    }

    [HttpGet("registo/{id?}")]
    public async Task<IActionResult> Index(string? id)
    {
        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

        var users = await _userService.GetAllUsers();

        if (!string.IsNullOrEmpty(id))
        {
            users = users.Where(u => u.Role == id).ToList();
        }

        return View(users);
    }

    [HttpGet("registo/criar")]
    public IActionResult Create()
    {
        var roles = _roleManager.Roles
            .Select(r => new SelectListItem 
            {
                Text = r.Name,
                Value = r.Name
            })
            .ToList();
        
        ViewData["Roles"] = roles; // Agora é lista de SelectListItem
        return View(new UserViewModel());
    }
    
    [HttpPost("registo/criar")]
    public async Task<IActionResult> Create(UserViewModel model)
    {
        
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.Role);
                
                return RedirectToAction("Index", "User");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }
        var roles = _roleManager.Roles.Select(r => r.Name).ToList();
        ViewData["Roles"] = new SelectList(roles);

        return View(model);
    }
}