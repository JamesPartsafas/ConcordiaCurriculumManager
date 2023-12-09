using ConcordiaCurriculumManager.DTO.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
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
        var dossier = TestData.GetSampleDossier();
        dossier.State = DossierStateEnum.InReview;

        dossier.MarkAsRejected();

        Assert.AreEqual(DossierStateEnum.Rejected, dossier.State);
    }

    [DataTestMethod]
    [DataRow(DossierStateEnum.Created)]
    [DataRow(DossierStateEnum.Rejected)]
    [DataRow(DossierStateEnum.Approved)]
    public void RejectDossier_ThatIsNotInReview_Throws(DossierStateEnum state)
    {
        var dossier = TestData.GetSampleDossier();

        dossier.State = state;

        Assert.ThrowsException<BadRequestException>(() => dossier.MarkAsRejected());
    }
}
