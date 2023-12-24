using AutoMapper;
using ConcordiaCurriculumManager.Controllers;
using ConcordiaCurriculumManager.DTO.Dossiers;
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
    private Mock<IMapper> mapper = null!;
    private DossierReviewController dossierReviewController = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        dossierReviewService = new Mock<IDossierReviewService>();
        mapper = new Mock<IMapper>();

        dossierReviewController = new DossierReviewController(dossierReviewService.Object, mapper.Object);
    }

    [TestMethod]
    public async Task RejectDossier_ValidCall_ReturnsOk()
    {
        dossierReviewService.Setup(drs => drs.RejectDossier(It.IsAny<Guid>())).Returns(Task.CompletedTask);
        var dossierId = Guid.NewGuid();

        var actionResult = await dossierReviewController.RejectDossier(dossierId);

        dossierReviewService.Verify(mock => mock.RejectDossier(dossierId), Times.Once());
    }

    [TestMethod]
    public async Task ReturnDossier_ValidCall_ReturnsOk()
    {
        dossierReviewService.Setup(drs => drs.ReturnDossier(It.IsAny<Guid>())).Returns(Task.CompletedTask);
        var dossierId = Guid.NewGuid();

        var actionResult = await dossierReviewController.ReturnDossier(dossierId);

        dossierReviewService.Verify(mock => mock.ReturnDossier(dossierId), Times.Once());
    }

    [TestMethod]
    public async Task ForwardDossier_ValidCall_ReturnsOk()
    {
        dossierReviewService.Setup(drs => drs.ForwardDossier(It.IsAny<Guid>())).Returns(Task.CompletedTask);
        var dossierId = Guid.NewGuid();

        var actionResult = await dossierReviewController.ForwardDossier(dossierId);

        dossierReviewService.Verify(mock => mock.ForwardDossier(dossierId), Times.Once());
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
    public async Task GetAllDossierReviews_ValidCall_ReturnsOkWithDossierDetails()
    {
        var dossierId = Guid.NewGuid();
        var dossier = TestData.GetSampleDossier();
        var dossierDetailsDTO = TestData.GetSampleDossierDetailsWithDiscussionDTO();

        dossierReviewService.Setup(drs => drs.GetDossierWithDiscussion(dossierId)).ReturnsAsync(dossier);
        mapper.Setup(m => m.Map<DossierDetailsWithDiscussionDTO>(dossier)).Returns(dossierDetailsDTO);

        var actionResult = await dossierReviewController.GetAllDossierReviews(dossierId);

        dossierReviewService.Verify(mock => mock.GetDossierWithDiscussion(dossierId), Times.Once());
        mapper.Verify(mock => mock.Map<DossierDetailsWithDiscussionDTO>(dossier), Times.Once());

        var okObjectResult = actionResult as OkObjectResult;
        Assert.IsNotNull(okObjectResult);
        Assert.AreEqual(StatusCodes.Status200OK, okObjectResult.StatusCode);
        Assert.IsNotNull(okObjectResult.Value);
        Assert.IsInstanceOfType(okObjectResult.Value, typeof(DossierDetailsWithDiscussionDTO));
    }

}
