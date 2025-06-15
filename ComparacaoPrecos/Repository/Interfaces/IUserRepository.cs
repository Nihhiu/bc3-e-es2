using ComparacaoPrecos.Models;

namespace ComparacaoPrecos.Repository.Interfaces;

public interface IUserRepository
{
    public Task<List<UserViewModel>> GetAllUsers();
    public Task<bool> DeleteUserAsync(string username);
}