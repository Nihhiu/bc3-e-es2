using Microsoft.AspNetCore.Mvc;
using ComparacaoPrecos.Service;
using ComparacaoPrecos.Models;

namespace ComparacaoPrecos.Controllers;

[Route("user")]
public class UserController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet("registo/Role/{id?}")]
    public async Task<IActionResult> Index(string id)
    {
        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

        if (role != "Admin")
        {
            return RedirectToAction("Index", "Home");
        }

        var users = await _userService.GetAllUsers();

        if (!string.IsNullOrEmpty(id))
        {
            users = users.Where(u => u.Role == id).ToList();
        }

        return View(users);
    }
}