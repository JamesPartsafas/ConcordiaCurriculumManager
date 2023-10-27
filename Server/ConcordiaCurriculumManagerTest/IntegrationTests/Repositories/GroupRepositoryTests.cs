using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using ConcordiaCurriculumManager.Repositories;
using Microsoft.EntityFrameworkCore;
using ConcordiaCurriculumManager.Settings;
using Microsoft.Extensions.Options;

namespace ConcordiaCurriculumManagerTest.IntegrationTests.Repositories;

[TestClass]
public class GroupRepositoryTests
{
    private static CCMDbContext dbContext = null!;
    private IGroupRepository groupRepository = null!;

    [ClassInitialize]
    public static void ClassInitialize(TestContext _)
    {
        var options = new DbContextOptionsBuilder<CCMDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        dbContext = new CCMDbContext(options);
    }

    [ClassCleanup]
    public static void ClassCleanup() => dbContext.Dispose();

    [TestInitialize]
    public void TestInitialize() 
    {
        groupRepository = new GroupRepository(dbContext);
    }

    [TestMethod]
    public async Task GetGroupById_ValidId_ReturnsGroup()
    {
        var group = new Group
        {
            Id = Guid.NewGuid(),
            Name = "name"
        };

        dbContext.Groups.Add(group);
        await dbContext.SaveChangesAsync();

        var result = await groupRepository.GetGroupById(group.Id);

        Assert.IsNotNull(result);
        Assert.AreEqual(group.Id, result.Id);
    }

    [TestMethod]
    public async Task GetAllGroups_ReturnsGroups()
    {
        var initialGroups = await dbContext.Groups.ToListAsync();
        var testGroup1 = new Group { Name = "TestGroup1" };
        var testGroup2 = new Group { Name = "TestGroup2" };

        dbContext.Groups.AddRange(testGroup1, testGroup2);
        await dbContext.SaveChangesAsync();

        var allGroups = await groupRepository.GetAllGroups();

        Assert.IsNotNull(allGroups);

        var expectedGroupCount = initialGroups.Count + 2;
        Assert.AreEqual(expectedGroupCount, allGroups.Count);

        Assert.IsNotNull(allGroups.First(group => group.Name == testGroup1.Name));
        Assert.IsNotNull(allGroups.First(group => group.Name == testGroup2.Name));
    }

    [TestMethod]
    public async Task SaveGroup_ValidGroup_ReturnsTrue()
    {
        var group = new Group
        {
            Name = "TestGroup",
        };

        var result = await groupRepository.SaveGroup(group);

        Assert.IsTrue(result);
    }
}
