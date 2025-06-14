using ComparacaoPrecos.Data;
using ComparacaoPrecos.Repository;
using ComparacaoPrecos.Models;
using ComparacaoPrecos.Repository.Interfaces;

namespace ComparacaoPrecos.Service;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserViewModel>> GetAllUsers(){
        return await _userRepository.GetAllUsers();
    }
}