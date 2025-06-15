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

}