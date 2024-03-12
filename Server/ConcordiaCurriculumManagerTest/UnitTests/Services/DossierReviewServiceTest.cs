using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManager.Models.Curriculum.CourseGroupings;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Repositories;
using ConcordiaCurriculumManager.Services;
using ConcordiaCurriculumManagerTest.UnitTests.UtilityFunctions;
using Microsoft.Extensions.Logging;
using Moq;

namespace ConcordiaCurriculumManagerTest.UnitTests.Services;

[TestClass]
public class DossierReviewServiceTest
{
    private Mock<ILogger<DossierReviewService>> logger = null!;
    private Mock<IDossierService> dossierService = null!;
    private Mock<IEmailService> emailService = null!;
    private Mock<IGroupService> groupService = null!;
    private Mock<ICourseService> courseService = null!;
    private Mock<IDossierRepository> dossierRepository = null!;
    private Mock<IUserAuthenticationService> userService = null!;
    private Mock<IDossierReviewRepository> dossierReviewRepository = null!;
    private Mock<ICourseRepository> courseRepository = null!;
    private Mock<ICourseGroupingService> courseGroupingService = null!;

    private DossierReviewService dossierReviewService = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        logger = new Mock<ILogger<DossierReviewService>>();
        dossierService = new Mock<IDossierService>();
        groupService = new Mock<IGroupService>();
        courseService = new Mock<ICourseService>();
        dossierRepository = new Mock<IDossierRepository>();
        userService = new Mock<IUserAuthenticationService>();
        dossierReviewRepository = new Mock<IDossierReviewRepository>();
        emailService = new Mock<IEmailService>();
        courseRepository = new Mock<ICourseRepository>();
        courseGroupingService = new Mock<ICourseGroupingService>();

        dossierReviewService = new DossierReviewService(
            logger.Object,
            dossierService.Object,
            groupService.Object,
            courseService.Object,
            dossierRepository.Object,
            dossierReviewRepository.Object,
            userService.Object,
            emailService.Object,
            courseRepository.Object,
            courseGroupingService.Object
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
        var dossier = TestData.GetSampleDossierInInitialStage();
        var user = TestData.GetSampleUser();

        dossier.State = DossierStateEnum.InReview;
        user.Id = dossier.InitiatorId;
        dossier.Initiator = user;

        dossierReviewRepository.Setup(drr => drr.GetDossierWithApprovalStages(dossier.Id)).ReturnsAsync(dossier);
        dossierRepository.Setup(dr => dr.UpdateDossier(It.IsAny<Dossier>())).ReturnsAsync(true);

        await dossierReviewService.RejectDossier(dossier.Id, user);

        Assert.AreEqual(DossierStateEnum.Rejected, dossier.State);
        dossierRepository.Verify(mock => mock.UpdateDossier(dossier), Times.Once());
    }

    [TestMethod]
    public async Task ReturnDossier_WithFinalApprovalStage_Returns()
    {
        var dossier = TestData.GetSampleDossierInFinalStage();
        var user = TestData.GetSampleUser();

        user.Id = dossier.InitiatorId;
        dossier.Initiator = user;

        dossierReviewRepository.Setup(drr => drr.GetDossierWithApprovalStages(dossier.Id)).ReturnsAsync(dossier);

        await dossierReviewService.ReturnDossier(dossier.Id, user);

        dossierRepository.Verify(mock => mock.UpdateDossier(dossier), Times.Once());
    }

    [TestMethod]
    [ExpectedException(typeof(BadRequestException))]
    public async Task ReturnDossier_InInitialApprovalStage_Throws()
    {
        var dossier = TestData.GetSampleDossierInInitialStage();
        var user = TestData.GetSampleUser();

        dossierReviewRepository.Setup(drr => drr.GetDossierWithApprovalStages(dossier.Id)).ReturnsAsync(dossier);

        await dossierReviewService.ReturnDossier(dossier.Id, user);
    }

    [TestMethod]
    public async Task ForwardDossier_WithInitialApprovalStage_Forwards()
    {
        var dossier = TestData.GetSampleDossierInInitialStage();
        var user = TestData.GetSampleUser();

        user.Id = dossier.InitiatorId;
        dossier.Initiator = user;

        dossierReviewRepository.Setup(drr => drr.GetDossierWithApprovalStagesAndRequests(dossier.Id)).ReturnsAsync(dossier);

        await dossierReviewService.ForwardDossier(dossier.Id, user);

        Assert.AreEqual(DossierStateEnum.InReview, dossier.State);
        dossierRepository.Verify(mock => mock.UpdateDossier(dossier), Times.Once());
    }

    [TestMethod]
    public async Task ForwardDossier_WithFinalApprovalStage_AcceptsAndSendsEmailWhenUpdateSucceeds()
    {
        var dossier = TestData.GetSampleDossierInFinalStage();
        var initiator = TestData.GetSampleUser();

        initiator.Id = dossier.InitiatorId;
        dossier.Initiator = initiator;

        dossierRepository.Setup(dr => dr.UpdateDossier(It.IsAny<Dossier>())).ReturnsAsync(true);
        dossierReviewRepository.Setup(drr => drr.GetDossierWithApprovalStagesAndRequests(dossier.Id)).ReturnsAsync(dossier);
        courseService.Setup(cs => cs.GetCourseVersions(dossier)).ReturnsAsync(TestData.GetSampleCourseVersionCollection());
        dossierRepository.Setup(dr => dr.GetAllNonApprovedDossiers()).ReturnsAsync(new List<Dossier>());

        await dossierReviewService.ForwardDossier(dossier.Id, initiator);

        Assert.AreEqual(DossierStateEnum.Approved, dossier.State);
        courseService.Verify(mock => mock.GetCourseVersions(dossier), Times.Once());
        dossierRepository.Verify(mock => mock.UpdateDossier(dossier), Times.Once());
        emailService.Verify(mock => mock.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Once);
    }

    [TestMethod]
    public async Task ForwardDossier_WithFinalApprovalStage_AcceptsAndDoesNotSendsEmailWhenUpdateFails()
    {
        var dossier = TestData.GetSampleDossierInFinalStage();
        var initiator = TestData.GetSampleUser();

        initiator.Id = dossier.InitiatorId;
        dossier.Initiator = initiator;

        dossierRepository.Setup(dr => dr.UpdateDossier(It.IsAny<Dossier>())).ReturnsAsync(false);
        dossierReviewRepository.Setup(drr => drr.GetDossierWithApprovalStagesAndRequests(dossier.Id)).ReturnsAsync(dossier);
        courseService.Setup(cs => cs.GetCourseVersions(dossier)).ReturnsAsync(TestData.GetSampleCourseVersionCollection());
        dossierRepository.Setup(dr => dr.GetAllNonApprovedDossiers()).ReturnsAsync(new List<Dossier>());

        await dossierReviewService.ForwardDossier(dossier.Id, initiator);

        Assert.AreEqual(DossierStateEnum.Approved, dossier.State);
        courseService.Verify(mock => mock.GetCourseVersions(dossier), Times.Once());
        dossierRepository.Verify(mock => mock.UpdateDossier(dossier), Times.Once());
        emailService.Verify(mock => mock.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Never);
    }

    [TestMethod]
    public async Task AcceptDossier_GivenOtherDossiers_UpdatesOtherDossierGroupings()
    {
        var acceptedDossier = TestData.GetSampleDossierInFinalStage();
        var existingDossier = TestData.GetSampleDossier();
        var initiator = TestData.GetSampleUser();

        var groupingToModifyId = Guid.NewGuid();
        var groupingToDeleteId = Guid.NewGuid();

        var acceptedGroupingToModify = TestData.GetSampleCourseGroupingRequest();
        acceptedGroupingToModify.CourseGrouping!.CourseIdentifiers = new List<CourseIdentifier>();
        acceptedGroupingToModify.CourseGrouping!.SubGroupingReferences = new List<CourseGroupingReference>();
        acceptedGroupingToModify.CourseGrouping!.CommonIdentifier = groupingToModifyId;
        acceptedGroupingToModify.CourseGrouping!.State = CourseGroupingStateEnum.CourseGroupingDeletionProposal;
        acceptedGroupingToModify.RequestType = RequestType.DeletionRequest;

        var acceptedGroupingToDelete = TestData.GetSampleCourseGroupingRequest();
        acceptedGroupingToDelete.CourseGrouping!.CourseIdentifiers = new List<CourseIdentifier>();
        acceptedGroupingToDelete.CourseGrouping!.SubGroupingReferences = new List<CourseGroupingReference>();
        acceptedGroupingToDelete.CourseGrouping!.CommonIdentifier = groupingToDeleteId;
        acceptedGroupingToDelete.CourseGrouping!.State = CourseGroupingStateEnum.CourseGroupingDeletionProposal;
        acceptedGroupingToDelete.RequestType = RequestType.DeletionRequest;

        var existingGroupingToModify = TestData.GetSampleCourseGroupingRequest();
        existingGroupingToModify.CourseGrouping!.CourseIdentifiers = new List<CourseIdentifier>();
        existingGroupingToModify.CourseGrouping!.SubGroupingReferences = new List<CourseGroupingReference>();
        existingGroupingToModify.CourseGrouping!.CommonIdentifier = groupingToModifyId;
        existingGroupingToModify.CourseGrouping!.State = CourseGroupingStateEnum.CourseGroupingChangeProposal;
        existingGroupingToModify.RequestType = RequestType.ModificationRequest;

        var existingGroupingToDelete = TestData.GetSampleCourseGroupingRequest();
        existingGroupingToDelete.CourseGrouping!.CourseIdentifiers = new List<CourseIdentifier>();
        existingGroupingToDelete.CourseGrouping!.SubGroupingReferences = new List<CourseGroupingReference>();
        existingGroupingToDelete.CourseGrouping!.CommonIdentifier = groupingToDeleteId;
        existingGroupingToDelete.CourseGrouping!.State = CourseGroupingStateEnum.CourseGroupingDeletionProposal;
        existingGroupingToDelete.RequestType = RequestType.DeletionRequest;

        acceptedDossier.CourseGroupingRequests = new List<CourseGroupingRequest> { acceptedGroupingToModify, acceptedGroupingToDelete };
        existingDossier.CourseGroupingRequests = new List<CourseGroupingRequest> { existingGroupingToModify, existingGroupingToDelete };

        initiator.Id = acceptedDossier.InitiatorId;
        acceptedDossier.Initiator = initiator;

        dossierRepository.Setup(dr => dr.UpdateDossier(It.IsAny<Dossier>())).ReturnsAsync(true);
        dossierReviewRepository.Setup(drr => drr.GetDossierWithApprovalStagesAndRequests(acceptedDossier.Id)).ReturnsAsync(acceptedDossier);
        courseService.Setup(cs => cs.GetCourseVersions(acceptedDossier)).ReturnsAsync(TestData.GetSampleCourseVersionCollection());
        courseGroupingService.Setup(cgs => cgs.GetGroupingVersions(acceptedDossier)).ReturnsAsync(TestData.GetSampleGroupingVersions());
        dossierRepository.Setup(dr => dr.GetAllNonApprovedDossiers()).ReturnsAsync(new List<Dossier> { existingDossier });

        await dossierReviewService.ForwardDossier(acceptedDossier.Id, initiator);

        Assert.AreEqual(RequestType.CreationRequest, existingGroupingToModify.RequestType);
        Assert.AreEqual(CourseGroupingStateEnum.NewCourseGroupingProposal, existingGroupingToModify.CourseGrouping!.State);
        Assert.AreEqual(1, existingDossier.CourseGroupingRequests.Count);
    }

    [TestMethod]
    public async Task AddDossierDiscussionReview_ValidInput_AddsMessageAndSavesDossier()
    {
        var dossier = TestData.GetSampleDossierWithDiscussion();
        var message = TestData.GetSampleDiscussionMessage();
        
        userService.Setup(x => x.GetCurrentUserClaim(It.IsAny<string>())).Returns(Guid.NewGuid().ToString());
        dossierRepository.Setup(drr => drr.GetDossierByDossierId(dossier.Id)).ReturnsAsync(dossier);
        dossierRepository.Setup(dr => dr.UpdateDossier(dossier)).ReturnsAsync(true);

        await dossierReviewService.AddDossierDiscussionReview(dossier.Id, message);

        Assert.AreEqual(1, dossier.Discussion!.Messages.Count); 
        Assert.AreEqual(message, dossier.Discussion.Messages.Last()); 
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public async Task AddDossierDiscussionReview_DossierDiscussionNotFound_Throws()
    {
        var dossierId = Guid.NewGuid();
        var message = TestData.GetSampleDiscussionMessage();
        var dossier = TestData.GetSampleDossier();
        dossier.Discussion = null!;
        dossier.State = DossierStateEnum.InReview;

        userService.Setup(x => x.GetCurrentUserClaim(It.IsAny<string>())).Returns(Guid.NewGuid().ToString());

        dossierReviewRepository.Setup(drr => drr.GetDossierWithApprovalStagesAndRequestsAndDiscussion(dossierId)).ReturnsAsync(dossier);

        await dossierReviewService.AddDossierDiscussionReview(dossierId, message);
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public async Task AddDossierDiscussionReview_UserIdNotFound_Throws()
    {
        var dossierId = Guid.NewGuid();
        var message = TestData.GetSampleDiscussionMessage();
        var dossier = TestData.GetSampleDossier();
        dossier.Discussion = null!;
        dossier.State = DossierStateEnum.InReview;

        dossierReviewRepository.Setup(drr => drr.GetDossierWithApprovalStagesAndRequestsAndDiscussion(dossierId)).ReturnsAsync(dossier);

        await dossierReviewService.AddDossierDiscussionReview(dossierId, message);
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public async Task EditDossierDiscussionReview_DossierDiscussionNotFound_Throws()
    {
        var dossierId = Guid.NewGuid();
        var discussionMessage = TestData.GetSampleEditDossierDiscussionMessageDTO();
        var dossier = TestData.GetSampleDossier();
        dossier.Discussion = null!;
        dossier.State = DossierStateEnum.InReview;

        userService.Setup(x => x.GetCurrentUserClaim(It.IsAny<string>())).Returns(Guid.NewGuid().ToString());

        dossierReviewRepository.Setup(drr => drr.GetDossierWithApprovalStagesAndRequestsAndDiscussion(dossierId)).ReturnsAsync(dossier);

        await dossierReviewService.EditDossierDiscussionReview(dossierId, discussionMessage);
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public async Task EditDossierDiscussionReview_UserIdNotFound_Throws()
    {
        var dossierId = Guid.NewGuid();
        var discussionMessage = TestData.GetSampleEditDossierDiscussionMessageDTO();
        var dossier = TestData.GetSampleDossier();
        dossier.Discussion = null!;
        dossier.State = DossierStateEnum.InReview;

        dossierReviewRepository.Setup(drr => drr.GetDossierWithApprovalStagesAndRequestsAndDiscussion(dossierId)).ReturnsAsync(dossier);

        await dossierReviewService.EditDossierDiscussionReview(dossierId, discussionMessage);
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public async Task EditDossierDiscussionReview_DossierIsNotPublishedYet_Throws()
    {
        var dossierId = Guid.NewGuid();
        var discussionMessage = TestData.GetSampleEditDossierDiscussionMessageDTO();
        var dossier = TestData.GetSampleDossier();
        dossier.Discussion = null!;
        dossier.State = DossierStateEnum.InReview;

        dossierReviewRepository.Setup(drr => drr.GetDossierWithApprovalStagesAndRequestsAndDiscussion(dossierId)).ReturnsAsync(dossier);

        await dossierReviewService.EditDossierDiscussionReview(dossierId, discussionMessage);
    }

    [TestMethod]
    [ExpectedException(typeof(BadRequestException))]
    public async Task EditDossierDiscussionReview_DiscussionMessageDoesNotBelongToUser_Throws()
    {
        var dossier = TestData.GetSampleDossierWithDiscussion();
        var discussionMessage = TestData.GetSampleDiscussionMessage();
        var newDiscussionMessage = TestData.GetSampleEditDossierDiscussionMessageDTO();

        userService.Setup(x => x.GetCurrentUserClaim(It.IsAny<string>())).Returns(Guid.NewGuid().ToString());
        dossierRepository.Setup(dr => dr.GetDossierByDossierId(dossier.Id)).ReturnsAsync(dossier);
        dossierRepository.Setup(dr => dr.UpdateDossier(dossier)).ReturnsAsync(true);
        dossierReviewRepository.Setup(drr => drr.GetDiscussionMessageWithId(It.IsAny<Guid>())).ReturnsAsync(discussionMessage);

        await dossierReviewService.EditDossierDiscussionReview(dossier.Id, newDiscussionMessage);

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task EditDossierDiscussionReview_DiscussionMessageNotExist_Throws()
    {
        var dossier = TestData.GetSampleDossierWithDiscussion();
        var discussionMessage = TestData.GetSampleDiscussionMessage();
        var newDiscussionMessage = TestData.GetSampleEditDossierDiscussionMessageDTO();

        userService.Setup(x => x.GetCurrentUserClaim(It.IsAny<string>())).Returns(discussionMessage.AuthorId.ToString());
        dossierRepository.Setup(dr => dr.GetDossierByDossierId(dossier.Id)).ReturnsAsync(dossier);
        dossierRepository.Setup(dr => dr.UpdateDossier(dossier)).ReturnsAsync(true);

        await dossierReviewService.EditDossierDiscussionReview(dossier.Id, newDiscussionMessage);
    }

    [TestMethod]
    public async Task EditDossierDiscussionReview_ValidInput_EditsMessageAndSaves()
    {
        var dossier = TestData.GetSampleDossierWithDiscussion();
        var discussionMessage = TestData.GetSampleDiscussionMessage();
        var newDiscussionMessage = TestData.GetSampleEditDossierDiscussionMessageDTO();

        userService.Setup(x => x.GetCurrentUserClaim(It.IsAny<string>())).Returns(discussionMessage.AuthorId.ToString());
        dossierRepository.Setup(dr => dr.GetDossierByDossierId(dossier.Id)).ReturnsAsync(dossier);
        dossierRepository.Setup(dr => dr.UpdateDossier(dossier)).ReturnsAsync(true);
       
        dossierReviewRepository.Setup(drr => drr.GetDiscussionMessageWithId(It.IsAny<Guid>())).ReturnsAsync(discussionMessage);
        dossierReviewRepository.Setup(drr => drr.UpdateDiscussionMessageReview(discussionMessage)).ReturnsAsync(true);

        await dossierReviewService.EditDossierDiscussionReview(dossier.Id, newDiscussionMessage);

        Assert.AreEqual(newDiscussionMessage.NewMessage, discussionMessage.Message);
    }

    [TestMethod]
    public async Task GetDossierWithApprovalStagesAndRequestsAndDiscussionOrThrow_DossierExists_ReturnsDossier()
    {
        var dossierId = Guid.NewGuid();
        var expectedDossier = TestData.GetSampleDossierWithDiscussion();

        dossierRepository.Setup(drr => drr.GetDossierByDossierId(dossierId)).ReturnsAsync(expectedDossier);

        var resultDossier = await dossierReviewService.GetDossierWithApprovalStagesAndRequestsAndDiscussionOrThrow(dossierId);

        Assert.IsNotNull(resultDossier);
        Assert.AreEqual(expectedDossier, resultDossier);
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public async Task GetDossierWithApprovalStagesAndRequestsAndDiscussionOrThrow_DossierDoesNotExist_ThrowsNotFoundException()
    {
        var dossierId = Guid.NewGuid();

        dossierReviewRepository.Setup(drr => drr.GetDossierWithApprovalStagesAndRequestsAndDiscussion(dossierId)).ReturnsAsync((Dossier)null!);

        await dossierReviewService.GetDossierWithApprovalStagesAndRequestsAndDiscussionOrThrow(dossierId);
    }

}
