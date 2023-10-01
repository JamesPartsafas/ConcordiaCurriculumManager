using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories;
using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using ConcordiaCurriculumManager.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ConcordiaCurriculumManagerTest.IntegrationTests.Repositories
{
    [TestClass]
    public class DossierRepositoryTests
    {
        private static CCMDbContext dbContext = null!;
        private IDossierRepository dossierRepository = null!;

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
            dossierRepository = new DossierRepository(dbContext);
        }

        [TestMethod]
        public async Task GetDossierById_ValidId_ReturnsListOfDossiers()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "fname",
                LastName = "lname",
                Email = "test@example.com",
                Password = "password"
            };

            var dossier = new Dossier
            {
                Initiator = user,
                InitiatorId = Guid.NewGuid(),
                Title = "test title",
                Description = "test description",
                Published = false

            };

            dbContext.Users.Add(user);
            dbContext.Dossiers.Add(dossier);
            await dbContext.SaveChangesAsync();

            var result = await dossierRepository.GetDossiersByID(user.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 1);
            Assert.AreEqual(user.Id, dossier.InitiatorId);
        }
    }
}

