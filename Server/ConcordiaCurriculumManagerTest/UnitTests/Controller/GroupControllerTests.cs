using ConcordiaCurriculumManager.Controllers;
using ConcordiaCurriculumManager.Services;
using ConcordiaCurriculumManager.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using ConcordiaCurriculumManager.DTO;
using AutoMapper;
using ConcordiaCurriculumManager.Filters.Exceptions;

namespace ConcordiaCurriculumManagerTest.UnitTests.Controllers
{
    [TestClass]
    public class GroupControllerTests
    {
        private Mock<IGroupService> _groupService = null!;
        private Mock<IUserAuthenticationService> _userService = null!;
        private Mock<ILogger<GroupController>> _logger = null!;
        private GroupController _groupController = null!;
        private Mock<IMapper> _mapper = null!;

        [TestInitialize]
        public void TestInitialize()
        {
            _logger = new Mock<ILogger<GroupController>>();
            _groupService = new Mock<IGroupService>();
            _userService = new Mock<IUserAuthenticationService>();
            _mapper = new Mock<IMapper>();

            _groupController = new GroupController(_groupService.Object, _userService.Object, _logger.Object, _mapper.Object);
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
        [ExpectedException(typeof(Exception))]
        public async Task CreateGroup_Failure_ThrowsException()
        {
            var groupCreateDTO = new GroupCreateDTO { Name = "Test Group" };

            _groupService.Setup(x => x.CreateGroupAsync(It.IsAny<Group>())).ReturnsAsync(false);

            await _groupController.CreateGroup(groupCreateDTO);
        }

        [TestMethod]
        public async Task GetGroupById_ValidId_ReturnsGroup()
        {
            var groupId = Guid.NewGuid();
            var expectedGroup = new Group { Id = groupId, Name = "Test Group", Members = new List<User> { }, GroupMasters = new List<User> { } };

            _groupService.Setup(service => service.GetGroupByIdAsync(It.IsAny<Guid>()))
                         .ReturnsAsync(expectedGroup);

            var result = await _groupController.GetGroupById(groupId);
            var objectResult = result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(objectResult);
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult!.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task GetGroupById_NotFound_ThrowsExceptionhrowsException()
        {
            _groupService.Setup(x => x.GetGroupByIdAsync(It.IsAny<Guid>())).Throws(new NotFoundException());

            await _groupController.GetGroupById(Guid.NewGuid());
        }

        [TestMethod]
        public async Task UpdateGroup_WithValidData_ReturnsOk()
        {
            var groupId = Guid.NewGuid();
            var groupDto = new GroupCreateDTO { Name = "Updated Name" };
            _groupService.Setup(s => s.UpdateGroupAsync(groupId, groupDto))
                        .ReturnsAsync(true);

            var result = await _groupController.UpdateGroup(groupId, groupDto);

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task UpdateGroup_WhenUpdateFails_ReturnsNotFound()
        {
            var groupId = Guid.NewGuid();
            var groupDto = new GroupCreateDTO { Name = "Updated Name" };
            _groupService.Setup(s => s.UpdateGroupAsync(groupId, groupDto))
                        .ReturnsAsync(false);

            var result = await _groupController.UpdateGroup(groupId, groupDto);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteGroup_ReturnOk_WhenDeletionIsSuccessful()
        {
            var groupId = Guid.NewGuid();
            _groupService.Setup(s => s.DeleteGroupAsync(groupId)).ReturnsAsync(true);

            var result = await _groupController.DeleteGroup(groupId);

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task DeleteGroup_ReturnsNotFound_WhenDeletionFails()
        {
            var groupId = Guid.NewGuid();
            _groupService.Setup(s => s.DeleteGroupAsync(groupId)).ReturnsAsync(false);

            var result = await _groupController.DeleteGroup(groupId);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}