using Microsoft.EntityFrameworkCore;
using ComparacaoPrecos.Data;
using ComparacaoPrecos.Models;
using ComparacaoPrecos.Repository.Interfaces;

namespace ComparacaoPrecos.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserViewModel>> GetAllUsers()
    {
        var usersWithRoles = await _context.Users
            .Select(user => new UserViewModel
            {
                Username = user.UserName,
                Role = _context.UserRoles
                    .Where(ur => ur.UserId == user.Id)
                    .Join(
                        _context.Roles,
                        userRole => userRole.RoleId,
                        role => role.Id,
                        (userRole, role) => role.Name
                    )
                    .FirstOrDefault() ?? string.Empty
            })
            .ToListAsync();

        return usersWithRoles;
    }

    public async Task<bool> DeleteUserAsync(string username)
    {
        // Busca o usuário pelo username (campo não-chave)
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.UserName == username);

        if (user == null)
            return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return true;
    }
    public async Task<bool> UpdateUserAsync(UserViewModel userViewModel)
    {
        var user = await _userManager.FindByNameAsync(userViewModel.Username);

        if (user == null) return false;

        var currentRoles = await _userManager.GetRolesAsync(user);

        if (!string.IsNullOrEmpty(userViewModel.Role) && !currentRoles.Contains(userViewModel.Role))
        {
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
            {
                return false;
            }
            var addResult = await _userManager.AddToRoleAsync(user, userViewModel.Role);
            if (!addResult.Succeeded)
            {
                return false;
            }
        }
        return true; 
    }
}