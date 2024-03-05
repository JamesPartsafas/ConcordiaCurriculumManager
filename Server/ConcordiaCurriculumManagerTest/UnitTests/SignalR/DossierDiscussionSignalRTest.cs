using AutoMapper;
using ConcordiaCurriculumManager.DTO.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Security;
using ConcordiaCurriculumManager.Services;
using ConcordiaCurriculumManager.SignalR;
using ConcordiaCurriculumManagerTest.UnitTests.UtilityFunctions;
using Microsoft.AspNetCore.SignalR;
using Moq;
using System.Security.Claims;

namespace ConcordiaCurriculumManagerTest.UnitTests.SignalR;

[TestClass]
public class DossierDiscussionSignalRTest
{
    private DossierDiscussionSignalR _dossierDiscussionSignalR = null!;
    private Mock<IMapper> _mapperMock = null!;
    private Mock<IDossierReviewService> _dossierReviewServiceMock = null!;
    private Mock<IHubCallerClients> _hubCallerClientsMock = null!;
    private Mock<HubCallerContext> _hubCallerContextMock = null!;
    private Mock<IClientProxy> _mockClientProxy = null!;
    private Mock<ISingleClientProxy> _mockSingleClientProxy = null!;

    [TestInitialize]
    public void Initialize()
    {
        _mapperMock = new Mock<IMapper>();
        _dossierReviewServiceMock = new Mock<IDossierReviewService>();
        _hubCallerClientsMock = new Mock<IHubCallerClients>();
        _hubCallerContextMock = new Mock<HubCallerContext>();
        _mockClientProxy = new Mock<IClientProxy>();
        _mockSingleClientProxy = new Mock<ISingleClientProxy>();

        _hubCallerClientsMock.Setup(clients => clients.All).Returns(_mockClientProxy.Object);
        _hubCallerClientsMock.Setup(clients => clients.Client(It.IsAny<string>())).Returns(_mockSingleClientProxy.Object);

        _dossierDiscussionSignalR = new DossierDiscussionSignalR(_mapperMock.Object, _dossierReviewServiceMock.Object)
        {
            Context = _hubCallerContextMock.Object,
            Clients = _hubCallerClientsMock.Object
        };
    }

    [TestMethod]
    public async Task ReviewDossier_WithValidData_CallsAddDossierDiscussionReviewAndSendsMessage()
    {
        var dossierId = Guid.NewGuid();
        var dossierMessageDTO = TestData.GetSampleCreateDossierDiscussionMessageDTO();
        var outputDTO = TestData.GetSampleDossierDiscussionMessageDTO();
        var userId = Guid.NewGuid().ToString();

        var discussionMessage = new DiscussionMessage()
        {
            DossierDiscussionId = dossierId,
            Message = dossierMessageDTO.Message,
            GroupId = dossierMessageDTO.GroupId,
            AuthorId = Guid.NewGuid()
        };

        _hubCallerContextMock.SetupGet(x => x.User)
            .Returns(new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new(Claims.Id, userId)
            })));

        var items = new Dictionary<object, object?>()
        {
            { "dossierId", dossierId }
        };

        _mapperMock.Setup(m => m.Map<DiscussionMessage>(dossierMessageDTO)).Returns(discussionMessage);
        _dossierReviewServiceMock.Setup(d => d.AddDossierDiscussionReview(It.IsAny<Guid>(), It.IsAny<DiscussionMessage>(), It.IsAny<Guid>())).Returns(Task.CompletedTask);
        _mapperMock.Setup(m => m.Map<DossierDiscussionMessageDTO>(It.IsAny<DiscussionMessage>())).Returns(outputDTO);
        _hubCallerContextMock.SetupGet(x => x.ConnectionId).Returns("connectionId");
        _hubCallerContextMock.SetupGet(x => x.Items).Returns(items);

        await _dossierDiscussionSignalR.ReviewDossier(dossierId, dossierMessageDTO);

        _dossierReviewServiceMock.Verify(d => d.AddDossierDiscussionReview(dossierId, It.IsAny<DiscussionMessage>(), It.IsAny<Guid>()), Times.Once);
        _mapperMock.Verify(m => m.Map<DossierDiscussionMessageDTO>(It.IsAny<DiscussionMessage>()), Times.Once);
        _hubCallerClientsMock.Verify(c => c.All.SendCoreAsync(nameof(DossierDiscussionSignalR.ReviewDossier), It.IsAny<object[]>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task ReviewDossier_WithoutUserId_DoesNotCallAddDossierDiscussionReviewAndSendsErrorMessage()
    {
        var dossierId = Guid.NewGuid();
        var dossierMessageDTO = TestData.GetSampleCreateDossierDiscussionMessageDTO();
        var outputDTO = TestData.GetSampleDossierDiscussionMessageDTO();
        var userId = Guid.NewGuid().ToString();

        var discussionMessage = new DiscussionMessage()
        {
            DossierDiscussionId = dossierId,
            Message = dossierMessageDTO.Message,
            GroupId = dossierMessageDTO.GroupId,
            AuthorId = Guid.NewGuid()
        };

        _hubCallerContextMock.SetupGet(x => x.User)
            .Returns(new ClaimsPrincipal(new ClaimsIdentity(Array.Empty<Claim>())));

        var items = new Dictionary<object, object?>()
        {
            { "dossierId", dossierId }
        };

        _mapperMock.Setup(m => m.Map<DiscussionMessage>(dossierMessageDTO)).Returns(discussionMessage);
        _dossierReviewServiceMock.Setup(d => d.AddDossierDiscussionReview(It.IsAny<Guid>(), It.IsAny<DiscussionMessage>(), It.IsAny<Guid>())).Returns(Task.CompletedTask);
        _mapperMock.Setup(m => m.Map<DossierDiscussionMessageDTO>(It.IsAny<DiscussionMessage>())).Returns(outputDTO);
        _hubCallerContextMock.SetupGet(x => x.ConnectionId).Returns("connectionId");
        _hubCallerContextMock.SetupGet(x => x.Items).Returns(items);

        await _dossierDiscussionSignalR.ReviewDossier(dossierId, dossierMessageDTO);

        _dossierReviewServiceMock.Verify(d => d.AddDossierDiscussionReview(dossierId, It.IsAny<DiscussionMessage>(), It.IsAny<Guid>()), Times.Never);
        _mapperMock.Verify(m => m.Map<DossierDiscussionMessageDTO>(It.IsAny<DiscussionMessage>()), Times.Never);
        _hubCallerClientsMock.Verify(c => c.Client("connectionId").SendCoreAsync(It.Is<string>(str => str.ToLowerInvariant().Contains("error")), It.IsAny<object[]>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task ReviewDossier_WithInValidInput_DoesNotCallAddDossierDiscussionReviewAndSendsErrorMessage()
    {
        var dossierId = Guid.NewGuid();
        var dossierMessageDTO = TestData.GetSampleCreateDossierDiscussionMessageDTO();
        var outputDTO = TestData.GetSampleDossierDiscussionMessageDTO();
        var userId = Guid.NewGuid().ToString();

        _hubCallerContextMock.SetupGet(x => x.User)
            .Returns(new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new(Claims.Id, userId)
            })));

        var items = new Dictionary<object, object?>()
        {
            { "dossierId", dossierId }
        };

        _mapperMock.Setup(m => m.Map<DiscussionMessage>(dossierMessageDTO)).Returns((DiscussionMessage)null!);
        _dossierReviewServiceMock.Setup(d => d.AddDossierDiscussionReview(It.IsAny<Guid>(), It.IsAny<DiscussionMessage>(), It.IsAny<Guid>())).Returns(Task.CompletedTask);
        _mapperMock.Setup(m => m.Map<DossierDiscussionMessageDTO>(It.IsAny<DiscussionMessage>())).Returns(outputDTO);
        _hubCallerContextMock.SetupGet(x => x.ConnectionId).Returns("connectionId");
        _hubCallerContextMock.SetupGet(x => x.Items).Returns(items);

        await _dossierDiscussionSignalR.ReviewDossier(dossierId, dossierMessageDTO);

        _dossierReviewServiceMock.Verify(d => d.AddDossierDiscussionReview(dossierId, It.IsAny<DiscussionMessage>(), It.IsAny<Guid>()), Times.Never);
        _mapperMock.Verify(m => m.Map<DossierDiscussionMessageDTO>(It.IsAny<DiscussionMessage>()), Times.Never);
        _hubCallerClientsMock.Verify(c => c.Client("connectionId").SendCoreAsync(It.Is<string>(str => str.ToLowerInvariant().Contains("error")), It.IsAny<object[]>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task ReviewDossier_WithDifferentDossierId_DoesNotCallAddDossierDiscussionReviewAndSendsErrorMessage()
    {
        var dossierId = Guid.NewGuid();
        var dossierMessageDTO = TestData.GetSampleCreateDossierDiscussionMessageDTO();
        var outputDTO = TestData.GetSampleDossierDiscussionMessageDTO();
        var userId = Guid.NewGuid().ToString();

        var discussionMessage = new DiscussionMessage()
        {
            DossierDiscussionId = dossierId,
            Message = dossierMessageDTO.Message,
            GroupId = dossierMessageDTO.GroupId,
            AuthorId = Guid.NewGuid()
        };

        _hubCallerContextMock.SetupGet(x => x.User)
            .Returns(new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new(Claims.Id, userId)
            })));

        var items = new Dictionary<object, object?>()
        {
            { "dossierId", dossierId }
        };

        _mapperMock.Setup(m => m.Map<DiscussionMessage>(dossierMessageDTO)).Returns(discussionMessage);
        _dossierReviewServiceMock.Setup(d => d.AddDossierDiscussionReview(It.IsAny<Guid>(), It.IsAny<DiscussionMessage>(), It.IsAny<Guid>())).Returns(Task.CompletedTask);
        _mapperMock.Setup(m => m.Map<DossierDiscussionMessageDTO>(It.IsAny<DiscussionMessage>())).Returns(outputDTO);
        _hubCallerContextMock.SetupGet(x => x.ConnectionId).Returns("connectionId");
        _hubCallerContextMock.SetupGet(x => x.Items).Returns(items);

        await _dossierDiscussionSignalR.ReviewDossier(Guid.NewGuid(), dossierMessageDTO);

        _dossierReviewServiceMock.Verify(d => d.AddDossierDiscussionReview(dossierId, It.IsAny<DiscussionMessage>(), It.IsAny<Guid>()), Times.Never);
        _mapperMock.Verify(m => m.Map<DossierDiscussionMessageDTO>(It.IsAny<DiscussionMessage>()), Times.Never);
        _hubCallerClientsMock.Verify(c => c.Client("connectionId").SendCoreAsync(It.Is<string>(str => str.ToLowerInvariant().Contains("error")), It.IsAny<object[]>(), It.IsAny<CancellationToken>()), Times.Once);
    }


    [TestMethod]
    public async Task ReviewDossier_WithInvalidOutput_SendsErrorMessageToEveryone()
    {
        var dossierId = Guid.NewGuid();
        var dossierMessageDTO = TestData.GetSampleCreateDossierDiscussionMessageDTO();
        var outputDTO = TestData.GetSampleDossierDiscussionMessageDTO();
        var userId = Guid.NewGuid().ToString();

        var discussionMessage = new DiscussionMessage()
        {
            DossierDiscussionId = dossierId,
            Message = dossierMessageDTO.Message,
            GroupId = dossierMessageDTO.GroupId,
            AuthorId = Guid.NewGuid()
        };

        _hubCallerContextMock.SetupGet(x => x.User)
            .Returns(new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new(Claims.Id, userId)
            })));

        var items = new Dictionary<object, object?>()
        {
            { "dossierId", dossierId }
        };

        _mapperMock.Setup(m => m.Map<DiscussionMessage>(dossierMessageDTO)).Returns(discussionMessage);
        _dossierReviewServiceMock.Setup(d => d.AddDossierDiscussionReview(It.IsAny<Guid>(), It.IsAny<DiscussionMessage>(), It.IsAny<Guid>())).Returns(Task.CompletedTask);
        _mapperMock.Setup(m => m.Map<DossierDiscussionMessageDTO>(It.IsAny<DiscussionMessage>())).Returns((DossierDiscussionMessageDTO)null!);
        _hubCallerContextMock.SetupGet(x => x.ConnectionId).Returns("connectionId");
        _hubCallerContextMock.SetupGet(x => x.Items).Returns(items);

        await _dossierDiscussionSignalR.ReviewDossier(dossierId, dossierMessageDTO);

        _dossierReviewServiceMock.Verify(d => d.AddDossierDiscussionReview(dossierId, It.IsAny<DiscussionMessage>(), It.IsAny<Guid>()), Times.Once);
        _mapperMock.Verify(m => m.Map<DossierDiscussionMessageDTO>(It.IsAny<DiscussionMessage>()), Times.Once);
        _hubCallerClientsMock.Verify(c => c.All.SendCoreAsync(It.Is<string>(str => str.ToLowerInvariant().Contains("error")), It.IsAny<object[]>(), It.IsAny<CancellationToken>()), Times.Once);
    }

}
