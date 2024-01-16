using ConcordiaCurriculumManager.Repositories;
using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using ConcordiaCurriculumManagerTest.UnitTests.UtilityFunctions;
using Microsoft.EntityFrameworkCore;

namespace ConcordiaCurriculumManagerTest.IntegrationTests.Repositories
{
    [TestClass]
    public class CourseIdentifiersRepositoryTests
    {
        private static CCMDbContext dbContext = null!;
        private ICourseIdentifiersRepository courseIdentifiersRepository = null!;

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
            courseIdentifiersRepository = new CourseIdentifiersRepository(dbContext);
        }

        [TestMethod]
        public async Task GetCourseIdentiferByConcordiaCourseId_ValidId_ReturnsCourseIdentifier()
        {
            var courseIdentifier = TestData.GetSampleCourseIdentifier();

            dbContext.CourseIdentifiers.Add(courseIdentifier);
            await dbContext.SaveChangesAsync();

            var result = await courseIdentifiersRepository.GetCourseIdentifierByConcordiaCourseId(courseIdentifier.ConcordiaCourseId);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.ConcordiaCourseId, courseIdentifier.ConcordiaCourseId);
        }

        [TestMethod]
        public async Task SaveCourseIdentifier_ReturnsTrue()
        {
            var courseIdentifier = TestData.GetSampleCourseIdentifier();

            var result = await courseIdentifiersRepository.SaveCourseIdentifier(courseIdentifier);

            Assert.IsTrue(result);
        }
    }
}

