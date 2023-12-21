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

        request.MarkAsAccepted();

        Assert.AreEqual(CourseStateEnum.Accepted, request.NewCourse.CourseState);
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public void MarkAsAccepted_WithoutCourse_Throws()
    {
        var request = TestData.GetSampleCourseCreationRequest();
        request.NewCourse = null;

        request.MarkAsAccepted();
    }
}
