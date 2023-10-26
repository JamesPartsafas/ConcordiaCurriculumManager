using AutoMapper;
using ConcordiaCurriculumManager.Controllers;
using ConcordiaCurriculumManager.DTO;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ConcordiaCurriculumManagerTest.UnitTests.Controller;

[TestClass]
public class UsersControllerTest
{
    private Mock<IUserService> userService = null!;
    private UsersController usersController = null!;
    private Mock<IMapper> mapper = null!;


    [TestInitialize]
    public void TestInitialize()
    {
        userService = new Mock<IUserService>();
        mapper = new Mock<IMapper>();

        usersController = new UsersController(mapper.Object, userService.Object);
    }

    [TestMethod]
    public async Task GetAllUsersAsync_WithNullGuid_CallsUserServiceWithEmptyGuid()
    {
        Guid expectedGuid = Guid.Empty;
        userService.Setup(service => service.GetAllUsersPageableAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new List<User>()); 

        mapper.Setup(mapper => mapper.Map<List<UserDTO>>(It.IsAny<List<User>>()))
            .Returns(new List<UserDTO>());

        var result = await usersController.GetAllUsersAsync(null) as OkObjectResult;

        userService.Verify(service => service.GetAllUsersPageableAsync(expectedGuid), Times.Once);
        Assert.IsInstanceOfType(result?.Value, typeof(List<UserDTO>));
    }

    [TestMethod]
    public async Task GetAllUsersAsync_WithGuid_CallsUserServiceWithProvidedGuid()
    {
        Guid expectedGuid = Guid.NewGuid();
        userService.Setup(service => service.GetAllUsersPageableAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new List<User>());

        mapper.Setup(mapper => mapper.Map<List<UserDTO>>(It.IsAny<List<User>>()))
            .Returns(new List<UserDTO>());

        var result = await usersController.GetAllUsersAsync(expectedGuid) as OkObjectResult;

        userService.Verify(service => service.GetAllUsersPageableAsync(expectedGuid), Times.Once);
        Assert.IsInstanceOfType(result?.Value, typeof(List<UserDTO>));
    }

    [TestMethod]
    public async Task SearchUsersByEmail_WithNullEmail_ReturnsBadRequestResult()
    {
        var result = await usersController.SearchUsersByEmail(null, null!);
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task SearchUsersByEmail_WithNullGuid_CallsUserServiceWithEmptyGuid()
    {
        Guid expectedGuid = Guid.Empty;
        userService.Setup(service => service.GetUserLikeEmailAsync(It.IsAny<Guid>(), It.IsAny<string>()))
            .ReturnsAsync(new List<User>()); 

        mapper.Setup(mapper => mapper.Map<List<UserDTO>>(It.IsAny<List<User>>()))
            .Returns(new List<UserDTO>());

        var result = await usersController.SearchUsersByEmail(null, "test@example.com") as OkObjectResult;

        userService.Verify(service => service.GetUserLikeEmailAsync(expectedGuid, It.IsAny<string>()), Times.Once);
        Assert.IsInstanceOfType(result?.Value, typeof(List<UserDTO>));
    }

    [TestMethod]
    public async Task SearchUsersByEmail_WithValidGuid_CallsUserServiceWithValidGuid()
    {
        Guid validGuid = Guid.NewGuid();
        userService.Setup(service => service.GetUserLikeEmailAsync(It.IsAny<Guid>(), It.IsAny<string>()))
            .ReturnsAsync(new List<User>()); 

        mapper.Setup(mapper => mapper.Map<List<UserDTO>>(It.IsAny<List<User>>()))
            .Returns(new List<UserDTO>());

        var result = await usersController.SearchUsersByEmail(validGuid, "test@example.com") as OkObjectResult;

        userService.Verify(service => service.GetUserLikeEmailAsync(validGuid, It.IsAny<string>()), Times.Once);
        Assert.IsInstanceOfType(result?.Value, typeof(List<UserDTO>));
    }
}
