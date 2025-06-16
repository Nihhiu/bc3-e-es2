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

    [HttpGet("registo/{role?}")]
    public async Task<IActionResult> Index(string? role)
    {
        var users = await _userService.GetAllUsers();

        if (!string.IsNullOrEmpty(role))
        {
            users = users.Where(u => u.Role == role).ToList();
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

    [HttpPost("delete/{username}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(string username)
    {
        if (User.Identity == null || string.IsNullOrEmpty(username))
        {
            TempData["ErrorMessage"] = "Utilizador não encontrado.";
            return RedirectToAction(nameof(Index));
        }

        if (User.IsInRole("Admin") == false)
        {
            TempData["ErrorMessage"] = "Apenas utilizadores com o papel de Admin podem eliminar utilizadores.";
            return RedirectToAction(nameof(Index));
        }

        if (User.Identity.Name == username)
        {
            TempData["ErrorMessage"] = "Não pode eliminar o seu próprio utilizador.";
            return RedirectToAction(nameof(Index));
        }

        if (username == "admin")
        {
            TempData["ErrorMessage"] = "Não pode eliminar o utilizador admin.";
            return RedirectToAction(nameof(Index));
        }



        await _userService.DeleteUser(username);
        TempData["SuccessMessage"] = "Utilizador eliminado com sucesso.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("logs/{username}")]
    public async Task<IActionResult> Detalhes(string username)
    {
        if(User.IsInRole("Admin") == false)
        {
            TempData["ErrorMessage"] = "Apenas utilizadores com o papel de Admin podem ver os detalhes dos utilizadores.";
            return RedirectToAction(nameof(Index));
        }
        var user = await _userService.GetUserByUsername(username);
        var logs = await _userService.GetUserLogs(username);
        if (user == null)
        {
            TempData["ErrorMessage"] = "Utilizador não encontrado.";
            return RedirectToAction(nameof(Index));
        }
        ViewData["Logs"] = logs;
        return View(user);
    }
}