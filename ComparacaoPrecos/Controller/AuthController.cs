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

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email }; // Correto
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            // Atribuir a role "User"
            await _userManager.AddToRoleAsync(user, "User");
            return Ok("Usuário registrado com sucesso!");
        }

        return BadRequest(result.Errors);
    }
}