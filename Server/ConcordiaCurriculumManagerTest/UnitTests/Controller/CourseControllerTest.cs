using AutoMapper;
using ConcordiaCurriculumManager.Controllers;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using ConcordiaCurriculumManager.DTO.Courses;
using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.OutputDTOs;
using ConcordiaCurriculumManagerTest.UnitTests.UtilityFunctions;
using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.InputDTOs;
using ConcordiaCurriculumManager.Repositories;
using ConcordiaCurriculumManager.Models.Curriculum;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Utilities;
using ConcordiaCurriculumManager.Models.Users;

namespace ConcordiaCurriculumManagerTest.UnitTests.Controller;

[TestClass]
public class CourseControllerTest
{
    private Mock<IMapper> _mapper = null!;
    private Mock<ILogger<CourseController>> _logger = null!;
    private Mock<ICourseService> _courseService = null!;
    private Mock<IUserAuthenticationService> _userAuthenticationService = null!;
    private CourseController _courseController = null!;
    private Mock<IDossierService> _dossierService = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _logger = new Mock<ILogger<CourseController>>();
        _mapper = new Mock<IMapper>();
        _courseService = new Mock<ICourseService>();
        _userAuthenticationService = new Mock<IUserAuthenticationService>();
        _dossierService = new Mock<IDossierService>();

        _courseController = new CourseController(_mapper.Object, _logger.Object, _courseService.Object, _userAuthenticationService.Object, _dossierService.Object);
    }

    [TestMethod]
    public async Task InitiatiateCourseCreation_ValidCall_ReturnsData()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        var course = TestData.GetSampleCourse();
        var courseCreationInitiationDTO = TestData.GetSampleCourseCreationInitiationDTO(dossier);
        var courseCreationRequestDTO = TestData.GetSampleCourseCreationRequestDTO(course, dossier);
        var courseCreationRequest = TestData.GetSampleCourseCreationRequest(dossier, course);

        _userAuthenticationService.Setup(x => x.GetCurrentUserClaim(It.IsAny<string>())).Returns(user.Id.ToString);
        _courseService.Setup(x => x.InitiateCourseCreation(courseCreationInitiationDTO, user.Id)).ReturnsAsync(courseCreationRequest);
        _mapper.Setup(x => x.Map<CourseCreationRequestDTO>(It.IsAny<CourseCreationRequest>())).Returns(courseCreationRequestDTO);

        var actionResult = await _courseController.InitiateCourseCreation(courseCreationInitiationDTO);
        var objectResult = (ObjectResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.Created, objectResult.StatusCode);
        _courseService.Verify(service => service.InitiateCourseCreation(courseCreationInitiationDTO, user.Id), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(BadRequestException))]
    public async Task InitiatiateCourseCreation_InvalidCall_ThrowsException()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        var course = TestData.GetSampleCourse();
        var courseCreationInitiationDTO = TestData.GetSampleCourseCreationInitiationDTO(dossier);
        var courseCreationRequestDTO = TestData.GetSampleCourseCreationRequestDTO(course, dossier);
        var courseCreationRequest = TestData.GetSampleCourseCreationRequest(dossier, course);

        _userAuthenticationService.Setup(x => x.GetCurrentUserClaim(It.IsAny<string>())).Returns(user.Id.ToString);
        _courseService.Setup(x => x.InitiateCourseCreation(courseCreationInitiationDTO, user.Id)).Throws(new BadRequestException());

        await _courseController.InitiateCourseCreation(courseCreationInitiationDTO);
        _courseService.Verify(service => service.InitiateCourseCreation(courseCreationInitiationDTO, user.Id), Times.Once);
    }

    [TestMethod]
    public async Task InitiatiateCourseModification_ValidCall_ReturnsData()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        var course = TestData.GetSampleCourse();
        var courseModificationInitiationDTO = TestData.GetSampleCourseCreationModificationDTO(course, dossier);
        var courseModificationRequestDTO = TestData.GetSampleCourseModificationRequestDTO(course, dossier);
        var courseModificationRequest = TestData.GetSampleCourseModificationRequest(dossier, course);

        _userAuthenticationService.Setup(x => x.GetCurrentUserClaim(It.IsAny<string>())).Returns(user.Id.ToString);
        _courseService.Setup(x => x.InitiateCourseModification(courseModificationInitiationDTO, user.Id)).ReturnsAsync(courseModificationRequest);
        _mapper.Setup(x => x.Map<CourseModificationRequestDTO>(It.IsAny<CourseModificationRequest>())).Returns(courseModificationRequestDTO);

        var actionResult = await _courseController.InitiateCourseModification(courseModificationInitiationDTO);
        var objectResult = (ObjectResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.Created, objectResult.StatusCode);
        _courseService.Verify(service => service.InitiateCourseModification(courseModificationInitiationDTO, user.Id), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public async Task InitiatiateCourseModification_InvalidCall_ThrowsException()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        var course = TestData.GetSampleCourse();
        var courseModificationInitiationDTO = TestData.GetSampleCourseCreationModificationDTO(course, dossier);
        var courseModificationRequestDTO = TestData.GetSampleCourseModificationRequestDTO(course, dossier);
        var courseModificationRequest = TestData.GetSampleCourseModificationRequest(dossier, course);

        _userAuthenticationService.Setup(x => x.GetCurrentUserClaim(It.IsAny<string>())).Returns(user.Id.ToString);
        _courseService.Setup(x => x.InitiateCourseModification(courseModificationInitiationDTO, user.Id)).Throws(new NotFoundException());

        await _courseController.InitiateCourseModification(courseModificationInitiationDTO);
        _courseService.Verify(service => service.InitiateCourseModification(courseModificationInitiationDTO, user.Id), Times.Once);
    }

    [TestMethod]
    public async Task InitiatiateCourseDeletion_ValidCall_ReturnsData()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        var course = TestData.GetSampleCourse();
        var courseDeletionInitiationDTO = TestData.GetSampleCourseCreationDeletionDTO(course, dossier);
        var courseDeletionnRequestDTO = TestData.GetSampleCourseDeletionRequestDTO(course, dossier);
        var courseDeletionRequest = TestData.GetSampleCourseDeletionRequest();

        _userAuthenticationService.Setup(x => x.GetCurrentUserClaim(It.IsAny<string>())).Returns(user.Id.ToString);
        _courseService.Setup(x => x.InitiateCourseDeletion(courseDeletionInitiationDTO, user.Id)).ReturnsAsync(courseDeletionRequest);
        _mapper.Setup(x => x.Map<CourseDeletionRequestDTO>(It.IsAny<CourseDeletionRequest>())).Returns(courseDeletionnRequestDTO);

        var actionResult = await _courseController.InitiateCourseDeletion(courseDeletionInitiationDTO);
        var objectResult = (ObjectResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.Created, objectResult.StatusCode);
        _courseService.Verify(service => service.InitiateCourseDeletion(courseDeletionInitiationDTO, user.Id), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public async Task InitiatiateCourseDeletion_InvalidCall_ThrowsException()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        var course = TestData.GetSampleCourse();
        var courseDeletionInitiationDTO = TestData.GetSampleCourseCreationDeletionDTO(course, dossier);
        var courseDeletionnRequestDTO = TestData.GetSampleCourseDeletionRequestDTO(course, dossier);
        var courseDeletionRequest = TestData.GetSampleCourseDeletionRequest();

        _userAuthenticationService.Setup(x => x.GetCurrentUserClaim(It.IsAny<string>())).Returns(user.Id.ToString);
        _courseService.Setup(x => x.InitiateCourseDeletion(courseDeletionInitiationDTO, user.Id)).Throws(new NotFoundException());

        var actionResult = await _courseController.InitiateCourseDeletion(courseDeletionInitiationDTO);
        var objectResult = (ObjectResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.BadRequest, objectResult.StatusCode);
        _courseService.Verify(service => service.InitiateCourseDeletion(courseDeletionInitiationDTO, user.Id), Times.Once);
    }

    [TestMethod]
    public async Task GetCourseData_ValidCall_ReturnsData()
    {
        var subject = "SOEN";
        var catalog = "490";
        var course = TestData.GetSampleCourse();
        _courseService.Setup(cr => cr.GetCourseDataWithSupportingFilesOrThrowOnDeleted(subject, catalog)).ReturnsAsync(course);

        var actionResult = await _courseController.GetCourseData(subject, catalog);
        var objectResult = (ObjectResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
        _mapper.Verify(mock => mock.Map<CourseDataDTO>(course), Times.Once());
    }

    [TestMethod]
    [ExpectedException(typeof(BadRequestException))]
    public async Task GetCourseData_ServerError_ThrowsTheSameExceptionAsCourseService()
    {
        _courseService.Setup(cr => cr.GetCourseDataWithSupportingFilesOrThrowOnDeleted(It.IsAny<string>(), It.IsAny<string>())).Throws(new BadRequestException());

        var actionResult = await _courseController.GetCourseData(It.IsAny<string>(), It.IsAny<string>());
        var objectResult = (ObjectResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.InternalServerError, objectResult.StatusCode);
    }

    [TestMethod]
    public async Task EditCourseCreationRequest_ValidCall_ReturnsData()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        var course = TestData.GetSampleCourse();
        var courseCreationRequestDTO = TestData.GetSampleCourseCreationRequestDTO(course, dossier);
        var courseCreationRequest = TestData.GetSampleCourseCreationRequest(dossier, course);
        var editCourseCreationRequestDTO = TestData.GetSampleEditCourseCreationRequestDTO();
        ;
        _courseService.Setup(x => x.EditCourseCreationRequest(editCourseCreationRequestDTO)).ReturnsAsync(courseCreationRequest);
        _mapper.Setup(x => x.Map<CourseCreationRequestDTO>(It.IsAny<CourseCreationRequest>())).Returns(courseCreationRequestDTO);

        var actionResult = await _courseController.EditCourseCreationRequest(editCourseCreationRequestDTO);
        var objectResult = (ObjectResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
        _courseService.Verify(service => service.EditCourseCreationRequest(editCourseCreationRequestDTO), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public async Task EditCourseCreationRequest_InvalidCall_ThrowsException()
    {
        var editCourseCreationRequestDTO = TestData.GetSampleEditCourseCreationRequestDTO();

        _courseService.Setup(x => x.EditCourseCreationRequest(editCourseCreationRequestDTO)).Throws(new NotFoundException());

        await _courseController.EditCourseCreationRequest(editCourseCreationRequestDTO);
        _courseService.Verify(service => service.EditCourseCreationRequest(editCourseCreationRequestDTO), Times.Once);
    }

    [TestMethod]
    public async Task EditCourseModificationRequest_ValidCall_ReturnsData()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        var course = TestData.GetSampleCourse();
        var courseModificationRequestDTO = TestData.GetSampleCourseModificationRequestDTO(course, dossier);
        var courseModificationRequest = TestData.GetSampleCourseModificationRequest(dossier, course);
        var editCourseModificationRequestDTO = TestData.GetSampleEditCourseModificationRequestDTO();

        _courseService.Setup(x => x.EditCourseModificationRequest(editCourseModificationRequestDTO)).ReturnsAsync(courseModificationRequest);
        _mapper.Setup(x => x.Map<CourseModificationRequestDTO>(It.IsAny<CourseModificationRequest>())).Returns(courseModificationRequestDTO);

        var actionResult = await _courseController.EditCourseModificationRequest(editCourseModificationRequestDTO);
        var objectResult = (ObjectResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
        _courseService.Verify(service => service.EditCourseModificationRequest(editCourseModificationRequestDTO), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public async Task EditCourseModificationRequest_InvalidCall_ThrowsException()
    {
        var editCourseModificationRequestDTO = TestData.GetSampleEditCourseModificationRequestDTO();

        _courseService.Setup(x => x.EditCourseModificationRequest(editCourseModificationRequestDTO)).Throws(new NotFoundException());

        var actionResult = await _courseController.EditCourseModificationRequest(editCourseModificationRequestDTO);
        var objectResult = (ObjectResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.BadRequest, objectResult.StatusCode);
        _courseService.Verify(service => service.EditCourseModificationRequest(editCourseModificationRequestDTO), Times.Once);
    }

    [TestMethod]
    public async Task EditCourseDeletionRequest_ValidCall_ReturnsData()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        var course = TestData.GetSampleCourse();
        var courseDeletionRequestDTO = TestData.GetSampleCourseDeletionRequestDTO(course, dossier);
        var courseDeletionRequest = TestData.GetSampleCourseDeletionRequest();
        var editCourseDeletionRequestDTO = TestData.GetSampleEditCourseDeletionRequestDTO();

        _courseService.Setup(x => x.EditCourseDeletionRequest(editCourseDeletionRequestDTO)).ReturnsAsync(courseDeletionRequest);
        _mapper.Setup(x => x.Map<CourseDeletionRequestDTO>(It.IsAny<CourseDeletionRequest>())).Returns(courseDeletionRequestDTO);

        var actionResult = await _courseController.EditCourseDeletionRequest(editCourseDeletionRequestDTO);
        var objectResult = (ObjectResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
        _courseService.Verify(service => service.EditCourseDeletionRequest(editCourseDeletionRequestDTO), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public async Task EditCourseDeletionRequest_InvalidCall_ThrowsException()
    {
        var editCourseDeletionRequestDTO = TestData.GetSampleEditCourseDeletionRequestDTO();

        _courseService.Setup(x => x.EditCourseDeletionRequest(editCourseDeletionRequestDTO)).Throws(new NotFoundException());

        var actionResult = await _courseController.EditCourseDeletionRequest(editCourseDeletionRequestDTO);
        var objectResult = (ObjectResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.BadRequest, objectResult.StatusCode);
        _courseService.Verify(service => service.EditCourseDeletionRequest(editCourseDeletionRequestDTO), Times.Once);
    }

    [TestMethod]
    public async Task DeleteCourseCreationRequest_ValidCall_Returns204()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        var course = TestData.GetSampleCourse();
        var courseCreationRequest = TestData.GetSampleCourseCreationRequest(dossier, course);

        var actionResult = await _courseController.DeleteCourseCreationRequest(courseCreationRequest.Id);
        var objectResult = (NoContentResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.NoContent, objectResult.StatusCode);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task DeleteCourseCreationRequest_InvalidCall_ThrowsException()
    {
        _courseService.Setup(service => service.DeleteCourseCreationRequest(It.IsAny<Guid>())).Throws(new Exception());

        await _courseController.DeleteCourseCreationRequest(It.IsAny<Guid>());
    }

    [TestMethod]
    public async Task DeleteCourseModificationRequest_ValidCall_Returns204()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        var course = TestData.GetSampleCourse();
        var courseModificationRequest = TestData.GetSampleCourseModificationRequest(dossier, course);

        var actionResult = await _courseController.DeleteCourseModificationRequest(courseModificationRequest.Id);
        var objectResult = (NoContentResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.NoContent, objectResult.StatusCode);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task DeleteCourseModificationRequest_InvalidCall_ThrowsException()
    {
        _courseService.Setup(service => service.DeleteCourseModificationRequest(It.IsAny<Guid>())).Throws(new Exception());

        await _courseController.DeleteCourseModificationRequest(It.IsAny<Guid>());
    }

    [TestMethod]
    public async Task DeleteCourseDeletionRequest_ValidCall_Returns204()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        var course = TestData.GetSampleCourse();
        var courseDeletionRequest = TestData.GetSampleCourseDeletionRequest(dossier, course);

        var actionResult = await _courseController.DeleteCourseDeletionRequest(courseDeletionRequest.Id);
        var objectResult = (NoContentResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.NoContent, objectResult.StatusCode);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task DeleteCourseDeletionRequest_InvalidCall_ThrowsException()
    {
        _courseService.Setup(service => service.DeleteCourseDeletionRequest(It.IsAny<Guid>())).Throws(new Exception());

        await _courseController.DeleteCourseDeletionRequest(It.IsAny<Guid>());
    }

    [TestMethod]
    public async Task GetCourseCreationRequest_ValidCall_ReturnsData()
    {
        var courseCreationRequest = TestData.GetSampleCourseCreationRequest();

        _dossierService.Setup(service => service.GetCourseCreationRequest(It.IsAny<Guid>())).ReturnsAsync(courseCreationRequest);

        var actionResult = await _courseController.GetCourseCreationRequest(It.IsAny<Guid>());
        var objectResult = (ObjectResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
        _mapper.Verify(mock => mock.Map<CourseCreationRequestCourseDetailsDTO>(courseCreationRequest), Times.Once());
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public async Task GetCourseCreationRequest_InvalidCall_ThrowsException()
    {
        _dossierService.Setup(service => service.GetCourseCreationRequest(It.IsAny<Guid>())).Throws(new NotFoundException());

        var actionResult = await _courseController.GetCourseCreationRequest(It.IsAny<Guid>());
        var objectResult = (ObjectResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.NotFound, objectResult.StatusCode);
    }

    [TestMethod]
    public async Task GetCourseModificationRequest_ValidCall_ReturnsData()
    {
        var courseModificationRequest = TestData.GetSampleCourseModificationRequest();

        _dossierService.Setup(service => service.GetCourseModificationRequest(It.IsAny<Guid>())).ReturnsAsync(courseModificationRequest);

        var actionResult = await _courseController.GetCourseModificationRequest(It.IsAny<Guid>());
        var objectResult = (ObjectResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
        _mapper.Verify(mock => mock.Map<CourseModificationRequestCourseDetailsDTO>(courseModificationRequest), Times.Once());
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public async Task GetCourseModificationRequest_InvalidCall_ThrowsException()
    {
        _dossierService.Setup(service => service.GetCourseModificationRequest(It.IsAny<Guid>())).Throws(new NotFoundException());

        var actionResult = await _courseController.GetCourseModificationRequest(It.IsAny<Guid>());
        var objectResult = (ObjectResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.NotFound, objectResult.StatusCode);
    }

    [TestMethod]
    public async Task GetCourseDeletionRequest_ValidCall_ReturnsData()
    {
        var courseDeletionRequest = TestData.GetSampleCourseDeletionRequest();

        _dossierService.Setup(service => service.GetCourseDeletionRequest(It.IsAny<Guid>())).ReturnsAsync(courseDeletionRequest);

        var actionResult = await _courseController.GetCourseDeletionRequest(It.IsAny<Guid>());
        var objectResult = (ObjectResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
        _mapper.Verify(mock => mock.Map<CourseDeletionRequestDTO>(courseDeletionRequest), Times.Once());
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public async Task GetCourseDeletionRequest_InvalidCall_ThrowsException()
    {
        _dossierService.Setup(service => service.GetCourseDeletionRequest(It.IsAny<Guid>())).Throws(new NotFoundException());

        var actionResult = await _courseController.GetCourseDeletionRequest(It.IsAny<Guid>());
        var objectResult = (ObjectResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.NotFound, objectResult.StatusCode);
    }

    [TestMethod]
    public async Task GetCourseById_ValidId_ReturnsCourse()
    {
        var expectedCourse = TestData.GetSampleCourse();
        var courseId = expectedCourse.Id;
        var expectedCourseDto = new CourseDataDTO
        {
            Id = courseId,
            CourseID = 2000,
            Subject = "COMP",
            Catalog = "1234",
            Title = "Test Course",
            Description = "Description of Test Course",
            CreditValue = "6",
            PreReqs = "SOEN 390",
            CourseNotes = "Lots of fun",
            Career = CourseCareerEnum.UGRD,
            EquivalentCourses = "",
            CourseState = CourseStateEnum.NewCourseProposal,
            Version = 1,
            ComponentCodes = new Dictionary<ComponentCodeEnum, int?>(),
            SupportingFiles = new Dictionary<string, string>()
        };

        _courseService.Setup(service => service.GetCourseByIdAsync(courseId)).ReturnsAsync(expectedCourse);
        _mapper.Setup(mapper => mapper.Map<CourseDataDTO>(expectedCourse)).Returns(expectedCourseDto);

        var actionResult = await _courseController.GetCourseById(courseId);

        Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult), "The result should be an OkObjectResult.");
        var okResult = actionResult.Result as OkObjectResult;
        Assert.IsNotNull(okResult, "The OK result should not be null.");
        Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode, "The status code should be 200 (OK).");
        Assert.AreEqual(expectedCourseDto, okResult.Value, "The returned course DTO does not match the expected value.");
    }

    [TestMethod]
    public async Task GetCoursesBySubject_ReturnsOkWithCourses()
    {
        var subjectCode = "SOEN";
        var id = Guid.NewGuid();
        var courses = new List<Course>
        {
            new Course {
                Id = Guid.NewGuid(),
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
                CourseCourseComponents = CourseCourseComponent.GetComponentCodeMapping(new Dictionary<ComponentCodeEnum, int?>
                    { { ComponentCodeEnum.LEC, 3 }, { ComponentCodeEnum.WKS, 5 } },
                    id
                )
            },
            new Course {
                Id = Guid.NewGuid(),
                CourseID = 2000,
                Subject = "SOEN",
                Catalog = "390",
                Title = "Mini-Capstone",
                Description = "Mini-Capstone",
                CreditValue = "6",
                PreReqs = "SOEN 390",
                CourseNotes = "Lots of fun",
                Career = CourseCareerEnum.UGRD,
                EquivalentCourses = "",
                CourseState = CourseStateEnum.NewCourseProposal,
                Version = 1,
                Published = true,
                CourseCourseComponents = CourseCourseComponent.GetComponentCodeMapping(new Dictionary<ComponentCodeEnum, int?>
                    { { ComponentCodeEnum.LEC, 3 }, { ComponentCodeEnum.WKS, 5 } },
                    id
                )
            }
        };

        _courseService.Setup(service => service.GetCoursesBySubjectAsync(subjectCode))
            .ReturnsAsync(courses);

        var actionResult = await _courseController.GetCoursesBySubject(subjectCode);

        var okResult = actionResult.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.IsInstanceOfType(okResult.Value, typeof(IEnumerable<CourseDataDTO>));
        var resultValue = okResult.Value as IEnumerable<CourseDataDTO>;
    }

    [TestMethod]
    public async Task PublishCourse_ValidCall_ReturnsPublishedCourse()
    {
        var course = TestData.GetSampleCourse();
        _courseService.Setup(x => x.PublishCourse(course.Subject, course.Catalog)).ReturnsAsync(course);

        var actionResult = await _courseController.PublishCourse(course.Subject, course.Catalog);
        var objectResult = (ObjectResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
        _mapper.Verify(mock => mock.Map<CourseDataDTO>(course), Times.Once());
        _courseService.Verify(service => service.PublishCourse(course.Subject, course.Catalog), Times.Once);
    }


    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public async Task PublishCourse_InvalidCall_ThrowsNotFoundException()
    {
        _courseService.Setup(service => service.PublishCourse(It.IsAny<string>(), It.IsAny<string>())).Throws(new NotFoundException());

        var actionResult = await _courseController.PublishCourse(It.IsAny<string>(), It.IsAny<string>());
        var objectResult = (ObjectResult)actionResult;

        Assert.AreEqual((int)HttpStatusCode.BadRequest, objectResult.StatusCode);
    }

}
