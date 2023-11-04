using AutoMapper;
using ConcordiaCurriculumManager.Controllers;
using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using ConcordiaCurriculumManager.DTO.Courses;

namespace ConcordiaCurriculumManagerTest.UnitTests.Controller;

[TestClass]
public class CourseControllerTest
{
    private Mock<IMapper> _mapper = null!;
    private Mock<ILogger<CourseController>> _logger = null!;
    private Mock<ICourseService> _courseService = null!;
    private Mock<IUserAuthenticationService> _userAuthenticationService = null!;
    private CourseController _courseController = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _logger = new Mock<ILogger<CourseController>>();
        _mapper = new Mock<IMapper>();
        _courseService = new Mock<ICourseService>();
        _userAuthenticationService = new Mock<IUserAuthenticationService>();

        _courseController = new CourseController(_mapper.Object, _logger.Object, _courseService.Object, _userAuthenticationService.Object);
    }

    [TestMethod]
    public async Task InitiatiateCourseCreation_ValidCall_ReturnsData()
    {
        var user = GetSampleUser();
        var dossier = GetSampleDossier(user);
        var course = GetSampleCourse();
        var courseCreationInitiationDTO = GetSampleCourseCreationInitiationDTO(course, dossier);
        var courseCreationRequestDTO = GetSampleCourseCreationRequestDTO(course, dossier);
        var courseCreationRequest = GetSampleCourseCreationRequest(course, dossier);

        _userAuthenticationService.Setup(x => x.GetCurrentUserClaim(It.IsAny<string>())).Returns(user.Id.ToString);
        _courseService.Setup(x => x.InitiateCourseCreation(courseCreationInitiationDTO, user.Id)).ReturnsAsync(courseCreationRequest);
        _mapper.Setup(x => x.Map<CourseCreationRequestDTO>(It.IsAny<CourseCreationRequest>())).Returns(courseCreationRequestDTO);

        var actionResult = await _courseController.InitiateCourseCreation(courseCreationInitiationDTO);
        var objectResult = (ObjectResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.Created, objectResult.StatusCode);
        _courseService.Verify(service => service.InitiateCourseCreation(courseCreationInitiationDTO, user.Id), Times.Once);
    }

    [TestMethod]
    public async Task InitiatiateCourseCreation_InvalidCall_Returns400()
    {
        var user = GetSampleUser();
        var dossier = GetSampleDossier(user);
        var course = GetSampleCourse();
        var courseCreationInitiationDTO = GetSampleCourseCreationInitiationDTO(course, dossier);
        var courseCreationRequestDTO = GetSampleCourseCreationRequestDTO(course, dossier);
        var courseCreationRequest = GetSampleCourseCreationRequest(course, dossier);

        _userAuthenticationService.Setup(x => x.GetCurrentUserClaim(It.IsAny<string>())).Returns(user.Id.ToString);
        _courseService.Setup(x => x.InitiateCourseCreation(courseCreationInitiationDTO, user.Id)).Throws(new ArgumentException());

        var actionResult = await _courseController.InitiateCourseCreation(courseCreationInitiationDTO);
        var objectResult = (ObjectResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.BadRequest, objectResult.StatusCode);
        _courseService.Verify(service => service.InitiateCourseCreation(courseCreationInitiationDTO, user.Id), Times.Once);
    }

    [TestMethod]
    public async Task InitiatiateCourseCreation_ServerError_Returns500()
    {
        var user = GetSampleUser();
        var dossier = GetSampleDossier(user);
        var course = GetSampleCourse();
        var courseCreationInitiationDTO = GetSampleCourseCreationInitiationDTO(course, dossier);
        var courseCreationRequestDTO = GetSampleCourseCreationRequestDTO(course, dossier);
        var courseCreationRequest = GetSampleCourseCreationRequest(course, dossier);

        _userAuthenticationService.Setup(x => x.GetCurrentUserClaim(It.IsAny<string>())).Returns(user.Id.ToString);
        _courseService.Setup(x => x.InitiateCourseCreation(courseCreationInitiationDTO, user.Id)).Throws(new Exception());

        var actionResult = await _courseController.InitiateCourseCreation(courseCreationInitiationDTO);
        var objectResult = (ObjectResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.InternalServerError, objectResult.StatusCode);
        _courseService.Verify(service => service.InitiateCourseCreation(courseCreationInitiationDTO, user.Id), Times.Once);
    }

    [TestMethod]
    public async Task InitiatiateCourseModification_ValidCall_ReturnsData()
    {
        var user = GetSampleUser();
        var dossier = GetSampleDossier(user);
        var course = GetSampleCourse();
        var courseModificationInitiationDTO = GetSampleCourseModificationInitiationDTO(course, dossier);
        var courseModificationRequestDTO = GetSampleCourseModificationRequestDTO(course, dossier);
        var courseModificationRequest = GetSampleCourseModificationRequest(course, dossier);

        _userAuthenticationService.Setup(x => x.GetCurrentUserClaim(It.IsAny<string>())).Returns(user.Id.ToString);
        _courseService.Setup(x => x.InitiateCourseModification(courseModificationInitiationDTO, user.Id)).ReturnsAsync(courseModificationRequest);
        _mapper.Setup(x => x.Map<CourseModificationRequestDTO>(It.IsAny<CourseModificationRequest>())).Returns(courseModificationRequestDTO);

        var actionResult = await _courseController.InitiateCourseModification(courseModificationInitiationDTO);
        var objectResult = (ObjectResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.Created, objectResult.StatusCode);
        _courseService.Verify(service => service.InitiateCourseModification(courseModificationInitiationDTO, user.Id), Times.Once);
    }

    [TestMethod]
    public async Task InitiatiateCourseModification_InvalidCall_Returns400()
    {
        var user = GetSampleUser();
        var dossier = GetSampleDossier(user);
        var course = GetSampleCourse();
        var courseModificationInitiationDTO = GetSampleCourseModificationInitiationDTO(course, dossier);
        var courseModificationRequestDTO = GetSampleCourseModificationRequestDTO(course, dossier);
        var courseModificationRequest = GetSampleCourseModificationRequest(course, dossier);

        _userAuthenticationService.Setup(x => x.GetCurrentUserClaim(It.IsAny<string>())).Returns(user.Id.ToString);
        _courseService.Setup(x => x.InitiateCourseModification(courseModificationInitiationDTO, user.Id)).Throws(new ArgumentException());

        var actionResult = await _courseController.InitiateCourseModification(courseModificationInitiationDTO);
        var objectResult = (ObjectResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.BadRequest, objectResult.StatusCode);
        _courseService.Verify(service => service.InitiateCourseModification(courseModificationInitiationDTO, user.Id), Times.Once);
    }

    [TestMethod]
    public async Task InitiatiateCourseModification_ServerError_Returns500()
    {
        var user = GetSampleUser();
        var dossier = GetSampleDossier(user);
        var course = GetSampleCourse();
        var courseModificationInitiationDTO = GetSampleCourseModificationInitiationDTO(course, dossier);
        var courseModificationRequestDTO = GetSampleCourseModificationRequestDTO(course, dossier);
        var courseModificationRequest = GetSampleCourseModificationRequest(course, dossier);

        _userAuthenticationService.Setup(x => x.GetCurrentUserClaim(It.IsAny<string>())).Returns(user.Id.ToString);
        _courseService.Setup(x => x.InitiateCourseModification(courseModificationInitiationDTO, user.Id)).Throws(new Exception());

        var actionResult = await _courseController.InitiateCourseModification(courseModificationInitiationDTO);
        var objectResult = (ObjectResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.InternalServerError, objectResult.StatusCode);
        _courseService.Verify(service => service.InitiateCourseModification(courseModificationInitiationDTO, user.Id), Times.Once);
    }

    [TestMethod]
    public async Task InitiatiateCourseDeletion_ValidCall_ReturnsData()
    {
        var user = GetSampleUser();
        var dossier = GetSampleDossier(user);
        var course = GetSampleCourse();
        var courseDeletionInitiationDTO = GetSampleCourseDeletionInitiationDTO(course, dossier);
        var courseDeletionnRequestDTO = GetSampleCourseDeletionRequestDTO(course, dossier);
        var courseDeletionRequest = GetSampleCourseDeletionRequest(course, dossier);

        _userAuthenticationService.Setup(x => x.GetCurrentUserClaim(It.IsAny<string>())).Returns(user.Id.ToString);
        _courseService.Setup(x => x.InitiateCourseDeletion(courseDeletionInitiationDTO, user.Id)).ReturnsAsync(courseDeletionRequest);
        _mapper.Setup(x => x.Map<CourseDeletionRequestDTO>(It.IsAny<CourseDeletionRequest>())).Returns(courseDeletionnRequestDTO);

        var actionResult = await _courseController.InitiateCourseDeletion(courseDeletionInitiationDTO);
        var objectResult = (ObjectResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.Created, objectResult.StatusCode);
        _courseService.Verify(service => service.InitiateCourseDeletion(courseDeletionInitiationDTO, user.Id), Times.Once);
    }

    [TestMethod]
    public async Task InitiatiateCourseDeletion_InvalidCall_Returns400()
    {
        var user = GetSampleUser();
        var dossier = GetSampleDossier(user);
        var course = GetSampleCourse();
        var courseDeletionInitiationDTO = GetSampleCourseDeletionInitiationDTO(course, dossier);
        var courseDeletionRequestDTO = GetSampleCourseDeletionRequestDTO(course, dossier);
        var courseDeletionRequest = GetSampleCourseDeletionRequest(course, dossier);

        _userAuthenticationService.Setup(x => x.GetCurrentUserClaim(It.IsAny<string>())).Returns(user.Id.ToString);
        _courseService.Setup(x => x.InitiateCourseDeletion(courseDeletionInitiationDTO, user.Id)).Throws(new ArgumentException());

        var actionResult = await _courseController.InitiateCourseDeletion(courseDeletionInitiationDTO);
        var objectResult = (ObjectResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.BadRequest, objectResult.StatusCode);
        _courseService.Verify(service => service.InitiateCourseDeletion(courseDeletionInitiationDTO, user.Id), Times.Once);
    }

    [TestMethod]
    public async Task InitiatiateCourseDeletion_ServerError_Returns500()
    {
        var user = GetSampleUser();
        var dossier = GetSampleDossier(user);
        var course = GetSampleCourse();
        var courseDeletionInitiationDTO = GetSampleCourseDeletionInitiationDTO(course, dossier);
        var courseDeletionRequestDTO = GetSampleCourseDeletionRequestDTO(course, dossier);
        var courseDeletionRequest = GetSampleCourseDeletionRequest(course, dossier);

        _userAuthenticationService.Setup(x => x.GetCurrentUserClaim(It.IsAny<string>())).Returns(user.Id.ToString);
        _courseService.Setup(x => x.InitiateCourseDeletion(courseDeletionInitiationDTO, user.Id)).Throws(new Exception());

        var actionResult = await _courseController.InitiateCourseDeletion(courseDeletionInitiationDTO);
        var objectResult = (ObjectResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.InternalServerError, objectResult.StatusCode);
        _courseService.Verify(service => service.InitiateCourseDeletion(courseDeletionInitiationDTO, user.Id), Times.Once);
    }

    [TestMethod]
    public async Task GetCourseData_ValidCall_ReturnsData()
    {
        var subject = "SOEN";
        var catalog = "490";
        var course = GetSampleCourse();
       _courseService.Setup(cr => cr.GetCourseData(subject, catalog)).ReturnsAsync(course);

        var actionResult = await _courseController.GetCourseData(subject, catalog);
        var objectResult = (ObjectResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
        _mapper.Verify(mock => mock.Map<CourseDataDTO>(course), Times.Once());
    }

    [TestMethod]
    public async Task GetCourseData_ServerError_Returns500()
    {
        _courseService.Setup(cr => cr.GetCourseData(It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());

        var actionResult = await _courseController.GetCourseData(It.IsAny<string>(), It.IsAny<string>());
        var objectResult = (ObjectResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.InternalServerError, objectResult.StatusCode);
    }

    [TestMethod]
    public async Task EditCourseCreationRequest_ValidCall_ReturnsData()
    {
        var user = GetSampleUser();
        var dossier = GetSampleDossier(user);
        var course = GetSampleCourse();
        var courseCreationRequestDTO = GetSampleCourseCreationRequestDTO(course, dossier);
        var courseCreationRequest = GetSampleCourseCreationRequest(course, dossier);
        var editCourseCreationRequestDTO = GetSampleEditCourseCreationRequestDTO();
;
        _courseService.Setup(x => x.EditCourseCreationRequest(editCourseCreationRequestDTO)).ReturnsAsync(courseCreationRequest);
        _mapper.Setup(x => x.Map<CourseCreationRequestDTO>(It.IsAny<CourseCreationRequest>())).Returns(courseCreationRequestDTO);

        var actionResult = await _courseController.EditCourseCreationRequest(editCourseCreationRequestDTO);
        var objectResult = (ObjectResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
        _courseService.Verify(service => service.EditCourseCreationRequest(editCourseCreationRequestDTO), Times.Once);
    }

    [TestMethod]
    public async Task EditCourseCreationRequest_InvalidCall_Returns400()
    {
        var editCourseCreationRequestDTO = GetSampleEditCourseCreationRequestDTO();

        _courseService.Setup(x => x.EditCourseCreationRequest(editCourseCreationRequestDTO)).Throws(new ArgumentException());

        var actionResult = await _courseController.EditCourseCreationRequest(editCourseCreationRequestDTO);
        var objectResult = (ObjectResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.BadRequest, objectResult.StatusCode);
        _courseService.Verify(service => service.EditCourseCreationRequest(editCourseCreationRequestDTO), Times.Once);
    }

    [TestMethod]
    public async Task EditCourseCreationRequest_ServerError_Returns500()
    {
        var editCourseCreationRequestDTO = GetSampleEditCourseCreationRequestDTO();

        _courseService.Setup(x => x.EditCourseCreationRequest(editCourseCreationRequestDTO)).Throws(new Exception());

        var actionResult = await _courseController.EditCourseCreationRequest(editCourseCreationRequestDTO);
        var objectResult = (ObjectResult)actionResult;
        
        Assert.AreEqual((int)HttpStatusCode.InternalServerError, objectResult.StatusCode);
        _courseService.Verify(service => service.EditCourseCreationRequest(editCourseCreationRequestDTO), Times.Once);
    }

    private User GetSampleUser()
    {
        return new User
        {
            Id = new Guid(),
            FirstName = "Joe",
            LastName = "Smith",
            Email = "jsmith@ccm.com",
            Password = "Password123!"
        };
    }

    private Dossier GetSampleDossier(User user)
    {
        return new Dossier
        {
            Initiator = user,
            InitiatorId = user.Id,
            Title = "Dossier 1",
            Description = "Text description of a dossier.",
            Published = false,
        };
    }

    private Course GetSampleCourse()
    {
        var id = Guid.NewGuid();

        return new Course
        {
            Id = id,
            CourseID = 1000,
            Subject = "SOEN",
            Catalog = "490",
            Title = "Capstone",
            Description = "Curriculum manager building simulator",
            CreditValue = "6",
            PreReqs = "SOEN 390",
            CourseNotes = "Lots of fun",
            Career = CourseCareerEnum.UGRD,
            EquivalentCourses = "",
            CourseState = CourseStateEnum.NewCourseProposal,
            Version = 1,
            Published = true,
            CourseCourseComponents = CourseCourseComponent.GetComponentCodeMapping(new Dictionary<ComponentCodeEnum, int?> { { ComponentCodeEnum.LEC, 3 } }, id)
        };
    }

    private CourseCreationInitiationDTO GetSampleCourseCreationInitiationDTO(Course course, Dossier dossier)
    {
        return new CourseCreationInitiationDTO
        {
            Subject = "SOEN",
            Catalog = "490",
            Title = "Capstone",
            Description = "Curriculum manager building simulator",
            CreditValue = "6",
            PreReqs = "SOEN 390",
            CourseNotes = "Fun",
            Rationale = "It's necessary",
            ResourceImplication = "New prof needed",
            Career = CourseCareerEnum.UGRD,
            EquivalentCourses = "",
            ComponentCodes = new Dictionary<ComponentCodeEnum, int?> { { ComponentCodeEnum.LEC, 3 } },
            SupportingFiles = new Dictionary<string, string> { { "name.pdf", "base64content" } },
            DossierId = dossier.Id
        };
    }

    private CourseCreationRequestDTO GetSampleCourseCreationRequestDTO(Course course, Dossier dossier)
    {
        return new CourseCreationRequestDTO
        {
            Id = Guid.NewGuid(),
            DossierId = dossier.Id,
            NewCourseId = course.Id,
            Rationale = "It's necessary",
            ResourceImplication = "New prof needed",
            Comment = "Fun",
        };
    }

    private CourseCreationRequest GetSampleCourseCreationRequest(Course course, Dossier dossier)
    {
        return new CourseCreationRequest
        {
            DossierId = dossier.Id,
            NewCourseId = course.Id,
            Rationale = "It's necessary",
            ResourceImplication = "New prof needed",
            Comment = "Fun",
        };
    }

    private CourseModificationInitiationDTO GetSampleCourseModificationInitiationDTO(Course course, Dossier dossier)
    {
        return new CourseModificationInitiationDTO
        {
            Subject = "SOEN",
            Catalog = "490",
            Title = "Capstone",
            Description = "Curriculum manager building simulator",
            CreditValue = "6",
            PreReqs = "SOEN 390",
            CourseNotes = "Fun",
            Rationale = "It's necessary",
            ResourceImplication = "New prof needed",
            Career = CourseCareerEnum.UGRD,
            EquivalentCourses = "",
            ComponentCodes = new Dictionary<ComponentCodeEnum, int?> { { ComponentCodeEnum.LEC, 3 } },
            SupportingFiles = new Dictionary<string, string> { { "name.pdf", "base64content" } },
            DossierId = dossier.Id,
            CourseId = 1,
        };
    }

    private CourseModificationRequestDTO GetSampleCourseModificationRequestDTO(Course course, Dossier dossier)
    {
        return new CourseModificationRequestDTO
        {
            Id = Guid.NewGuid(),
            DossierId = dossier.Id,
            CourseId = course.Id,
            Rationale = "It's necessary",
            ResourceImplication = "New prof needed",
            Comment = "Fun",
        };
    }

    private CourseModificationRequest GetSampleCourseModificationRequest(Course course, Dossier dossier)
    {
        return new CourseModificationRequest
        {
            DossierId = dossier.Id,
            CourseId = course.Id,
            Rationale = "It's necessary",
            ResourceImplication = "New prof needed",
            Comment = "Fun",
        };
    }

    private CourseDeletionInitiationDTO GetSampleCourseDeletionInitiationDTO(Course course, Dossier dossier)
    {
        return new CourseDeletionInitiationDTO
        {
            Rationale = "It's necessary",
            ResourceImplication = "New prof needed",
            DossierId = dossier.Id,
            Subject = "SOEN",
            Catalog = "490"
        };
    }

    private CourseDeletionRequestDTO GetSampleCourseDeletionRequestDTO(Course course, Dossier dossier)
    {
        return new CourseDeletionRequestDTO
        {
            Id = Guid.NewGuid(),
            DossierId = dossier.Id,
            CourseId = course.Id,
            Rationale = "It's necessary",
            ResourceImplication = "New prof needed",
            Comment = "Fun",
        };
    }

    private CourseDeletionRequest GetSampleCourseDeletionRequest(Course course, Dossier dossier)
    {
        return new CourseDeletionRequest
        {
            DossierId = dossier.Id,
            CourseId = course.Id,
            Rationale = "It's necessary",
            ResourceImplication = "New prof needed",
            Comment = "Fun",
        };
    }

    private EditCourseCreationRequestDTO GetSampleEditCourseCreationRequestDTO()
    {
        return new EditCourseCreationRequestDTO
        {
            Id = Guid.NewGuid(),
            DossierId = Guid.NewGuid(),
            Rationale = "It's necessary",
            ResourceImplication = "New prof needed",
            Subject = "SOEN Modified3",
            Catalog = "500",
            Title = "Super Capstone for Software Engineers",
            Description = "An advanced capstone project for final year software engineering students.",
            CourseNotes = "Students are required to present their project at the end of the semester.",
            CreditValue = "6.5",
            PreReqs = "SOEN 490 previously or concurrently",
            Career = CourseCareerEnum.UGRD,
            EquivalentCourses = "SOEN 499",
            ComponentCodes = new Dictionary<ComponentCodeEnum, int?> { { ComponentCodeEnum.LEC, 3 } },
            SupportingFiles = new Dictionary<string, string> { { "name.pdf", "base64content" } },
        };
    }

}
