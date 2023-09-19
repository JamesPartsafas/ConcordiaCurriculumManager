using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories;
using ConcordiaCurriculumManager.Security;
using ConcordiaCurriculumManager.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ConcordiaCurriculumManager.Services;

public interface IUserAuthenticationService
{
    bool IsBlacklistedToken(string accessToken);
    Task<string> CreateUserAsync(User user);
    Task<string> SigninUser(string email, string password);
    Task SignoutUser();
    Task<User> GetCurrentUser();
}

public class UserAuthenticationService : IUserAuthenticationService
{
    private readonly ILogger<UserAuthenticationService> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IdentitySettings _identitySettings;
    private readonly IInputHasherService _inputHasher;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICacheService<string> _cacheService;

    public UserAuthenticationService(ILogger<UserAuthenticationService> logger,
                                IUserRepository userRepository,
                                IOptions<IdentitySettings> options,
                                IInputHasherService inputHasher,
                                ICacheService<string> cacheService,
                                IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _userRepository = userRepository;
        _inputHasher = inputHasher;
        _cacheService = cacheService;
        _identitySettings = options.Value ?? throw new ArgumentNullException("Identity Settings is null");
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> SigninUser(string email, string password)
    {
        var savedUser = await _userRepository.GetUserByEmail(email);

        if (savedUser is null || !_inputHasher.Verify(password, savedUser.Password))
        {
            throw new ArgumentException("Email and/or password are incorrect");
        }

        return GenerateAccessToken(savedUser);
    }

    public async Task<string> CreateUserAsync(User user)
    {
        var exists = await _userRepository.GetUserByEmail(user.Email) is not null;

        if (exists)
        {
            throw new ArgumentException("The user already exists");
        }

        var hashedPassword = _inputHasher.Hash(user.Password);
        user.Password = hashedPassword;

        var visitorRole = new Role() { UserRole = RoleEnum.FacultyMember };
        user.Roles.Add(visitorRole);

        var savedUser = await _userRepository.SaveUser(user);

        if (!savedUser)
        {
            _logger.LogWarning("Failed to save a user in the database");
            throw new InvalidOperationException("Could not save user");
        }

        return GenerateAccessToken(user);
    }

    public async Task SignoutUser()
    {
        var httpContext = _httpContextAccessor.HttpContext ?? throw new InvalidOperationException("Method can only be used in http(s) scope");
        var token = await httpContext.GetTokenAsync("access_token") ?? throw new InvalidOperationException("Method can only be used to signout authenticated users");
        _ = _cacheService.GetOrCreate(token, () =>
        {
            var jwtToken = new JwtSecurityToken(token);
            var expiryDate = TimeSpan.FromTicks(jwtToken.ValidTo.Ticks - DateTime.UtcNow.Ticks);
            return (cacheEntry: token,  expiryDate, neverRemove: true);
        });
    }

    public async Task<User> GetCurrentUser()
    {
        if (_httpContextAccessor.HttpContext == null)
        {
            throw new NullReferenceException("HttpContext is null");
        }

        string email = _httpContextAccessor.HttpContext.User.Claims.First(i => i.Type == ClaimTypes.Email).Value;

        User? user = await _userRepository.GetUserByEmail(email);

        if (user == null)
        {
            throw new NullReferenceException($"User with email {email} could not be found");
        }

        return user;
    }

    public bool IsBlacklistedToken(string accessToken) => _cacheService.Exists(accessToken);

    private string GenerateAccessToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_identitySettings.Key));
        var credentials = new SigningCredentials(securityKey, _identitySettings.SecurityAlgorithms);

        var claims = new List<Claim>
        {
            new Claim(Claims.Email, user.Email),
            new Claim(Claims.FirstName, user.FirstName),
            new Claim(Claims.LastName, user.LastName)
        };

        foreach (Role role in user.Roles)
        {
            claims.Add(new Claim(Claims.Roles, role.UserRole.ToString()));
        }

        var time = DateTime.UtcNow - new DateTime(1970, 1, 1);
        var epoch = Convert.ToInt64(time.TotalSeconds);
        claims.Add(new Claim("iat", epoch.ToString(), ClaimValueTypes.Integer64));

        var token = new JwtSecurityToken(
            _identitySettings.Issuer,
            _identitySettings.Audience,
            claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
