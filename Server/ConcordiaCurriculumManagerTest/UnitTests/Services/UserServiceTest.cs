using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories;
using ConcordiaCurriculumManager.Services;
using ConcordiaCurriculumManagerTest.UnitTests.UtilityFunctions;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Moq;

namespace ConcordiaCurriculumManagerTest.UnitTests.Services;

[TestClass]
public class UserServiceTest
{
    private Mock<IUserRepository> userRepositoryMock = null!;
    private Mock<IEmailService> emailServiceMock = null!;
    private Mock<IInputHasherService> inputHasher = null!;
    private UserService userService = null!;

    [TestInitialize]
    public void Initialize()
    {
        userRepositoryMock = new Mock<IUserRepository>();
        emailServiceMock = new Mock<IEmailService>();
        inputHasher = new Mock<IInputHasherService>();
        userService = new UserService(userRepositoryMock.Object, emailServiceMock.Object, inputHasher.Object);
    }

    [TestMethod]
    public async Task GetAllUsersPageableAsync_ShouldCallGetAllUsersPageableMethodOfRepository_WhenValidIdProvided()
    {
        Guid validId = Guid.NewGuid();

        await userService.GetAllUsersPageableAsync(validId);

        userRepositoryMock.Verify(repo => repo.GetAllUsersPageable(validId), Times.Once);
    }

    [TestMethod]
    public async Task GetUserLikeEmailPageableAsync_ShouldCallGetUsersLikeEmailMethodOfRepository_WhenValidIdAndEmailProvided()
    {
        Guid validId = Guid.NewGuid();
        string validEmail = "test@example.com";

        await userService.GetUserLikeEmailAsync(validId, validEmail);
        userRepositoryMock.Verify(repo => repo.GetUsersLikeEmailPageable(validId, validEmail), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public async Task SendResetPasswordEmail_ThrowsNotFoundException()
    {
        var reset = TestData.GetSampleEmailPasswordResetDTO();
        userRepositoryMock.Setup(repo => repo.GetUserByEmail(It.IsAny<string>())).Throws(new NotFoundException());

        await userService.SendResetPasswordEmail(reset);
    }

    [TestMethod]
    public async Task ResetPassword_Succeeds()
    {
        var newPassword = TestData.GetSamplePasswordResetDTO();
        var token = Guid.NewGuid();
        var user = TestData.GetSampleUser();
        userRepositoryMock.Setup(repo => repo.GetUserByResetPasswordToken(token)).ReturnsAsync(user);
        userRepositoryMock.Setup(repo => repo.UpdateUser(user)).ReturnsAsync(true);

        var result = await userService.ResetPassword(newPassword, token);
        Assert.IsTrue(result);
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public async Task ResetPassword_ThrowsNotFoundException()
    {
        var newPassword = TestData.GetSamplePasswordResetDTO();
        var token = Guid.NewGuid();
        userRepositoryMock.Setup(repo => repo.GetUserByResetPasswordToken(It.IsAny<Guid>())).Throws(new NotFoundException());

        await userService.ResetPassword(newPassword, token);
    }
}
