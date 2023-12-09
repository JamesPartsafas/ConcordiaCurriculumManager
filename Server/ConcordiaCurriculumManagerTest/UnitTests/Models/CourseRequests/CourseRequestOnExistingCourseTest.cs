using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManagerTest.UnitTests.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordiaCurriculumManagerTest.UnitTests.Models.CourseRequests;

[TestClass]
public class CourseRequestOnExistingCourseTest
{
    [TestMethod]
    public void MarkAsAccepted_WithCourse_MarksCorrectly()
    {
        var collection = TestData.GetSampleCourseVersionCollection();
        var courseVersion = TestData.GetSampleCourseVersion();
        collection.Add(courseVersion);
        var request = TestData.GetSampleCourseModificationRequest();

        request.Course!.Version = courseVersion.Version;
        var expectedVersion = courseVersion.Version + 1;

        request.MarkAsAccepted(collection);

        Assert.AreEqual(CourseStateEnum.Accepted, request.Course.CourseState);
        Assert.AreEqual(expectedVersion, request.Course.Version);
    }

    [TestMethod]
    public void MarkAsDeleted_WithCourse_MarksCorrectly()
    {
        var collection = TestData.GetSampleCourseVersionCollection();
        var courseVersion = TestData.GetSampleCourseVersion();
        collection.Add(courseVersion);
        var request = TestData.GetSampleCourseDeletionRequest();

        request.Course!.Version = courseVersion.Version;
        var expectedVersion = courseVersion.Version + 1;

        request.MarkAsDeleted(collection);

        Assert.AreEqual(CourseStateEnum.Deleted, request.Course.CourseState);
        Assert.AreEqual(expectedVersion, request.Course.Version);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void MarkAsAccepeted_WithCourseNotFound_Throws()
    {
        var collection = TestData.GetSampleCourseVersionCollection();
        var request = TestData.GetSampleCourseModificationRequest();

        var version = 9;
        request.Course!.Version = version;
        var expectedVersion = version + 1;

        request.MarkAsAccepted(collection);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void MarkAsDeleted_WithCourseNotFound_Throws()
    {
        var collection = TestData.GetSampleCourseVersionCollection();
        var request = TestData.GetSampleCourseDeletionRequest();

        var version = 9;
        request.Course!.Version = version;
        var expectedVersion = version + 1;

        request.MarkAsDeleted(collection);
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public void GetNextCourseVersion_WithoutCourse_Throws()
    {
        var request = TestData.GetSampleCourseModificationRequest();
        request.Course = null;

        request.MarkAsAccepted(TestData.GetSampleCourseVersionCollection());
    }
}
