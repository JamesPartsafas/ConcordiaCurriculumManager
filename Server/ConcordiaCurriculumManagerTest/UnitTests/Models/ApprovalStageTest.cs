using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManagerTest.UnitTests.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordiaCurriculumManagerTest.UnitTests.Models;

[TestClass]
public class ApprovalStageTest
{
    [DataTestMethod]
    [DataRow(-1, false)]
    [DataRow(0, true)]
    [DataRow(1, false)]
    public void VerifyCourseData_WithValidState_DoesNotThrow(int index, bool expected)
    {
        var stage = TestData.GetSampleApprovalStage();
        stage.StageIndex = index;

        var actual = stage.IsInitialStage();

        Assert.AreEqual(expected, actual);
    }
}
