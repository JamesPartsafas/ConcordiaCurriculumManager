using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using ConcordiaCurriculumManager.Repositories;
using Microsoft.EntityFrameworkCore;
using ConcordiaCurriculumManager.Settings;
using Microsoft.Extensions.Options;

namespace ConcordiaCurriculumManagerTest.IntegrationTests.Repositories;

[TestClass]
public class UserRepositoryTests
{
    private static CCMDbContext dbContext = null!;
    private IUserRepository userRepository = null!;

    [ClassInitialize]
    public static void ClassInitialize(TestContext _)
    {
        var options = new DbContextOptionsBuilder<CCMDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        dbContext = new CCMDbContext(options);
    }

    [TestInitialize]
    public void TestInitialize()
    {
        userRepository = new UserRepository(dbContext);
    }

    [TestMethod]
    public async Task GetUserById_ValidId_ReturnsUser()
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "fname",
            LastName = "lname",
            Email = "test@example.com",
            Password = "password"
        };

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();

        var result = await userRepository.GetUserById(user.Id);

        Assert.IsNotNull(result);
        Assert.AreEqual(user.Id, result.Id);
    }

    [TestMethod]
    public async Task GetUserByEmail_ValidEmail_ReturnsUser()
    {
        var user = new User
        {
            FirstName = "fname",
            LastName = "lname",
            Email = "test@example.com",
            Password = "password"
        };

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();

        var result = await userRepository.GetUserByEmail(user.Email);

        Assert.IsNotNull(result);
        Assert.AreEqual(user.Email, result.Email);
    }

    [TestMethod]
    public async Task SaveUser_ValidUser_ReturnsTrue()
    {
        var user = new User
        {
            FirstName = "fname",
            LastName = "lname",
            Email = "test@example.com",
            Password = "password"
        };

        var result = await userRepository.SaveUser(user);

        Assert.IsTrue(result);
    }
}
