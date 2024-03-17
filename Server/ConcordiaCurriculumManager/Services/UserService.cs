using ConcordiaCurriculumManager.DTO;
using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories;
using RTools_NTS.Util;
using System.Text.Json;

namespace ConcordiaCurriculumManager.Services;

public interface IUserService
{
    Task<IList<User>> GetUserLikeEmailAsync(Guid id, string email);
    Task<IList<User>> GetAllUsersPageableAsync(Guid id);
    Task<User?> GetUserById(Guid id);
    Task<bool> SendResetPasswordEmail(PasswordResetDTO reset);
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public UserService(IUserRepository userRepository, IEmailService emailService)
    {
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task<IList<User>> GetAllUsersPageableAsync(Guid id) => await _userRepository.GetAllUsersPageable(id);

    public async Task<IList<User>> GetUserLikeEmailAsync(Guid id, string email) => await _userRepository.GetUsersLikeEmailPageable(id, email);

    public async Task<User?> GetUserById(Guid id) => await _userRepository.GetUserById(id);

    public async Task<bool> SendResetPasswordEmail(PasswordResetDTO reset) {
        var _ = await _userRepository.GetUserByEmail(reset.Email) ?? throw new NotFoundException("The email " + reset.Email + " does not exist.");
        Guid token = Guid.NewGuid();
        await _userRepository.SavePasswordResetToken(token, reset.Email);

        string resetLink = "http://localhost:4173/resetPassword" + "?token=" + token;
        string subject = $"Reset Password";
        string body = "Click the following link to reset your password: " + resetLink;

        return await _emailService.SendEmail(reset.Email, subject, body);

    }
}
