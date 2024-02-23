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
        var thirdGrouping = TestData.GetSampleCourseGrouping();

        secondGrouping.CommonIdentifier = firstGrouping.CommonIdentifier;
        thirdGrouping.CommonIdentifier = firstGrouping.CommonIdentifier;

        firstGrouping.School = SchoolEnum.GinaCody;
        secondGrouping.School = SchoolEnum.GinaCody;
        thirdGrouping.School = SchoolEnum.GinaCody;

        firstGrouping.Version = 1;
        secondGrouping.Version = null;
        thirdGrouping.Version = 2;

        firstGrouping.State = CourseGroupingStateEnum.Accepted;
        secondGrouping.State = CourseGroupingStateEnum.CourseGroupingDeletionProposal;
        thirdGrouping.State = CourseGroupingStateEnum.Accepted;

        firstGrouping.Published = false;
        secondGrouping.Published = false;
        thirdGrouping.Published = true;

        var groupings = new List<CourseGrouping> { firstGrouping, secondGrouping, thirdGrouping };

        await dbContext.AddRangeAsync(groupings);
        await dbContext.SaveChangesAsync();
        var result = await courseGroupingRepository.GetCourseGroupingsBySchool(SchoolEnum.GinaCody);

        Assert.AreEqual(result.Count, 1);
        Assert.AreEqual(result.First().Id, thirdGrouping.Id);
    }

    [TestMethod]
    public async Task GetCourseGroupingsLikeName_WithValidSchoolName_ReturnsOnlyAcceptedCourseGroupings()
    {
        var firstCourseFirstGrouping = TestData.GetSampleCourseGrouping();
        var secondCourseFirstGrouping = TestData.GetSampleCourseGrouping();

        var firstCourseSecondGrouping = TestData.GetSampleCourseGrouping();
        var secondCourseSecondGrouping = TestData.GetSampleCourseGrouping();

        firstCourseFirstGrouping.CommonIdentifier = secondCourseFirstGrouping.CommonIdentifier;
        firstCourseSecondGrouping.CommonIdentifier = secondCourseSecondGrouping.CommonIdentifier;

        firstCourseFirstGrouping.Name = "This is a name1";
        secondCourseFirstGrouping.Name = "This is a name1";
        firstCourseSecondGrouping.Name = "This is a name2";
        secondCourseSecondGrouping.Name = "This is a name2";

        firstCourseFirstGrouping.School = SchoolEnum.GinaCody;
        secondCourseFirstGrouping.School = SchoolEnum.GinaCody;
        firstCourseSecondGrouping.School = SchoolEnum.GinaCody;
        secondCourseSecondGrouping.School = SchoolEnum.GinaCody;

        firstCourseFirstGrouping.Version = 1;
        secondCourseFirstGrouping.Version = 2;
        firstCourseSecondGrouping.Version = 1;
        secondCourseSecondGrouping.Version = null;

        firstCourseFirstGrouping.State = CourseGroupingStateEnum.Accepted;
        secondCourseFirstGrouping.State = CourseGroupingStateEnum.Accepted;
        firstCourseSecondGrouping.State = CourseGroupingStateEnum.Accepted;
        secondCourseSecondGrouping.State = CourseGroupingStateEnum.Accepted;

        firstCourseFirstGrouping.Published = false;
        secondCourseFirstGrouping.Published = true;
        firstCourseSecondGrouping.Published = true;
        secondCourseSecondGrouping.Published = false;

        var groupings = new List<CourseGrouping> { firstCourseFirstGrouping, secondCourseFirstGrouping, firstCourseSecondGrouping, secondCourseSecondGrouping };

        await dbContext.AddRangeAsync(groupings);
        await dbContext.SaveChangesAsync();
        var result = await courseGroupingRepository.GetCourseGroupingsLikeName("  This is a  ");

        Assert.AreEqual(result.Count, 2);

        foreach (var courseGrouping in result)
        {
            if (courseGrouping.CommonIdentifier.Equals(firstCourseSecondGrouping.CommonIdentifier))
            {
                Assert.AreEqual(firstCourseSecondGrouping.Id, courseGrouping.Id);
            }
            else
            {
                Assert.AreEqual(secondCourseFirstGrouping.Id, courseGrouping.Id);
            }
        }
    }
}
