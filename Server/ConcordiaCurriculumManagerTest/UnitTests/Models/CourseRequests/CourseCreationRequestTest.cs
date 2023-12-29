using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManagerTest.UnitTests.UtilityFunctions;

namespace ConcordiaCurriculumManagerTest.UnitTests.Models.CourseRequests;

[TestClass]
public class CourseCreationRequestTest
{
    [TestMethod]
    public void MarkAsAccepted_WithCourse_MarksCorrectly()
    {
        var request = TestData.GetSampleCourseCreationRequest();
        var course = TestData.GetSampleCourse();
        request.NewCourse = course;

        request.MarkAsAccepted(new List<CourseVersion>());

        Assert.AreEqual(CourseStateEnum.Accepted, request.NewCourse.CourseState);
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public void MarkAsAccepted_WithoutCourse_Throws()
    {
        var request = TestData.GetSampleCourseCreationRequest();
        request.NewCourse = null;

        request.MarkAsAccepted(new List<CourseVersion>());
    }

    [TestMethod]
    public void MarkAsAccepted_WithDeletedCourse_MarksCorrectly()
    {
        var request = TestData.GetSampleCourseCreationRequest();
        var course = TestData.GetSampleCourse();
        request.NewCourse = course;
        var versions = TestData.GetSampleCourseVersionCollection();
        versions.Add(TestData.GetSampleCourseVersion());

        request.MarkAsAccepted(versions);

        Assert.AreEqual(CourseStateEnum.Accepted, request.NewCourse.CourseState);
    }
}
