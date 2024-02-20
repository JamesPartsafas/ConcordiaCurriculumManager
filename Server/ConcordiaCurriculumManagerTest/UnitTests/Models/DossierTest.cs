using ConcordiaCurriculumManager.DTO.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManagerTest.UnitTests.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordiaCurriculumManagerTest.UnitTests.Models;

[TestClass]
public class DossierTest
{
    [DataTestMethod]
    [DataRow(1, 0)]
    [DataRow(2, 1)]
    [DataRow(3, 2)]
    public void PrepareForPublishing_WithDifferentInputs_GivesExpectedResult(int numOfGroups, int expectedLastIndex)
    {
        var dto = TestData.GetSampleDossierSubmissionDTO();
        var dossier = TestData.GetSampleDossier();

        dto.GroupIds = new List<Guid>();
        for (var i = 0; i < numOfGroups; i++)
        {
            dto.GroupIds.Add(Guid.NewGuid());
        }

        Assert.AreEqual(DossierStateEnum.Created, dossier.State);

        var stages = dossier.PrepareForPublishing(dto);

        Assert.AreEqual(DossierStateEnum.InReview, dossier.State);
        Assert.AreEqual(numOfGroups, stages.Count);
        Assert.AreEqual(expectedLastIndex, stages[expectedLastIndex].StageIndex);
        Assert.IsTrue(stages[0].IsCurrentStage);
        Assert.IsTrue(stages[expectedLastIndex].IsFinalStage);
    }

    [DataTestMethod]
    [DataRow(DossierStateEnum.InReview)]
    [DataRow(DossierStateEnum.Rejected)]
    [DataRow(DossierStateEnum.Approved)]
    public void PrepareForPublishing_WithDifferentStates_Throws(DossierStateEnum state)
    {
        var dto = TestData.GetSampleDossierSubmissionDTO();
        var dossier = TestData.GetSampleDossier();

        dossier.State = state;

        Assert.ThrowsException<BadRequestException>(() => dossier.PrepareForPublishing(dto));
    }

    [TestMethod]
    public void RejectDossier_ThatIsInReview_Rejects()
    {
        var dossier = TestData.GetSampleDossierInInitialStage();
        var user = TestData.GetSampleUser();

        dossier.State = DossierStateEnum.InReview;

        dossier.MarkAsRejected(user);

        Assert.AreEqual(DossierStateEnum.Rejected, dossier.State);
    }

    [DataTestMethod]
    [DataRow(DossierStateEnum.Created)]
    [DataRow(DossierStateEnum.Rejected)]
    [DataRow(DossierStateEnum.Approved)]
    public void RejectDossier_ThatIsNotInReview_Throws(DossierStateEnum state)
    {
        var dossier = TestData.GetSampleDossierInInitialStage();
        var user = TestData.GetSampleUser();

        dossier.State = state;

        Assert.ThrowsException<BadRequestException>(() => dossier.MarkAsRejected(user));
    }

    [TestMethod]
    public void ForwardDossier_ThatIsInReview_Forwards()
    {
        var dossier = TestData.GetSampleDossierInInitialStage();
        var user = TestData.GetSampleUser();

        dossier.MarkAsForwarded(user);

        var initialStage = dossier.ApprovalStages.Where(stage => stage.StageIndex == 0).First();
        var finalStage = dossier.ApprovalStages.Where(stage => stage.StageIndex == 1).First();

        Assert.AreEqual(false, initialStage.IsCurrentStage);
        Assert.AreEqual(true, finalStage.IsCurrentStage);
    }

    [TestMethod]
    [ExpectedException(typeof(BadRequestException))]
    public void ForwardDossier_ThatIsInFinalReviewStage_Throws()
    {
        var dossier = TestData.GetSampleDossierInFinalStage();
        var user = TestData.GetSampleUser();

        dossier.MarkAsForwarded(user);
    }

    [TestMethod]
    public void AcceptDossier_ThatIsInFinalStage_MarksCompleted()
    {
        var dossier = TestData.GetSampleDossierInFinalStage();
        var user = TestData.GetSampleUser();

        dossier.MarkAsAccepted(TestData.GetSampleCourseVersionCollection(), user);

        var finalStage = dossier.ApprovalStages.Where(stage => stage.StageIndex == 1).First();

        Assert.AreEqual(DossierStateEnum.Approved, dossier.State);
        Assert.AreEqual(false, finalStage.IsCurrentStage);
    }

    [TestMethod]
    [ExpectedException(typeof(BadRequestException))]
    public void AcceptDossier_ThatIsInInitialStage_Throws()
    {
        var dossier = TestData.GetSampleDossierInInitialStage();
        var user = TestData.GetSampleUser();


        dossier.MarkAsAccepted(TestData.GetSampleCourseVersionCollection(), user);
    }

    [TestMethod]
    public void ReturnDossier_ThatIsInNonInitialStage_MarksReturned()
    {
        var dossier = TestData.GetSampleDossierInFinalStage();
        var user = TestData.GetSampleUser();

        var currentStage = dossier.ApprovalStages.Where(stage => stage.IsCurrentStage).First();

        dossier.MarkAsReturned(user);

        var previousStage = dossier.ApprovalStages.Where(stage => stage.IsCurrentStage).First();

        Assert.AreEqual(previousStage.StageIndex, currentStage.StageIndex - 1);
        Assert.AreEqual(true, previousStage.IsCurrentStage);
        Assert.AreEqual(false, currentStage.IsCurrentStage);
    }

    [TestMethod]
    [ExpectedException(typeof(BadRequestException))]
    public void ReturnDossier_ThatIsInInitialStage_Throws()
    {
        var dossier = TestData.GetSampleDossierInInitialStage();
        var user = TestData.GetSampleUser();

        dossier.MarkAsReturned(user);
    }

    [TestMethod]
    public void IsDossierCreatingCourse_ThatIsCreating_ReturnsTrue()
    {
        var dossier = TestData.GetSampleDossierInInitialStage();
        var request = TestData.GetSampleCourseCreationRequest();
        dossier.CourseCreationRequests.Add(request);

        var result = dossier.IsDossierCreatingCourse(request.NewCourse!.CourseID);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsDossierCreatingCourse_ThatIsNotCreating_ReturnsFalse()
    {
        var dossier = TestData.GetSampleDossierInInitialStage();

        var result = dossier.IsDossierCreatingCourse(5);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsDossierCreatingGrouping_ThatIsCreating_ReturnsTrue()
    {
        var dossier = TestData.GetSampleDossierInInitialStage();
        var request = TestData.GetSampleCourseGroupingRequest();
        dossier.CourseGroupingRequests.Add(request);

        var result = dossier.IsDossierCreatingGrouping(request.CourseGrouping!.CommonIdentifier);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsDossierCreatingGrouping_ThatIsNotCreating_ReturnsFalse()
    {
        var dossier = TestData.GetSampleDossierInInitialStage();

        var result = dossier.IsDossierCreatingGrouping(Guid.NewGuid());

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsDossierDeletingCourse_ThatIsDeleting_ReturnsTrue()
    {
        var dossier = TestData.GetSampleDossierInInitialStage();
        var request = TestData.GetSampleCourseDeletionRequest();
        dossier.CourseDeletionRequests.Add(request);

        var result = dossier.IsDossierDeletingCourse(request.Course!.CourseID);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsDossierDeletingCourse_ThatIsDeleting_ReturnsFalse()
    {
        var dossier = TestData.GetSampleDossierInInitialStage();

        var result = dossier.IsDossierDeletingCourse(5);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsDossierDeletingGrouping_ThatIsDeleting_ReturnsTrue()
    {
        var dossier = TestData.GetSampleDossierInInitialStage();
        var request = TestData.GetSampleCourseGroupingRequest();
        request.RequestType = RequestType.DeletionRequest;
        dossier.CourseGroupingRequests.Add(request);

        var result = dossier.IsDossierDeletingGrouping(request.CourseGrouping!.CommonIdentifier);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsDossierDeletingGrouping_ThatIsDeleting_ReturnsFalse()
    {
        var dossier = TestData.GetSampleDossierInInitialStage();

        var result = dossier.IsDossierDeletingGrouping(Guid.NewGuid());

        Assert.IsFalse(result);
    }
}
