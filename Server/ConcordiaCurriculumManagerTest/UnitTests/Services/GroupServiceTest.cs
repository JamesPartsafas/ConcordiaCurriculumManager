using ConcordiaCurriculumManager.DTO;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories;
using ConcordiaCurriculumManager.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordiaCurriculumManagerTest.UnitTests.Services;

[TestClass]
public class GroupServiceTest
{
    private Mock<IGroupRepository> groupRepository = null!;
    private GroupService groupService = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        groupRepository = new Mock<IGroupRepository>();

        groupService = new GroupService(groupRepository.Object);
    }

    [TestMethod]
    public async Task VerifyGroupIds_WithValidIds_ReturnsTrue()
    {
        var validId1 = Guid.NewGuid();
        var validId2 = Guid.NewGuid();
        var validId3 = Guid.NewGuid();
        var validIds = new List<Guid> { validId1, validId2, validId3};

        groupRepository.Setup(gr => gr.GetValidGroupIds()).ReturnsAsync(validIds);

        var areValid = await groupService.IsGroupIdListValid(new List<Guid> { validId1, validId2 });

        Assert.IsTrue(areValid);
    }

    [TestMethod]
    public async Task VerifyGroupIds_WithInvalidIds_ReturnsFalse()
    {
        var validId1 = Guid.NewGuid();
        var validId2 = Guid.NewGuid();
        var validId3 = Guid.NewGuid();
        var validIds = new List<Guid> { validId1, validId2, validId3 };

        groupRepository.Setup(gr => gr.GetValidGroupIds()).ReturnsAsync(validIds);

        var areValid = await groupService.IsGroupIdListValid(new List<Guid> { validId1, Guid.NewGuid() });

        Assert.IsFalse(areValid);
    }

    [TestMethod]
    public async Task UpdateGroupAsync_WithValidData_ShouldReturnTrue()
    {
        var groupId = Guid.NewGuid();
        var mockGroup = new Group { Id = groupId, Name = "Original Name" };
        var groupDto = new GroupCreateDTO { Name = "Updated Name" };
        groupRepository.Setup(repo => repo.GetGroupById(groupId)).ReturnsAsync(mockGroup);
        groupRepository.Setup(repo => repo.UpdateGroupAsync(It.IsAny<Group>())).ReturnsAsync(true);

        var result = await groupService.UpdateGroupAsync(groupId, groupDto);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task DeleteGroupAsync_ExistingGroupId_ReturnsTrue()
    {
        var groupId = Guid.NewGuid();
        groupRepository.Setup(repo => repo.DeleteGroupAsync(groupId)).ReturnsAsync(true);

        var result = await groupService.DeleteGroupAsync(groupId);

        Assert.IsTrue(result);
    }
}
