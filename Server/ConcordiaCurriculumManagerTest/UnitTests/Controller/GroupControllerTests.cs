using ConcordiaCurriculumManager.Controllers;
using ConcordiaCurriculumManager.Services;
using ConcordiaCurriculumManager.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using ConcordiaCurriculumManager.DTO;

namespace ConcordiaCurriculumManagerTest.UnitTests.Controllers
{
    [TestClass]
    public class GroupControllerTests
    {
        private Mock<IGroupService> _groupService = null!;
        private Mock<IUserAuthenticationService> _userService = null!;
        private Mock<ILogger<GroupController>> _logger = null!;
        private GroupController _groupController = null!;

        [TestInitialize]
        public void TestInitialize()
        {
            _logger = new Mock<ILogger<GroupController>>();
            _groupService = new Mock<IGroupService>();
            _userService = new Mock<IUserAuthenticationService>();

            _groupController = new GroupController(_groupService.Object, _userService.Object, _logger.Object);
        }

        [TestMethod]
        public async Task CreateGroup_ValidRequest_CreatesGroup()
        {
            var groupCreateDTO = new GroupCreateDTO { Name = "Test Group" };
            var createdGroup = new Group { Id = Guid.NewGuid(), Name = groupCreateDTO.Name };

            _groupService.Setup(x => x.CreateGroupAsync(It.IsAny<Group>()))
                         .ReturnsAsync(true)
                         .Callback<Group>((x) => x.Id = createdGroup.Id); // Simulate setting the ID upon creation

            var result = await _groupController.CreateGroup(groupCreateDTO);

            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
            var createdResult = (CreatedAtActionResult)result;
            Assert.AreEqual((int)HttpStatusCode.Created, createdResult.StatusCode);
            Assert.AreEqual(createdGroup.Id, (createdResult.Value as Group)!.Id);
        }

        [TestMethod]
        public async Task CreateGroup_Failure_Returns500()
        {
            var groupCreateDTO = new GroupCreateDTO { Name = "Test Group" };

            _groupService.Setup(x => x.CreateGroupAsync(It.IsAny<ConcordiaCurriculumManager.Models.Users.Group>())).ReturnsAsync(false);

            var result = await _groupController.CreateGroup(groupCreateDTO);

            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            var objectResult = (ObjectResult)result;
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task GetGroupById_ValidId_ReturnsGroup()
        {
            var groupId = Guid.NewGuid();
            var expectedGroup = new GroupDTO { Id = groupId, Name = "Test Group", Members = new List<UserDTO> { }, GroupMasters = new List<UserDTO> { } };

            _groupService.Setup(service => service.GetGroupByIdAsync(It.IsAny<Guid>()))
                         .ReturnsAsync(expectedGroup);

            var result = await _groupController.GetGroupById(groupId);

            Assert.IsNotNull(result);

            var objectResult = result as OkObjectResult;

            Assert.AreEqual((int)HttpStatusCode.OK, objectResult!.StatusCode);
            Assert.AreEqual(expectedGroup, objectResult.Value);
        }

        [TestMethod]
        public async Task GetGroupById_NotFound_Returns404()
        {
            _groupService.Setup(x => x.GetGroupByIdAsync(It.IsAny<Guid>())).ReturnsAsync((GroupDTO)null!);

            var result = await _groupController.GetGroupById(Guid.NewGuid());

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}