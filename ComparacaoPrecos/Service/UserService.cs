using ComparacaoPrecos.Data;
using ComparacaoPrecos.Repository;
using ComparacaoPrecos.Models;
using ComparacaoPrecos.Repository.Interfaces;

namespace ComparacaoPrecos.Service;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogsRepository _logsRepository;

    public UserService(IUserRepository userRepository, ILogsRepository logsRepository)
    {
        _logsRepository = logsRepository;
        _userRepository = userRepository;
    }

    public async Task<List<UserViewModel>> GetAllUsers()
    {
        return await _userRepository.GetAllUsers();
    }

    public async Task<UserViewModel?> GetUserByUsername(string username)
    {
        return await _userRepository.GetUserByUsername(username);
    }

    public async Task<bool> DeleteUser(string username)
    {
        return await _userRepository.DeleteUserAsync(username);
    }
    public async Task<bool> UpdateUser(UserViewModel userViewModel)
    {
        return await _userRepository.UpdateUserAsync(userViewModel);
    }

    public async Task<List<Logs>> GetUserLogs(string username)
    {
        var userId = await _userRepository.GetUserIdByUsername(username);
        return await _logsRepository.GetUserLogs(userId);
    }
}