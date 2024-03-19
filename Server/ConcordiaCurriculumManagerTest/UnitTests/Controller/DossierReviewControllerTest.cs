using AutoMapper;
using ConcordiaCurriculumManager.Controllers;
using ConcordiaCurriculumManager.DTO.Dossiers;
using ConcordiaCurriculumManager.DTO.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Services;
using ConcordiaCurriculumManagerTest.UnitTests.UtilityFunctions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ConcordiaCurriculumManagerTest.UnitTests.Controller;

[TestClass]
public class DossierReviewControllerTest
{
    private Mock<IDossierReviewService> dossierReviewService = null!;
    private Mock<IUserAuthenticationService> userAuthenticationService = null!;
    private Mock<IMapper> mapper = null!;
    private DossierReviewController dossierReviewController = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        dossierReviewService = new Mock<IDossierReviewService>();
        userAuthenticationService = new Mock<IUserAuthenticationService>();

        mapper = new Mock<IMapper>();

        dossierReviewController = new DossierReviewController(dossierReviewService.Object, mapper.Object, userAuthenticationService.Object);
    }

    [TestMethod]
    public async Task RejectDossier_ValidCall_ReturnsOk()
    {
        var user = TestData.GetSampleUser();

        userAuthenticationService.Setup(uas => uas.GetCurrentUser().Result).Returns(user);
        dossierReviewService.Setup(drs => drs.RejectDossier(It.IsAny<Guid>(), user)).Returns(Task.CompletedTask);
        var dossierId = Guid.NewGuid();

        var actionResult = await dossierReviewController.RejectDossier(dossierId);

        dossierReviewService.Verify(mock => mock.RejectDossier(dossierId, user), Times.Once());
    }

    [TestMethod]
    public async Task ReturnDossier_ValidCall_ReturnsOk()
    {
        var user = TestData.GetSampleUser();

        userAuthenticationService.Setup(uas => uas.GetCurrentUser().Result).Returns(user);
        dossierReviewService.Setup(drs => drs.ReturnDossier(It.IsAny<Guid>(), user)).Returns(Task.CompletedTask);
        var dossierId = Guid.NewGuid();

        var actionResult = await dossierReviewController.ReturnDossier(dossierId);

        dossierReviewService.Verify(mock => mock.ReturnDossier(dossierId, user), Times.Once());
    }

    [TestMethod]
    public async Task ForwardDossier_ValidCall_ReturnsOk()
    {
        var user = TestData.GetSampleUser();

        userAuthenticationService.Setup(uas => uas.GetCurrentUser().Result).Returns(user);
        dossierReviewService.Setup(drs => drs.ForwardDossier(It.IsAny<Guid>(), user)).Returns(Task.CompletedTask);
        var dossierId = Guid.NewGuid();

        var actionResult = await dossierReviewController.ForwardDossier(dossierId);

        dossierReviewService.Verify(mock => mock.ForwardDossier(dossierId, user), Times.Once());
    }

    [TestMethod]
    public async Task ReviewDossier_ValidCall_ReturnsNoContent()
    {
        var dossierId = Guid.NewGuid();
        var dossierMessageDTO = TestData.GetSampleCreateDossierDiscussionMessageDTO();

        var discussionMessage = new DiscussionMessage()
        {
            DossierDiscussionId = dossierId,
            Message = dossierMessageDTO.Message,
            GroupId = dossierMessageDTO.GroupId,
            AuthorId = Guid.NewGuid()
        };

        mapper.Setup(m => m.Map<DiscussionMessage>(dossierMessageDTO)).Returns(discussionMessage);
        dossierReviewService.Setup(drs => drs.AddDossierDiscussionReview(dossierId, It.IsAny<DiscussionMessage>())).Returns(Task.CompletedTask);

        var actionResult = await dossierReviewController.ReviewDossier(dossierId, dossierMessageDTO);

        dossierReviewService.Verify(mock => mock.AddDossierDiscussionReview(dossierId, It.IsAny<DiscussionMessage>()), Times.Once());
        Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
    }

    [TestMethod]
    public async Task EditReviewMessage_ValidCall_ReturnsNoContent()
    {
        var dossierId = Guid.NewGuid();
        var dossierMessageDTO = TestData.GetSampleDossierDiscussionMessageDTO();

        var discussionMessage = new EditDossierDiscussionMessageDTO()
        {
            NewMessage = "new message",
            DiscussionMessageId = dossierMessageDTO.Id
        };

        dossierReviewService.Setup(drs => drs.EditDossierDiscussionReview(dossierId, It.IsAny<EditDossierDiscussionMessageDTO>())).Returns(Task.CompletedTask);

        var actionResult = await dossierReviewController.EditReviewMessage(dossierId, discussionMessage);

        dossierReviewService.Verify(mock => mock.EditDossierDiscussionReview(dossierId, It.IsAny<EditDossierDiscussionMessageDTO>()), Times.Once());
        Assert.IsInstanceOfType(actionResult, typeof(NoContentResult));
    }


}
