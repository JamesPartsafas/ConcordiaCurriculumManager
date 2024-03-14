using ConcordiaCurriculumManager.Repositories;
using ConcordiaCurriculumManager.Services;
using Moq;

namespace ConcordiaCurriculumManagerTest.UnitTests.Services;

[TestClass]
public class UserServiceTest
{
    private Mock<IUserRepository> userRepositoryMock = null!;
    private Mock<IEmailService> userServiceMock = null!;
    private UserService userService = null!;

    [TestInitialize]
    public void Initialize()
    {
        userRepositoryMock = new Mock<IUserRepository>();
        userServiceMock = new Mock<IEmailService>();
        userService = new UserService(userRepositoryMock.Object, userServiceMock.Object);
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
}
