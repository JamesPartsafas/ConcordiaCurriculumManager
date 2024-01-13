using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Curriculum.CourseGrouping;
using ConcordiaCurriculumManager.Repositories;
using ConcordiaCurriculumManager.Services;
using ConcordiaCurriculumManagerTest.UnitTests.UtilityFunctions;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordiaCurriculumManagerTest.UnitTests.Services;

[TestClass]
public class CourseGroupingServiceTest
{
    private Mock<ILogger<CourseGroupingService>> logger = null!;
    private Mock<ICourseRepository> courseRepository = null!;
    private Mock<ICourseGroupingRepository> courseGroupingRepository = null!;

    private CourseGroupingService courseGroupingService = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        logger = new Mock<ILogger<CourseGroupingService>>();
        courseRepository = new Mock<ICourseRepository>();
        courseGroupingRepository = new Mock<ICourseGroupingRepository>();

        courseGroupingService = new CourseGroupingService(
            logger.Object,
            courseRepository.Object,
            courseGroupingRepository.Object
        );
    }

    [TestMethod]
    public async Task GetCourseGrouping_WithMultipleLevels_QueriesRecursively()
    {
        var grouping = TestData.GetSampleCourseGrouping();
        var subgrouping = grouping.SubGroupings.First();
        var course = grouping.Courses.First();
        grouping.SubGroupings = new List<CourseGrouping>();
        grouping.Courses = new List<Course>();
        subgrouping.Courses = new List<Course>();

        courseGroupingRepository.Setup(cgr => cgr.GetCourseGroupingById(grouping.Id)).ReturnsAsync(grouping);
        courseGroupingRepository.Setup(cgr => cgr.GetCourseGroupingByCommonIdentifier(subgrouping.CommonIdentifier)).ReturnsAsync(subgrouping);
        courseRepository.Setup(cr => cr.GetCoursesByConcordiaCourseIds(It.IsAny<IList<int>>())).ReturnsAsync(new List<Course> { { course } } );

        var output = await courseGroupingService.GetCourseGrouping(grouping.Id);

        Assert.AreEqual(course.Id, grouping.Courses.First().Id);
        Assert.AreEqual(course.Id, subgrouping.Courses.First().Id);
        Assert.AreEqual(subgrouping.CommonIdentifier, grouping.SubGroupings.First().CommonIdentifier);
        Assert.AreEqual(subgrouping.CommonIdentifier, grouping.SubGroupingReferences.First().ChildGroupCommonIdentifier);

        courseGroupingRepository.Verify(mock => mock.GetCourseGroupingById(It.IsAny<Guid>()), Times.Once());
        courseGroupingRepository.Verify(mock => mock.GetCourseGroupingByCommonIdentifier(It.IsAny<Guid>()), Times.Once());
        courseRepository.Verify(mock => mock.GetCoursesByConcordiaCourseIds(It.IsAny<List<int>>()), Times.Exactly(2));
    }

    [TestMethod]
    public async Task GetCourseGrouping_WithLowestLevel_QueriesOnlyOnce()
    {
        var grouping = TestData.GetSampleCourseGrouping().SubGroupings.First();
        var course = grouping.Courses.First();
        grouping.Courses = new List<Course>();

        courseGroupingRepository.Setup(cgr => cgr.GetCourseGroupingById(grouping.Id)).ReturnsAsync(grouping);
        courseRepository.Setup(cr => cr.GetCoursesByConcordiaCourseIds(It.IsAny<IList<int>>())).ReturnsAsync(new List<Course> { { course } });

        var output = await courseGroupingService.GetCourseGrouping(grouping.Id);

        Assert.AreEqual(course.Id, grouping.Courses.First().Id);
        Assert.AreEqual(0, grouping.SubGroupings.Count);

        courseGroupingRepository.Verify(mock => mock.GetCourseGroupingById(It.IsAny<Guid>()), Times.Once());
        courseGroupingRepository.Verify(mock => mock.GetCourseGroupingByCommonIdentifier(It.IsAny<Guid>()), Times.Never());
        courseRepository.Verify(mock => mock.GetCoursesByConcordiaCourseIds(It.IsAny<List<int>>()), Times.Once());
    }

    [TestMethod]
    public async Task GetCourseGroupingBySchool_WithValidSchool_QueriesOnlyOnce()
    {
        var grouping = TestData.GetSampleCourseGrouping();
        var groupings = new List<CourseGrouping> { { grouping } };

        courseGroupingRepository.Setup(cgr => cgr.GetCourseGroupingsBySchool(grouping.School)).ReturnsAsync(groupings);

        var output = await courseGroupingService.GetCourseGroupingsBySchoolNonRecursive(grouping.School);

        courseGroupingRepository.Verify(mock => mock.GetCourseGroupingsBySchool(It.IsAny<SchoolEnum>()), Times.Once());
    }

    [TestMethod]
    public async Task GetCourseGroupingLikeName_WithValidName_QueriesOnlyOnce()
    {
        var grouping = TestData.GetSampleCourseGrouping();
        var groupings = new List<CourseGrouping> { { grouping } };

        courseGroupingRepository.Setup(cgr => cgr.GetCourseGroupingsLikeName(grouping.Name)).ReturnsAsync(groupings);

        var output = await courseGroupingService.GetCourseGroupingsLikeName(grouping.Name);

        courseGroupingRepository.Verify(mock => mock.GetCourseGroupingsLikeName(It.IsAny<string>()), Times.Once());
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidInputException))]
    public async Task GetCourseGroupingLikeName_WithEmptyName_ThrowsInvalidInputException()
    {
        var output = await courseGroupingService.GetCourseGroupingsLikeName(" ");
    }
}
