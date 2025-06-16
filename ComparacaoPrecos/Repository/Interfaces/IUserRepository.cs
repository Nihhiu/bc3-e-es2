using ComparacaoPrecos.Models;

namespace ComparacaoPrecos.Repository.Interfaces;

public interface IUserRepository
{
    public Task<List<UserViewModel>> GetAllUsers();
    public Task<UserViewModel?> GetUserByUsername(string username);
    public Task<bool> DeleteUserAsync(string username);
    public Task<bool> UpdateUserAsync(UserViewModel userViewModel);
    public Task<string> GetUserIdByUsername(string username);
}