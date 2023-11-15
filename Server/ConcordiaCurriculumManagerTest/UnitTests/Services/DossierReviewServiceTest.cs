using Castle.Core.Logging;
using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Repositories;
using ConcordiaCurriculumManager.Services;
using ConcordiaCurriculumManagerTest.UnitTests.UtilityFunctions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordiaCurriculumManagerTest.UnitTests.Services;

[TestClass]
public class DossierReviewServiceTest
{
    private Mock<ILogger<DossierReviewService>> logger = null!;
    private Mock<IDossierService> dossierService = null!;
    private Mock<IGroupService> groupService = null!;
    private Mock<IDossierRepository> dossierRepository = null!;
    private Mock<IDossierReviewRepository> dossierReviewRepository = null!;

    private DossierReviewService dossierReviewService = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        logger = new Mock<ILogger<DossierReviewService>>();
        dossierService = new Mock<IDossierService>();
        groupService = new Mock<IGroupService>();
        dossierRepository = new Mock<IDossierRepository>();
        dossierReviewRepository = new Mock<IDossierReviewRepository>();

        dossierReviewService = new DossierReviewService(
            logger.Object,
            dossierService.Object,
            groupService.Object,
            dossierRepository.Object,
            dossierReviewRepository.Object
        );
    }

    [TestMethod]
    public async Task SubmitDossier_WithValidInput_SubmitsSuccessfully()
    {
        var dto = TestData.GetSampleDossierSubmissionDTO();
        var dossier = TestData.GetSampleDossier();
        dossier.Published = false;

        groupService.Setup(gs => gs.IsGroupIdListValid(dto.GroupIds)).ReturnsAsync(true);
        dossierService.Setup(ds => ds.GetDossierDetailsByIdOrThrow(dto.DossierId)).ReturnsAsync(dossier);
        dossierRepository.Setup(dr => dr.UpdateDossier(It.IsAny<Dossier>())).ReturnsAsync(true);
        dossierReviewRepository.Setup(drr => drr.SaveApprovalStages(It.IsAny<IList<ApprovalStage>>())).ReturnsAsync(true);

        await dossierReviewService.SubmitDossierForReview(dto);

        // No exception thrown means method succeeded
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidInputException))]
    public async Task SubmitDossier_WithNoGroups_Throws()
    {
        var dto = TestData.GetSampleDossierSubmissionDTO();
        dto.GroupIds = new List<Guid>();

        await dossierReviewService.SubmitDossierForReview(dto);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidInputException))]
    public async Task SubmitDossier_WithInvalidGroups_Throws()
    {
        var dto = TestData.GetSampleDossierSubmissionDTO();

        groupService.Setup(gs => gs.IsGroupIdListValid(dto.GroupIds)).ReturnsAsync(false);

        await dossierReviewService.SubmitDossierForReview(dto);
    }

    [TestMethod]
    [ExpectedException(typeof(BadRequestException))]
    public async Task SubmitDossier_WithAlreadyPublishedDossier_Throws()
    {
        var dto = TestData.GetSampleDossierSubmissionDTO();
        var dossier = TestData.GetSampleDossier();
        dossier.Published = true;

        groupService.Setup(gs => gs.IsGroupIdListValid(dto.GroupIds)).ReturnsAsync(true);
        dossierService.Setup(ds => ds.GetDossierDetailsByIdOrThrow(dto.DossierId)).ReturnsAsync(dossier);

        await dossierReviewService.SubmitDossierForReview(dto);
    }
}
