using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using ConcordiaCurriculumManager.Repositories;
using Microsoft.EntityFrameworkCore;
using ConcordiaCurriculumManager.Models.Curriculum.CourseGroupings;
using ConcordiaCurriculumManagerTest.UnitTests.UtilityFunctions;

namespace ConcordiaCurriculumManagerTest.IntegrationTests.Repositories;

[TestClass]
public class CourseGroupingRepositoryTests
{
    private static CCMDbContext dbContext = null!;
    private ICourseGroupingRepository courseGroupingRepository = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        var options = new DbContextOptionsBuilder<CCMDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        dbContext = new CCMDbContext(options);
        courseGroupingRepository = new CourseGroupingRepository(dbContext);
    }

    [ClassCleanup]
    public static void ClassCleanup() => dbContext.Dispose();

    [TestMethod]
    public async Task GetCourseGroupingBySchool_WithValidSchool_ReturnsOnlyAcceptedCourseGroupings()
    {
        var firstGrouping = TestData.GetSampleCourseGrouping();
        var secondGrouping = TestData.GetSampleCourseGrouping();

        firstGrouping.School = SchoolEnum.GinaCody;
        secondGrouping.School = SchoolEnum.GinaCody;

        firstGrouping.Version = 1;
        secondGrouping.Version = null;

        firstGrouping.State = CourseGroupingStateEnum.Accepted;
        secondGrouping.State = CourseGroupingStateEnum.CourseGroupingDeletionProposal;

        firstGrouping.Published = true;
        secondGrouping.Published = false;

        var groupings = new List<CourseGrouping> { firstGrouping, secondGrouping };

        await dbContext.AddRangeAsync(groupings);
        await dbContext.SaveChangesAsync();
        var result = await courseGroupingRepository.GetCourseGroupingsBySchool(SchoolEnum.GinaCody);

        Assert.AreEqual(result.Count, 1);
    }

    [TestMethod]
    public async Task GetCourseGroupingsLikeName_WithValidSchoolName_ReturnsOnlyAcceptedCourseGroupings()
    {
        var firstGrouping = TestData.GetSampleCourseGrouping();
        var secondGrouping = TestData.GetSampleCourseGrouping();

        firstGrouping.Name = "This is a name";
        secondGrouping.Name = "This is a name";

        firstGrouping.School = SchoolEnum.GinaCody;
        secondGrouping.School = SchoolEnum.GinaCody;

        firstGrouping.Version = 1;
        secondGrouping.Version = null;

        firstGrouping.State = CourseGroupingStateEnum.Accepted;
        secondGrouping.State = CourseGroupingStateEnum.CourseGroupingDeletionProposal;

        firstGrouping.Published = true;
        secondGrouping.Published = false;

        var groupings = new List<CourseGrouping> { firstGrouping, secondGrouping };

        await dbContext.AddRangeAsync(groupings);
        await dbContext.SaveChangesAsync();
        var result = await courseGroupingRepository.GetCourseGroupingsLikeName("  This is a  ");

        Assert.AreEqual(result.Count, 1);
    }
}
