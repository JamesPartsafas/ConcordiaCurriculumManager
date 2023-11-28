using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories;
using ConcordiaCurriculumManager.Security;
using ConcordiaCurriculumManager.Services;
using ConcordiaCurriculumManager.Settings;
using ConcordiaCurriculumManagerTest.UnitTests.UtilityFunctions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ConcordiaCurriculumManagerTest.UnitTests.Services;

[TestClass]
public class UserAuthenticationServiceTests
{
    private Mock<IUserRepository> userRepository = null!;
    private Mock<IInputHasherService> inputHasher = null!;
    private Mock<ILogger<UserAuthenticationService>> logger = null!;
    private Mock<ICacheService<string>> cacheService = null!;
    private Mock<IHttpContextAccessor> httpContextAccessor = null!;
    private IOptions<IdentitySettings> options = null!;
    private UserAuthenticationService userService = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        userRepository = new Mock<IUserRepository>();
        inputHasher = new Mock<IInputHasherService>();
        logger = new Mock<ILogger<UserAuthenticationService>>();
        cacheService = new Mock<ICacheService<string>>();
        httpContextAccessor = new Mock<IHttpContextAccessor>();

        var identitySettings = new IdentitySettings()
        {
            Audience = "testSuite",
            Key = "This is a the security key for testing",
            Issuer = "testSuite",
            SecurityAlgorithms = "HS256"
        };

        options = Options.Create(identitySettings);

        userService = new UserAuthenticationService(
            logger.Object,
            userRepository.Object,
            options,
            inputHasher.Object,
            cacheService.Object,
            httpContextAccessor.Object);
    }

    [TestMethod]
    public async Task SigninUser_ValidUser_ReturnsAccessToken()
    {
        var userEmail = "test@example.com";
        var userPassword = "password";
        var hashedPassword = "hashedpassword";

        var user = new User
        {
            FirstName = "fname",
            LastName = "lname",
            Email = userEmail,
            Password = hashedPassword
        };

        userRepository.Setup(repo => repo.GetUserByEmail(userEmail))
            .ReturnsAsync(user);

        inputHasher.Setup(hasher => hasher.Verify(userPassword, hashedPassword))
            .Returns(true);

        var accessToken = await userService.SigninUser(userEmail, userPassword);

        Assert.IsNotNull(accessToken);
    }

    [TestMethod]
    public async Task SigninUser_ValidUserWithGroupMemberAndGroupMaster_ReturnsAccessTokenWithGroupAndGroupMasterClaims()
    {
        var userEmail = "test@example.com";
        var userPassword = "password";
        var hashedPassword = "hashedpassword";
        var group1 = new Group() { Id = Guid.NewGuid(), Name = "TestGroup1" };
        var group2 = new Group() { Id = Guid.NewGuid(), Name = "TestGroup2" };

        var user = new User
        {
            FirstName = "fname",
            LastName = "lname",
            Email = userEmail,
            Password = hashedPassword,
            Groups = new List<Group>() { group1, group2 },
            MasteredGroups = new List<Group> { group1 }
        };

        userRepository.Setup(repo => repo.GetUserByEmail(userEmail))
            .ReturnsAsync(user);

        inputHasher.Setup(hasher => hasher.Verify(userPassword, hashedPassword))
            .Returns(true);

        var accessToken = await userService.SigninUser(userEmail, userPassword);
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(accessToken);
        var groupClaims = jwtSecurityToken.Claims.Where(c => c.Type == Claims.Group).ToList();
        var groupMasterClaims = jwtSecurityToken.Claims.Where(c => c.Type == Claims.GroupMaster).ToList();

        Assert.IsNotNull(accessToken);
        Assert.IsNotNull(groupClaims);
        Assert.IsNotNull(groupMasterClaims);

        for (int i = 0; i < user.Groups.Count; i++)
        {
            Assert.AreEqual(user.Groups[i].Id.ToString(), groupClaims[i].Value);
        }

        for (int i = 0; i < user.MasteredGroups.Count; i++)
        {
            Assert.AreEqual(user.MasteredGroups[i].Id.ToString(), groupMasterClaims[i].Value);
        }
    }

    [TestMethod]
    public async Task SigninUser_ValidUserNotInAnyGroup_ReturnsAccessTokenWithoutGroupAndGroupMasterClaims()
    {
        var userEmail = "test@example.com";
        var userPassword = "password";
        var hashedPassword = "hashedpassword";

        var user = new User
        {
            FirstName = "fname",
            LastName = "lname",
            Email = userEmail,
            Password = hashedPassword
        };

        userRepository.Setup(repo => repo.GetUserByEmail(userEmail))
            .ReturnsAsync(user);

        inputHasher.Setup(hasher => hasher.Verify(userPassword, hashedPassword))
            .Returns(true);

        var accessToken = await userService.SigninUser(userEmail, userPassword);
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(accessToken);
        var groupClaims = jwtSecurityToken.Claims.Where(c => c.Type == Claims.Group).ToList();
        var groupMasterClaims = jwtSecurityToken.Claims.Where(c => c.Type == Claims.GroupMaster).ToList();

        Assert.IsNotNull(accessToken);
        Assert.AreEqual(0, groupClaims.Count);
        Assert.AreEqual(0, groupMasterClaims.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(BadRequestException))]
    public async Task SigninUser_InvalidUser_ThrowsArgumentException()
    {
        var userEmail = "test@example.com";
        var userPassword = "password";
        User? result = null;

        userRepository.Setup(repo => repo.GetUserByEmail(userEmail))
            .ReturnsAsync(result);

        await userService.SigninUser(userEmail, userPassword);
    }

    [TestMethod]
    public async Task CreateUserAsync_NewUser_ReturnsAccessToken()
    {
        var newUser = new User
        {
            FirstName = "fname",
            LastName = "lname",
            Email = "test@example.com",
            Password = "password"
        };

        userRepository.Setup(repo => repo.GetUserByEmail(newUser.Email))
            .ReturnsAsync((User?)null);

        inputHasher.Setup(hasher => hasher.Hash(newUser.Password))
            .Returns(newUser.Password);

        userRepository.Setup(repo => repo.SaveUser(newUser))
            .ReturnsAsync(true);

        var accessToken = await userService.CreateUserAsync(newUser);

        Assert.IsNotNull(accessToken);
    }

    [TestMethod]
    [ExpectedException(typeof(BadRequestException))]
    public async Task CreateUserAsync_ExistingUser_ThrowsArgumentException()
    {
        var existingUser = new User
        {
            FirstName = "fname",
            LastName = "lname",
            Email = "existinguser@example.com",
            Password = "existingpassword"
        };

        userRepository.Setup(repo => repo.GetUserByEmail(existingUser.Email))
            .ReturnsAsync(existingUser);

        await userService.CreateUserAsync(existingUser);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public async Task CreateUserAsync_NewUser_ThrowsInvalidOperationException()
    {
        var newUser = new User
        {
            FirstName = "fname",
            LastName = "lname",
            Email = "test@example.com",
            Password = "password"
        };

        userRepository.Setup(repo => repo.GetUserByEmail(newUser.Email))
            .ReturnsAsync((User?)null);

        inputHasher.Setup(hasher => hasher.Hash(newUser.Password))
            .Returns(newUser.Password);

        userRepository.Setup(repo => repo.SaveUser(newUser))
            .ReturnsAsync(false);

        await userService.CreateUserAsync(newUser);
    }

    [TestMethod]
    public async Task SignoutUser_ValidToken_CallsCacheService()
    {
        var token = "valid_token";

        var claims = new Claim[]
        {
            new Claim("email", "test@example.com")
        };

        var jwtToken = new JwtSecurityToken(claims: claims);

        var httpContextAccessor = new Mock<IHttpContextAccessor>();
        HttpContextUtil.MockHttpContextGetToken(httpContextAccessor, token);
        var newUserService = new UserAuthenticationService(
            logger.Object,
            userRepository.Object,
            options,
            inputHasher.Object,
            cacheService.Object,
            httpContextAccessor.Object
        );


        cacheService.Setup(cs => cs.GetOrCreate(It.IsAny<string>(), It.IsAny<Func<(string, TimeSpan, bool)>>()))
            .Returns(token);

        await newUserService.SignoutUser();

        cacheService.Verify(cs => cs.GetOrCreate(token, It.IsAny<Func<(string, TimeSpan, bool)>>()), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(BadRequestException))]
    public async Task SignoutUser_InvalidHttpContext_ThrowsInvalidOperationException()
    {
        httpContextAccessor.Setup(a => a.HttpContext)
            .Returns((HttpContext?)null);

        await userService.SignoutUser();
    }

    [TestMethod]
    [ExpectedException(typeof(BadRequestException))]
    public async Task SignoutUser_NullToken_ThrowsInvalidOperationException()
    {
        var httpContextAccessor = new Mock<IHttpContextAccessor>();
        HttpContextUtil.MockHttpContextGetToken(httpContextAccessor, null!);
        var newUserService = new UserAuthenticationService(
            logger.Object,
            userRepository.Object,
            options,
            inputHasher.Object,
            cacheService.Object,
            httpContextAccessor.Object
        );

        await newUserService.SignoutUser();
    }

    [TestMethod]
    public void IsBlacklistedToken_BlacklistedToken_CallsCacheService()
    {
        var blacklistedToken = "blacklisted_token";
        cacheService.Setup(cs => cs.Exists(blacklistedToken))
            .Returns(true);

        userService.IsBlacklistedToken(blacklistedToken);

        cacheService.Verify(cs => cs.Exists(blacklistedToken), Times.Once);
    }
}