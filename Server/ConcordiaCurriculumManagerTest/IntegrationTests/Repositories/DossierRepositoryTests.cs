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

        [TestMethod]
        public async Task GetDossierByDossierId_ValidId_ReturnsDossier()
        {
            var dossier = new Dossier
            {
                InitiatorId = Guid.NewGuid(),
                Title = "test title",
                Description = "test description",
                Published = false

            };

            dbContext.Dossiers.Add(dossier);
            await dbContext.SaveChangesAsync();

            var result = await dossierRepository.GetDossierByDossierId(dossier.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(result, dossier);
        }

        [TestMethod]
        public async Task SaveDossier_ReturnsTrue() {
            var dossier = new Dossier
            {
                InitiatorId = Guid.NewGuid(),
                Title = "test title",
                Description = "test description",
                Published = false
            };

            var result = await dossierRepository.SaveDossier(dossier);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task SaveCourseCreationRequest_ReturnsTrue()
        {
            var courseCreationRequest = new CourseCreationRequest
            {
                NewCourseId = Guid.NewGuid(),
                DossierId = Guid.NewGuid(),
            };

            var result = await dossierRepository.SaveCourseCreationRequest(courseCreationRequest);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task SaveCourseModificationRequest_ReturnsTrue()
        {
            var courseModificationRequest = new CourseModificationRequest
            {
                CourseId = Guid.NewGuid(),
                DossierId = Guid.NewGuid(),
            };

            var result = await dossierRepository.SaveCourseModificationRequest(courseModificationRequest);

            Assert.IsTrue(result);
        }
    }
}

