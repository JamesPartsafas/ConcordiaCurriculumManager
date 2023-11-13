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
}
