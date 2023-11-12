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
}
