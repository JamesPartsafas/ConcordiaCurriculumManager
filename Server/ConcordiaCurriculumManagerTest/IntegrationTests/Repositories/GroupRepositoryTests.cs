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

        dbContext = new CCMDbContext(options, Options.Create(new SeedDatabase()));
    }

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
    public async Task GetGroupByName_ValidName_ReturnsGroup()
    {
        var group = new Group
        {
            Name = "name"
        };

        dbContext.Groups.Add(group);
        await dbContext.SaveChangesAsync();

        var result = await groupRepository.GetGroupByName(group.Name);

        Assert.IsNotNull(result);
        Assert.AreEqual(group.Name, result.Name);
    }

    [TestMethod]
    public async Task GetAllGroups_ReturnsGroups()
    {
        var group1 = new Group { Name = "Group1" };
        var group2 = new Group { Name = "Group2" };

        dbContext.Groups.AddRange(group1, group2);
        await dbContext.SaveChangesAsync();

        var result = await groupRepository.GetAllGroups();

        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count);
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
