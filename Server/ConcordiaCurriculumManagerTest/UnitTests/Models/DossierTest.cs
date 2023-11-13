using ConcordiaCurriculumManager.DTO.Dossiers.DossierReview;
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

        var stages = dossier.PrepareForPublishing(dto);

        Assert.IsTrue(dossier.Published);
        Assert.AreEqual(numOfGroups, stages.Count);
        Assert.AreEqual(expectedLastIndex, stages[expectedLastIndex].StageIndex);
        Assert.IsTrue(stages[0].IsCurrentStage);
        Assert.IsTrue(stages[expectedLastIndex].IsFinalStage);
    }
}
