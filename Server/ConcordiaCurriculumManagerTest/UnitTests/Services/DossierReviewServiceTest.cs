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
    private Mock<ICourseService> courseService = null!;
    private Mock<IDossierRepository> dossierRepository = null!;
    private Mock<IDossierReviewRepository> dossierReviewRepository = null!;

    private DossierReviewService dossierReviewService = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        logger = new Mock<ILogger<DossierReviewService>>();
        dossierService = new Mock<IDossierService>();
        groupService = new Mock<IGroupService>();
        courseService = new Mock<ICourseService>();
        dossierRepository = new Mock<IDossierRepository>();
        dossierReviewRepository = new Mock<IDossierReviewRepository>();

        dossierReviewService = new DossierReviewService(
            logger.Object,
            dossierService.Object,
            groupService.Object,
            courseService.Object,
            dossierRepository.Object,
            dossierReviewRepository.Object
        );
    }

    [TestMethod]
    public async Task SubmitDossier_WithValidInput_SubmitsSuccessfully()
    {
        var dto = TestData.GetSampleDossierSubmissionDTO();
        var dossier = TestData.GetSampleDossier();
        dossier.State = DossierStateEnum.Created;

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
        dossier.State = DossierStateEnum.InReview;

        groupService.Setup(gs => gs.IsGroupIdListValid(dto.GroupIds)).ReturnsAsync(true);
        dossierService.Setup(ds => ds.GetDossierDetailsByIdOrThrow(dto.DossierId)).ReturnsAsync(dossier);

        await dossierReviewService.SubmitDossierForReview(dto);
    }

    [TestMethod]
    public async Task GetDossierWithApprovalStages_NotNull_Returns()
    {
        var dossier = TestData.GetSampleDossier();

        dossierReviewRepository.Setup(drr => drr.GetDossierWithApprovalStages(dossier.Id)).ReturnsAsync(dossier);

        var returnedDossier = await dossierReviewService.GetDossierWithApprovalStagesOrThrow(dossier.Id);

        Assert.IsNotNull(returnedDossier);
        Assert.AreEqual(dossier.Id, returnedDossier.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public async Task GetDossierWithApprovalStages_Null_Throws()
    {
        dossierReviewRepository.Setup(drr => drr.GetDossierWithApprovalStages(It.IsAny<Guid>())).ReturnsAsync((Dossier)null!);

        await dossierReviewService.GetDossierWithApprovalStagesOrThrow(Guid.NewGuid());
    }

    [TestMethod]
    public async Task RejectDossier_ValidDossier_MarksDossierForRejection()
    {
        var dossier = TestData.GetSampleDossier();
        dossier.State = DossierStateEnum.InReview;

        dossierReviewRepository.Setup(drr => drr.GetDossierWithApprovalStages(dossier.Id)).ReturnsAsync(dossier);
        dossierRepository.Setup(dr => dr.UpdateDossier(It.IsAny<Dossier>())).ReturnsAsync(true);

        await dossierReviewService.RejectDossier(dossier.Id);

        Assert.AreEqual(DossierStateEnum.Rejected, dossier.State);
        dossierRepository.Verify(mock => mock.UpdateDossier(dossier), Times.Once());
    }

    [TestMethod]
    public async Task ReturnDossier_WithFinalApprovalStage_Returns()
    {
        var dossier = TestData.GetSampleDossierInFinalStage();

        dossierReviewRepository.Setup(drr => drr.GetDossierWithApprovalStages(dossier.Id)).ReturnsAsync(dossier);

        await dossierReviewService.ReturnDossier(dossier.Id);

        dossierRepository.Verify(mock => mock.UpdateDossier(dossier), Times.Once());
    }

    [TestMethod]
    [ExpectedException(typeof(BadRequestException))]
    public async Task ReturnDossier_InInitialApprovalStage_Throws()
    {
        var dossier = TestData.GetSampleDossierInInitialStage();

        dossierReviewRepository.Setup(drr => drr.GetDossierWithApprovalStages(dossier.Id)).ReturnsAsync(dossier);

        await dossierReviewService.ReturnDossier(dossier.Id);
    }

    [TestMethod]
    public async Task ForwardDossier_WithInitialApprovalStage_Forwards()
    {
        var dossier = TestData.GetSampleDossierInInitialStage();

        dossierReviewRepository.Setup(drr => drr.GetDossierWithApprovalStagesAndRequests(dossier.Id)).ReturnsAsync(dossier);

        await dossierReviewService.ForwardDossier(dossier.Id);

        Assert.AreEqual(DossierStateEnum.InReview, dossier.State);
        dossierRepository.Verify(mock => mock.UpdateDossier(dossier), Times.Once());
    }

    [TestMethod]
    public async Task ForwardDossier_WithFinalApprovalStage_Accepts()
    {
        var dossier = TestData.GetSampleDossierInFinalStage();

        dossierReviewRepository.Setup(drr => drr.GetDossierWithApprovalStagesAndRequests(dossier.Id)).ReturnsAsync(dossier);
        courseService.Setup(cs => cs.GetCourseVersions(dossier)).ReturnsAsync(TestData.GetSampleCourseVersionCollection());

        await dossierReviewService.ForwardDossier(dossier.Id);

        Assert.AreEqual(DossierStateEnum.Approved, dossier.State);
        courseService.Verify(mock => mock.GetCourseVersions(dossier), Times.Once());
        dossierRepository.Verify(mock => mock.UpdateDossier(dossier), Times.Once());
    }
}
