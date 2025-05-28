using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ComparacaoPrecos.Models;

namespace ComparacaoPrecos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
}