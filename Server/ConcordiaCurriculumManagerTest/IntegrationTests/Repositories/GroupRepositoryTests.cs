using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using ConcordiaCurriculumManager.Repositories;
using Microsoft.EntityFrameworkCore;
using ConcordiaCurriculumManager.Settings;
using Microsoft.Extensions.Options;
using ConcordiaCurriculumManagerTest.UnitTests.UtilityFunctions;

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

    [TestMethod]
    public async Task RemoveUserFromGroup_UserIsMemberAndGroupExists_ReturnsTrue()
    {
        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Password = "hashedpassword"
        };

        var group = new Group
        {
            Id = Guid.NewGuid(),
            Name = "Test Group",
            Members = new List<User> { user },
            GroupMasters = new List<User> { user }
        };

        dbContext.Groups.Add(group);
        await dbContext.SaveChangesAsync();

        var result = await groupRepository.RemoveUserFromGroup(userId, group.Id);

        Assert.IsTrue(result);
        var updatedGroup = await dbContext.Groups
            .Include(g => g.Members)
            .Include(g => g.GroupMasters)
            .FirstOrDefaultAsync(g => g.Id == group.Id);

        Assert.IsNotNull(updatedGroup);
        Assert.IsFalse(updatedGroup.Members.Any(u => u.Id == userId));
        Assert.IsFalse(updatedGroup.GroupMasters.Any(u => u.Id == userId));
    }

    [TestMethod]
    public async Task RemoveUserFromGroup_UserIsNotMember_ReturnsFalse()
    {
        var userId = Guid.NewGuid();
        var group = new Group
        {
            Id = Guid.NewGuid(),
            Name = "Test Group",
            Members = new List<User>(),
            GroupMasters = new List<User>()
        };
        dbContext.Groups.Add(group);
        await dbContext.SaveChangesAsync();

        var result = await groupRepository.RemoveUserFromGroup(userId, group.Id);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public async Task RemoveUserFromGroup_GroupDoesNotExist_ReturnsFalse()
    {
        var userId = Guid.NewGuid();
        var groupId = Guid.NewGuid();

        var result = await groupRepository.RemoveUserFromGroup(userId, groupId);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public async Task RemoveUserFromGroup_UserIsGroupMasterAndMember_ReturnsTrueAndRemovesFromBoth()
    {
        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Password = "hashedpassword"
        };

        dbContext.Users.Add(user);

        var group = new Group
        {
            Id = Guid.NewGuid(),
            Name = "Test Group",
            Members = new List<User> { user },
            GroupMasters = new List<User> { user }
        };

        dbContext.Groups.Add(group);
        await dbContext.SaveChangesAsync();

        var result = await groupRepository.RemoveUserFromGroup(userId, group.Id);

        Assert.IsTrue(result);
        var updatedGroup = await dbContext.Groups
            .Include(g => g.Members)
            .Include(g => g.GroupMasters)
            .FirstOrDefaultAsync(g => g.Id == group.Id);

        Assert.IsNotNull(updatedGroup);
        Assert.IsFalse(updatedGroup.Members.Any(u => u.Id == userId));
        Assert.IsFalse(updatedGroup.GroupMasters.Any(u => u.Id == userId));
    }


    [TestMethod]
    public async Task AddUserToGroup_UserNotAdminAndGroupExists_UserAdded()
    {
        var userId = Guid.NewGuid();
        var groupId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Password = "hashedpassword",
            Roles = new List<Role>()
        };
        var group = new Group
        {
            Id = groupId,
            Name = "Test Group",
            Members = new List<User>()
        };

        dbContext.Users.Add(user);
        dbContext.Groups.Add(group);
        await dbContext.SaveChangesAsync();

        var result = await groupRepository.AddUserToGroup(userId, groupId);

        Assert.IsTrue(result);
        var updatedGroup = await dbContext.Groups
            .Include(g => g.Members)
            .FirstOrDefaultAsync(g => g.Id == groupId);

        Assert.IsNotNull(updatedGroup);
        Assert.IsTrue(updatedGroup.Members.Any(u => u.Id == userId));
    }

    [TestMethod]
    public async Task AddUserToGroup_UserIsAdminAndGroupExists_UserNotAdded()
    {
        var userId = Guid.NewGuid();
        var groupId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            FirstName = "Jane",
            LastName = "Smith",
            Email = "jane.smith@example.com",
            Password = "hashedpassword",
            Roles = new List<Role> { new Role { UserRole = RoleEnum.Admin } }
        };
        var group = new Group
        {
            Id = groupId,
            Name = "Admin Group",
            Members = new List<User>()
        };

        dbContext.Users.Add(user);
        dbContext.Groups.Add(group);
        await dbContext.SaveChangesAsync();

        var result = await groupRepository.AddUserToGroup(userId, groupId);

        Assert.IsFalse(result);
        var updatedGroup = await dbContext.Groups
            .Include(g => g.Members)
            .FirstOrDefaultAsync(g => g.Id == groupId);

        Assert.IsNotNull(updatedGroup);
        Assert.IsFalse(updatedGroup.Members.Any(u => u.Id == userId));
    }

    [TestMethod]
    public async Task AddUserToGroup_UserDoesNotExist_ReturnsFalse()
    {
        var userId = Guid.NewGuid();
        var groupId = Guid.NewGuid();
        var group = new Group
        {
            Id = groupId,
            Name = "Nonexistent User Group",
            Members = new List<User>()
        };

        dbContext.Groups.Add(group);
        await dbContext.SaveChangesAsync();

        var result = await groupRepository.AddUserToGroup(userId, groupId);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public async Task AddUserToGroup_GroupDoesNotExist_ReturnsFalse()
    {
        var userId = Guid.NewGuid();
        var groupId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            FirstName = "Alice",
            LastName = "Johnson",
            Email = "alice.johnson@example.com",
            Password = "hashedpassword",
            Roles = new List<Role>()
        };

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();

        var result = await groupRepository.AddUserToGroup(userId, groupId);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public async Task AddUserToGroup_NeitherUserNorGroupExist_ReturnsFalse()
    {
        var userId = Guid.NewGuid();
        var groupId = Guid.NewGuid();

        var result = await groupRepository.AddUserToGroup(userId, groupId);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public async Task GetValidGroupIds_WithInputtedData_ReturnsAllIds()
    {
        var group1 = TestData.GetSampleGroup();
        var group2 = TestData.GetSampleGroup();

        dbContext.Groups.Add(group1);
        dbContext.Groups.Add(group2);
        await dbContext.SaveChangesAsync();

        var validIds = await groupRepository.GetValidGroupIds();

        Assert.IsTrue(validIds.Contains(group1.Id));
        Assert.IsTrue(validIds.Contains(group2.Id));
    }
}
