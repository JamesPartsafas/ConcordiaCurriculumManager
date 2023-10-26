using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using ConcordiaCurriculumManager.Repositories;
using Microsoft.EntityFrameworkCore;

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

    [ClassCleanup]
    public static void ClassCleanup() => dbContext.Dispose();

    [TestInitialize]
    public void TestInitialize()
    {
        userRepository = new UserRepository(dbContext);
    }

    [TestCleanup]
    public void TestCleanup()
    {
        dbContext.Users.RemoveRange(dbContext.Users);
        dbContext.SaveChanges();
    }

    [TestMethod]
    public async Task GetUserById_ValidId_ReturnsUser()
    {
        var user = await InsertUsersIntoDbContext(numberOfUsersToInsert: 3);

        var result = await userRepository.GetUserById(user[1].Id);

        Assert.IsNotNull(result);
        Assert.AreEqual(user[1].Id, result.Id);
    }

    [TestMethod]
    public async Task GetUserByEmail_ValidEmail_ReturnsUser()
    {
        var user = await InsertUsersIntoDbContext(numberOfUsersToInsert: 4);

        var result = await userRepository.GetUserByEmail(user[3].Email);

        Assert.IsNotNull(result);
        Assert.AreEqual(user[3].Email, result.Email);
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

    [TestMethod]
    public async Task GetAllUsersPageable_ReturnsCorrectNumberOfUsers()
    {
        await InsertUsersIntoDbContext(numberOfUsersToInsert: 20);
        Guid id = Guid.Empty;
        var users = await userRepository.GetAllUsersPageable(id);

        Assert.AreEqual(10, users.Count);
    }

    [TestMethod]
    public async Task GetAllUsersPageable_ReturnsCorrectPageableUsers()
    {
        var users = await InsertUsersIntoDbContext(numberOfUsersToInsert: 25);
        users = users.OrderBy(u => u.Id).ToList();
        Guid firstSearch = Guid.Empty, secondSearch = users[9].Id, thirdSearch = users[19].Id, fourthSearch = users[24].Id;

        var firstResult = await userRepository.GetAllUsersPageable(firstSearch);
        var secondResult = await userRepository.GetAllUsersPageable(secondSearch);
        var thirdResult = await userRepository.GetAllUsersPageable(thirdSearch);
        var fourthResult = await userRepository.GetAllUsersPageable(fourthSearch);

        Assert.AreEqual(10, firstResult.Count);
        Assert.AreEqual(10, secondResult.Count);
        Assert.AreEqual(5, thirdResult.Count);
        Assert.AreEqual(0, fourthResult.Count);
    }

    [TestMethod]
    public async Task GetUsersLikeEmail_ReturnsCorrectNumberOfUsersAfterInsertion()
    {
        await InsertUsersIntoDbContext(numberOfUsersToInsert: 20);
        Guid id = Guid.Empty;
        string email = "@example";

        var users = await userRepository.GetUsersLikeEmailPageable(id, email);

        Assert.AreEqual(10, users.Count);
    }

    [TestMethod]
    public async Task GetUsersLikeEmail_ReturnsCorrectPageableUsers()
    {
        var users = await InsertUsersIntoDbContext(numberOfUsersToInsert: 25);
        users = users.OrderBy(u => u.Id).ToList();
        string email = "@example";
        Guid firstSearch = Guid.Empty, secondSearch = users[9].Id, thirdSearch = users[19].Id, fourthSearch = users[24].Id;

        var firstResult = await userRepository.GetUsersLikeEmailPageable(firstSearch, email);
        var secondResult = await userRepository.GetUsersLikeEmailPageable(secondSearch, email);
        var thirdResult = await userRepository.GetUsersLikeEmailPageable(thirdSearch, email);
        var fourthResult = await userRepository.GetUsersLikeEmailPageable(fourthSearch, email);

        Assert.AreEqual(10, firstResult.Count);
        Assert.AreEqual(10, secondResult.Count);
        Assert.AreEqual(5, thirdResult.Count);
        Assert.AreEqual(0, fourthResult.Count);
    }

    private async Task<List<User>> InsertUsersIntoDbContext(int numberOfUsersToInsert)
    {
        var usersToAdd = new List<User>();

        for (int i = 0; i < numberOfUsersToInsert; i++)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = $"user{i}@example.com",
                Password = "password123"
            };

            usersToAdd.Add(user);
        }

        await dbContext.Users.AddRangeAsync(usersToAdd);
        await dbContext.SaveChangesAsync();
        return usersToAdd;
    }
}
