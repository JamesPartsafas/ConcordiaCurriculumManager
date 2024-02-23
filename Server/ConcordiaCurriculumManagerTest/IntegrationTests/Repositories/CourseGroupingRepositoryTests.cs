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
    public async Task GetCourseGroupingBySchool_WithDeletedCourseGrouping_ReturnsEmptyList()
    {
        var firstGrouping = TestData.GetSampleCourseGrouping();
        var secondGrouping = TestData.GetSampleCourseGrouping();

        secondGrouping.CommonIdentifier = firstGrouping.CommonIdentifier;

        firstGrouping.School = SchoolEnum.GinaCody;
        secondGrouping.School = SchoolEnum.GinaCody;

        firstGrouping.Version = 1;
        secondGrouping.Version = 2;

        firstGrouping.State = CourseGroupingStateEnum.Accepted;
        secondGrouping.State = CourseGroupingStateEnum.Deleted;

        var groupings = new List<CourseGrouping> { firstGrouping, secondGrouping };

        await dbContext.AddRangeAsync(groupings);
        await dbContext.SaveChangesAsync();
        var result = await courseGroupingRepository.GetCourseGroupingsBySchool(SchoolEnum.GinaCody);

        Assert.AreEqual(result.Count, 0);
    }

    [TestMethod]
    public async Task GetCourseGroupingsLikeName_WithValidSchoolName_ReturnsOnlyAcceptedCourseGroupings()
    {
        var firstCourseGroupingFirstGroupingName = TestData.GetSampleCourseGrouping();
        var secondCourseGroupingFirstGroupingName = TestData.GetSampleCourseGrouping();

        var firstCourseGroupingSecondGroupingName = TestData.GetSampleCourseGrouping();
        var secondCourseGroupingSecondGroupingName = TestData.GetSampleCourseGrouping();

        firstCourseGroupingFirstGroupingName.CommonIdentifier = secondCourseGroupingFirstGroupingName.CommonIdentifier;
        firstCourseGroupingSecondGroupingName.CommonIdentifier = secondCourseGroupingSecondGroupingName.CommonIdentifier;

        firstCourseGroupingFirstGroupingName.Name = "This is a name1";
        secondCourseGroupingFirstGroupingName.Name = "This is a name1";
        firstCourseGroupingSecondGroupingName.Name = "This is a name2";
        secondCourseGroupingSecondGroupingName.Name = "This is a name2";

        firstCourseGroupingFirstGroupingName.School = SchoolEnum.GinaCody;
        secondCourseGroupingFirstGroupingName.School = SchoolEnum.GinaCody;
        firstCourseGroupingSecondGroupingName.School = SchoolEnum.GinaCody;
        secondCourseGroupingSecondGroupingName.School = SchoolEnum.GinaCody;

        firstCourseGroupingFirstGroupingName.Version = 1;
        secondCourseGroupingFirstGroupingName.Version = 2;
        firstCourseGroupingSecondGroupingName.Version = 1;
        secondCourseGroupingSecondGroupingName.Version = null;

        firstCourseGroupingFirstGroupingName.State = CourseGroupingStateEnum.Accepted;
        secondCourseGroupingFirstGroupingName.State = CourseGroupingStateEnum.Accepted;
        firstCourseGroupingSecondGroupingName.State = CourseGroupingStateEnum.Accepted;
        secondCourseGroupingSecondGroupingName.State = CourseGroupingStateEnum.Accepted;

        firstCourseGroupingFirstGroupingName.Published = false;
        secondCourseGroupingFirstGroupingName.Published = true;
        firstCourseGroupingSecondGroupingName.Published = true;
        secondCourseGroupingSecondGroupingName.Published = false;

        var groupings = new List<CourseGrouping> { firstCourseGroupingFirstGroupingName, secondCourseGroupingFirstGroupingName, firstCourseGroupingSecondGroupingName, secondCourseGroupingSecondGroupingName };

        await dbContext.AddRangeAsync(groupings);
        await dbContext.SaveChangesAsync();
        var result = await courseGroupingRepository.GetCourseGroupingsLikeName("  This is a  ");

        Assert.AreEqual(result.Count, 2);

        foreach (var courseGrouping in result)
        {
            if (courseGrouping.CommonIdentifier.Equals(firstCourseGroupingSecondGroupingName.CommonIdentifier))
            {
                Assert.AreEqual(firstCourseGroupingSecondGroupingName.Id, courseGrouping.Id);
            }
            else
            {
                Assert.AreEqual(secondCourseGroupingFirstGroupingName.Id, courseGrouping.Id);
            }
        }
    }
}
