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

    [HttpGet("registo")]
    public async Task<IActionResult> Index()
    {
        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

        if (role != "Admin")
        {
            return RedirectToAction("Index", "Home");
        }

        var user = await _userService.GetAllUsers();
        return View(user);
    }
}