using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManagerTest.UnitTests.UtilityFunctions;

namespace ConcordiaCurriculumManagerTest.UnitTests.Models;

[TestClass]
public class CourseTest
{
    [TestMethod]
    public void ModifyCourseFromDTOData_WithValidInput_AppliesChanges()
    {
        var course = TestData.GetSampleCourse();
        var edit = TestData.GetSampleEditCourseCreationRequestDTO();

        course.ModifyCourseFromDTOData(edit);

        Assert.AreEqual(edit.Title, course.Title);
        Assert.AreEqual(edit.Description, course.Description);
        Assert.AreEqual(edit.CourseNotes, course.CourseNotes);
        Assert.AreEqual(edit.CreditValue, course.CreditValue);
        Assert.AreEqual(edit.PreReqs, course.PreReqs);
        Assert.AreEqual(edit.Career, course.Career);
        Assert.AreEqual(edit.EquivalentCourses, course.EquivalentCourses);
        Assert.AreEqual(edit.ComponentCodes.Count, course.CourseCourseComponents!.Count);
        Assert.AreEqual(edit.SupportingFiles.Count, course.SupportingFiles!.Count);
    }

    [TestMethod]
    public void CreateCourseFromDTOData_WithValidInput_CreatesCourse()
    {
        var dto = TestData.GetSampleCourseCreationInitiationDTO(TestData.GetSampleDossier());
        var concordiaCourseId = 1;
        var version = 1;

        var course = Course.CreateCourseFromDTOData(dto, concordiaCourseId, version);

        Assert.AreEqual(concordiaCourseId, course.CourseID);
        Assert.AreEqual(version, course.Version);
        Assert.AreEqual(CourseStateEnum.NewCourseProposal, course.CourseState);
        Assert.AreEqual(dto.ComponentCodes.Count, course.CourseCourseComponents!.Count);
        Assert.AreEqual(dto.SupportingFiles.Count, course.SupportingFiles!.Count);
    }

    [DataTestMethod]
    [DataRow(CourseStateEnum.Accepted, true)]
    [DataRow(CourseStateEnum.NewCourseProposal, false)]
    [DataRow(CourseStateEnum.CourseChangeProposal, false)]
    [DataRow(CourseStateEnum.CourseDeletionProposal, false)]
    [DataRow(CourseStateEnum.Deleted, true)]
    public void CheckIfCourseIsFinalized_WithValidData_ReturnsCorrectValue(CourseStateEnum state, bool expected)
    {
        var course = TestData.GetSampleCourse();
        course.CourseState = state;

        bool actual = course.IsCourseStateFinalized();

        Assert.AreEqual(expected, actual);
    }

    [DataTestMethod]
    [DataRow(true, CourseStateEnum.Accepted, 4)]
    [DataRow(false, CourseStateEnum.Accepted, 4)]
    [DataRow(false, CourseStateEnum.NewCourseProposal, null)]
    public void VerifyCourseData_WithValidState_DoesNotThrow(bool published, CourseStateEnum state, int? version)
    {
        var course = TestData.GetSampleCourse();
        course.Published = published;
        course.CourseState = state;
        course.Version = version;

        course.VerifyCourseIsValidOrThrow();

        // If no exception is thrown, test has passed
    }

    [DataTestMethod]
    [DataRow(true, CourseStateEnum.Accepted, null)]
    [DataRow(true, CourseStateEnum.NewCourseProposal, 4)]
    [DataRow(true, CourseStateEnum.NewCourseProposal, null)]
    [DataRow(false, CourseStateEnum.Accepted, null)]
    [DataRow(false, CourseStateEnum.NewCourseProposal, 3)]
    public void VerifyCourseData_WithInvalidState_ThrowsArgumentException(bool published, CourseStateEnum state, int? version)
    {
        var course = TestData.GetSampleCourse();
        course.Published = published;
        course.CourseState = state;
        course.Version = version;

        Action action = course.VerifyCourseIsValidOrThrow;

        Assert.ThrowsException<ArgumentException>(action);
    }

    [TestMethod]
    public void MarkAsAccepted_GivenValidCourse_CorrectlyMarks()
    {
        var course = TestData.GetSampleCourse();
        var version = 5;

        course.MarkAsAccepted(version);

        Assert.AreEqual(CourseStateEnum.Accepted, course.CourseState);
        Assert.AreEqual(version, course.Version);
    }

    [TestMethod]
    public void MarkAsDeleted_GivenValidCourse_CorrectlyMarks()
    {
        var course = TestData.GetSampleCourse();
        var version = 5;

        course.MarkAsDeleted(version);

        Assert.AreEqual(CourseStateEnum.Deleted, course.CourseState);
        Assert.AreEqual(version, course.Version);
    }

    [TestMethod]
    public void CloneCourse_ReturnsDeepCopyOfACourse()
    {
        var originalCourse = TestData.GetSampleCourse();
        var clonedCourse = Course.CloneCourse(originalCourse);

        Assert.IsNotNull(clonedCourse);
        Assert.AreNotEqual(clonedCourse.Id, originalCourse.Id);
        Assert.AreEqual(clonedCourse.CourseID, originalCourse.CourseID);
        Assert.AreEqual(clonedCourse.Subject, originalCourse.Subject);
        Assert.AreEqual(clonedCourse.Catalog, originalCourse.Catalog);
        Assert.AreEqual(clonedCourse.Title, originalCourse.Title);
        Assert.AreEqual(clonedCourse.Description, originalCourse.Description);
        Assert.AreEqual(clonedCourse.CreditValue, originalCourse.CreditValue);
        Assert.AreEqual(clonedCourse.PreReqs, originalCourse.PreReqs);
        Assert.AreEqual(clonedCourse.Career, originalCourse.Career);
        Assert.AreEqual(clonedCourse.EquivalentCourses, originalCourse.EquivalentCourses);
        Assert.AreEqual(clonedCourse.CourseNotes, originalCourse.CourseNotes);
        Assert.AreEqual(clonedCourse.CourseState, originalCourse.CourseState);
        Assert.AreEqual(clonedCourse.Version, originalCourse.Version);
        Assert.AreEqual(clonedCourse.Published, originalCourse.Published);
    }
}
