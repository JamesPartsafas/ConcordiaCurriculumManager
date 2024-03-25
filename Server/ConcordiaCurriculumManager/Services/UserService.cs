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
    Task<IList<User>> GetUsersByFirstName(string firstName);
    Task<IList<User>> GetUsersByLastName(string lastName);
    Task<bool> SendResetPasswordEmail(EmailPasswordResetDTO reset);
    Task<bool> ResetPassword(PasswordResetDTO password, Guid token);
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    private readonly IInputHasherService _inputHasher;

    public UserService(IUserRepository userRepository, IEmailService emailService, IInputHasherService inputHasher)
    {
        _userRepository = userRepository;
        _emailService = emailService;
        _inputHasher = inputHasher;
    }

    public async Task<IList<User>> GetAllUsersPageableAsync(Guid id) => await _userRepository.GetAllUsersPageable(id);

    public async Task<IList<User>> GetUserLikeEmailAsync(Guid id, string email) => await _userRepository.GetUsersLikeEmailPageable(id, email);

    public async Task<User?> GetUserById(Guid id) => await _userRepository.GetUserById(id);

    public async Task<IList<User>> GetUsersByFirstName(string firstName) => await _userRepository.GetUsersByFirstName(firstName);
    public async Task<IList<User>> GetUsersByLastName(string lastName) => await _userRepository.GetUsersByLastName(lastName);
    public async Task<bool> SendResetPasswordEmail(EmailPasswordResetDTO reset) {
        var _ = await _userRepository.GetUserByEmail(reset.Email) ?? throw new NotFoundException("The email " + reset.Email + " does not exist.");
        Guid token = Guid.NewGuid();
        await _userRepository.SavePasswordResetToken(token, reset.Email);

        string resetLink = "http://localhost:4173/resetPassword" + "?token=" + token;
        string subject = $"Reset Password";
        string body = "Click the following link to reset your password: " + resetLink;

        return await _emailService.SendEmail(reset.Email, subject, body);

    }
    public async Task<bool> ResetPassword(PasswordResetDTO password, Guid token)
    {
        var user = await _userRepository.GetUserByResetPasswordToken(token) ?? throw new NotFoundException("A user with the reset password token " + token + " does not exist.");
        var newPassword = password.Password;
        var hashedPassword = _inputHasher.Hash(newPassword);

        user.Password = hashedPassword;
        user.ResetPasswordToken = null;

        return await _userRepository.UpdateUser(user); ;

    }
}
