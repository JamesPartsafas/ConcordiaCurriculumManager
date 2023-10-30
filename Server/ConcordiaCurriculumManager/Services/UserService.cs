using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories;

namespace ConcordiaCurriculumManager.Services;

public interface IUserService
{
    Task<IList<User>> GetUserLikeEmailAsync(Guid id, string email);
    Task<IList<User>> GetAllUsersPageableAsync(Guid id);
    Task<User?> GetUserById(Guid id);
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IList<User>> GetAllUsersPageableAsync(Guid id) => await _userRepository.GetAllUsersPageable(id);

    public async Task<IList<User>> GetUserLikeEmailAsync(Guid id, string email) => await _userRepository.GetUsersLikeEmailPageable(id, email);

    public async Task<User?> GetUserById(Guid id) => await _userRepository.GetUserById(id);
}
