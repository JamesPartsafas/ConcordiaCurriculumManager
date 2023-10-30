using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests;
using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories;
using ConcordiaCurriculumManager.Services;
using ConcordiaCurriculumManager.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordiaCurriculumManagerTest.UnitTests.Services;

[TestClass]
public class CourseServiceTest
{
    private Mock<ICourseRepository> courseRepository = null!;
    private Mock<IDossierService> dossierService = null!;
    private Mock<ILogger<CourseService>> logger = null!;
    private CourseService courseService = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        logger = new Mock<ILogger<CourseService>>();
        courseRepository = new Mock<ICourseRepository>();
        dossierService = new Mock<IDossierService>();

        courseService = new CourseService(logger.Object, courseRepository.Object, dossierService.Object);
    }

    [TestMethod]
    public async Task GetAllCourseSubjects_ValidCall_QueriesRepo()
    {
        await courseService.GetAllCourseSubjects();

        courseRepository.Verify(cs => cs.GetUniqueCourseSubjects());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task InitiateCourseCreation_CourseExists_ThrowsArgumentException()
    {
        var user = GetSampleUser();
        var dossier = GetSampleDossier(user);
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(GetSampleCourse());

        await courseService.InitiateCourseCreation(GetSampleCourseCreationInitiationDTO(dossier), user.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task InitiateCourseCreation_DossierDoesNotExist_LogsAndThrowsException()
    {
        var user = GetSampleUser();
        var dossier = GetSampleDossier(user);
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((Course?)null);

        await courseService.InitiateCourseCreation(GetSampleCourseCreationInitiationDTO(dossier), user.Id);

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task InitiateCourseCreation_DossierDoesNotBelongToUser_LogsAndThrowsException()
    {
        var user = GetSampleUser();
        var dossier = GetSampleDossier(user);
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((Course?)null);
        dossierService.Setup(cr => cr.GetDossierForUserOrThrow(It.IsAny<Guid>(), user.Id)).ReturnsAsync(GetSampleDossier(user));

        await courseService.InitiateCourseCreation(GetSampleCourseCreationInitiationDTO(dossier), Guid.NewGuid());

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));

    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task InitiateCourseCreation_CourseDoesNotAdd_LogsAndThrowsException()
    {
        var user = GetSampleUser();
        var dossier = GetSampleDossier(user);
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((Course?)null);
        courseRepository.Setup(cr => cr.GetMaxCourseId()).ReturnsAsync(5);
        courseRepository.Setup(cr => cr.SaveCourse(It.IsAny<Course>())).ReturnsAsync(false);

        await courseService.InitiateCourseCreation(GetSampleCourseCreationInitiationDTO(dossier), user.Id);

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task InitiateCourseCreation_DossierDoesNotSave_LogsAndThrowsException()
    {
        var user = GetSampleUser();
        var dossier = GetSampleDossier(user);
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((Course?)null);
        courseRepository.Setup(cr => cr.GetMaxCourseId()).ReturnsAsync(5);
        courseRepository.Setup(cr => cr.SaveCourse(It.IsAny<Course>())).ReturnsAsync(true);
        dossierService.Setup(ds => ds.GetDossierForUserOrThrow(dossier.Id, user.Id)).ReturnsAsync(dossier);
        dossierService.Setup(cr => cr.SaveCourseCreationRequest(It.IsAny<CourseCreationRequest>()))
            .ThrowsAsync(new Exception());

        await courseService.InitiateCourseCreation(GetSampleCourseCreationInitiationDTO(dossier), user.Id);
    }

    [TestMethod]
    public async Task InitiateCourseCreation_ValidInput_Succeeds()
    {
        var user = GetSampleUser();
        var dossier = GetSampleDossier(user);
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((Course?)null);
        courseRepository.Setup(cr => cr.GetMaxCourseId()).ReturnsAsync(5);
        courseRepository.Setup(cr => cr.SaveCourse(It.IsAny<Course>())).ReturnsAsync(true);
        dossierService.Setup(cr => cr.SaveCourseCreationRequest(It.IsAny<CourseCreationRequest>()))
            .Returns(Task.CompletedTask);
        dossierService.Setup(cr => cr.GetDossierForUserOrThrow(dossier.Id, user.Id)).ReturnsAsync(dossier);
        
        var courseCreationRequest = await courseService.InitiateCourseCreation(GetSampleCourseCreationInitiationDTO(dossier), user.Id);

        Assert.IsNotNull(courseCreationRequest);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task InitiateCourseModification_CourseDoesNotExists_ThrowsArgumentException()
    {
        var user = GetSampleUser();
        var dossier = GetSampleDossier(user);
        var course = GetSampleCourse();

        await courseService.InitiateCourseModification(GetSampleCourseCreationModificationDTO(course, dossier), user.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task InitiateCourseModification_DossierDoesNotExist_LogsAndThrowsException()
    {
        var user = GetSampleUser();
        var dossier = GetSampleDossier(user);
        var course = GetSampleCourse();
        courseRepository.Setup(cr => cr.GetCourseByCourseId(It.IsAny<int>())).ReturnsAsync(course);

        await courseService.InitiateCourseModification(GetSampleCourseCreationModificationDTO(course, dossier), user.Id);

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task InitiateCourseModification_DossierDoesNotBelongToUser_LogsAndThrowsException()
    {
        var user = GetSampleUser();
        var dossier = GetSampleDossier(user);
        var course = GetSampleCourse();
        courseRepository.Setup(cr => cr.GetCourseByCourseId(It.IsAny<int>())).ReturnsAsync(course);
        dossierService.Setup(cr => cr.GetDossierDetailsById(It.IsAny<Guid>())).ReturnsAsync(GetSampleDossier(user));

        await courseService.InitiateCourseModification(GetSampleCourseCreationModificationDTO(course, dossier), Guid.NewGuid());

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));

    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task InitiateCourseModification_CourseDoesNotSave_LogsAndThrowsException()
    {
        var user = GetSampleUser();
        var dossier = GetSampleDossier(user);
        var course = GetSampleCourse();
        courseRepository.Setup(cr => cr.GetCourseByCourseId(It.IsAny<int>())).ReturnsAsync(course);
        dossierService.Setup(cr => cr.GetDossierDetailsById(It.IsAny<Guid>())).ReturnsAsync(GetSampleDossier(user));
        courseRepository.Setup(cr => cr.SaveCourse(It.IsAny<Course>())).ReturnsAsync(false);

        await courseService.InitiateCourseModification(GetSampleCourseCreationModificationDTO(course, dossier), user.Id);

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task InitiateCourseModification_CourseModificationRequestDoesNotSave_LogsAndThrowsException()
    {
        var user = GetSampleUser();
        var dossier = GetSampleDossier(user);
        var course = GetSampleCourse();
        courseRepository.Setup(cr => cr.GetCourseByCourseId(It.IsAny<int>())).ReturnsAsync(course);
        dossierService.Setup(cr => cr.GetDossierDetailsById(It.IsAny<Guid>())).ReturnsAsync(GetSampleDossier(user));
        courseRepository.Setup(cr => cr.SaveCourse(It.IsAny<Course>())).ReturnsAsync(true);
        dossierService.Setup(ds => ds.GetDossierForUserOrThrow(dossier.Id, user.Id)).ReturnsAsync(dossier);
        dossierService.Setup(cr => cr.SaveCourseModificationRequest(It.IsAny<CourseModificationRequest>())).ThrowsAsync(new Exception());

        await courseService.InitiateCourseModification(GetSampleCourseCreationModificationDTO(course, dossier), user.Id);
    }

    [TestMethod]
    public async Task InitiateCourseModification_ValidInput_Succeeds()
    {
        var user = GetSampleUser();
        var dossier = GetSampleDossier(user);
        var course = GetSampleCourse();
        courseRepository.Setup(cr => cr.GetCourseByCourseId(It.IsAny<int>())).ReturnsAsync(course);
        dossierService.Setup(cr => cr.GetDossierDetailsById(It.IsAny<Guid>())).ReturnsAsync(GetSampleDossier(user));
        courseRepository.Setup(cr => cr.SaveCourse(It.IsAny<Course>())).ReturnsAsync(true);
        dossierService.Setup(ds => ds.GetDossierForUserOrThrow(dossier.Id, user.Id)).ReturnsAsync(dossier);
        dossierService.Setup(cr => cr.SaveCourseModificationRequest(It.IsAny<CourseModificationRequest>())).Returns(Task.CompletedTask);

        var courseModificationRequest = await courseService.InitiateCourseModification(GetSampleCourseCreationModificationDTO(course, dossier), user.Id);

        Assert.IsNotNull(courseModificationRequest);
    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task InitiateCourseDeletion_CourseDoesNotExists_ThrowsArgumentException()
    {
        var user = GetSampleUser();
        var dossier = GetSampleDossier(user);
        var course = GetSampleCourse();

        await courseService.InitiateCourseDeletion(GetSampleCourseCreationDeletionDTO(course, dossier), user.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task InitiateCourseDeletion_DossierDoesNotExist_LogsAndThrowsException()
    {
        var user = GetSampleUser();
        var dossier = GetSampleDossier(user);
        var course = GetSampleCourse();
        courseRepository.Setup(cr => cr.GetCourseByCourseId(It.IsAny<int>())).ReturnsAsync(course);

        await courseService.InitiateCourseDeletion(GetSampleCourseCreationDeletionDTO(course, dossier), user.Id);

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task InitiateCourseDeletion_DossierDoesNotBelongToUser_LogsAndThrowsException()
    {
        var user = GetSampleUser();
        var dossier = GetSampleDossier(user);
        var course = GetSampleCourse();
        courseRepository.Setup(cr => cr.GetCourseByCourseId(It.IsAny<int>())).ReturnsAsync(course);
        dossierService.Setup(cr => cr.GetDossierDetailsById(It.IsAny<Guid>())).ReturnsAsync(GetSampleDossier(user));

        await courseService.InitiateCourseDeletion(GetSampleCourseCreationDeletionDTO(course, dossier), Guid.NewGuid());

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));

    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task InitiateCourseDeletion_CourseDoesNotSave_LogsAndThrowsException()
    {
        var user = GetSampleUser();
        var dossier = GetSampleDossier(user);
        var course = GetSampleCourse();
        courseRepository.Setup(cr => cr.GetCourseByCourseId(It.IsAny<int>())).ReturnsAsync(course);
        dossierService.Setup(cr => cr.GetDossierDetailsById(It.IsAny<Guid>())).ReturnsAsync(GetSampleDossier(user));
        courseRepository.Setup(cr => cr.SaveCourse(It.IsAny<Course>())).ReturnsAsync(false);

        await courseService.InitiateCourseDeletion(GetSampleCourseCreationDeletionDTO(course, dossier), user.Id);

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task InitiateCourseDeletion_CourseDeletionRequestDoesNotSave_LogsAndThrowsException()
    {
        var user = GetSampleUser();
        var dossier = GetSampleDossier(user);
        var course = GetSampleCourse();
        courseRepository.Setup(cr => cr.GetCourseByCourseId(It.IsAny<int>())).ReturnsAsync(course);
        dossierService.Setup(cr => cr.GetDossierDetailsById(It.IsAny<Guid>())).ReturnsAsync(GetSampleDossier(user));
        courseRepository.Setup(cr => cr.SaveCourse(It.IsAny<Course>())).ReturnsAsync(true);
        dossierService.Setup(ds => ds.GetDossierForUserOrThrow(dossier.Id, user.Id)).ReturnsAsync(dossier);
        dossierService.Setup(cr => cr.SaveCourseDeletionRequest(It.IsAny<CourseDeletionRequest>())).ThrowsAsync(new Exception());

        await courseService.InitiateCourseDeletion(GetSampleCourseCreationDeletionDTO(course, dossier), user.Id);
    }

    [TestMethod]
    public async Task InitiateCourseDeletion_ValidInput_Succeeds()
    {
        var user = GetSampleUser();
        var dossier = GetSampleDossier(user);
        var course = GetSampleCourse();
        courseRepository.Setup(cr => cr.GetCourseByCourseId(It.IsAny<int>())).ReturnsAsync(course);
        dossierService.Setup(cr => cr.GetDossierDetailsById(It.IsAny<Guid>())).ReturnsAsync(GetSampleDossier(user));
        courseRepository.Setup(cr => cr.SaveCourse(It.IsAny<Course>())).ReturnsAsync(true);
        dossierService.Setup(ds => ds.GetDossierForUserOrThrow(dossier.Id, user.Id)).ReturnsAsync(dossier);
        dossierService.Setup(cr => cr.SaveCourseDeletionRequest(It.IsAny<CourseDeletionRequest>())).Returns(Task.CompletedTask);

        var courseDeletionRequest = await courseService.InitiateCourseDeletion(GetSampleCourseCreationDeletionDTO(course, dossier), user.Id);

        Assert.IsNotNull(courseDeletionRequest);
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

    private CourseCreationInitiationDTO GetSampleCourseCreationInitiationDTO(Dossier dossier)
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
            DossierId = dossier.Id,
        };
    }

    private CourseModificationInitiationDTO GetSampleCourseCreationModificationDTO(Course course, Dossier dossier)
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
            CourseId = course.CourseID
        };
    }

    private CourseDeletionInitiationDTO GetSampleCourseCreationDeletionDTO(Course course, Dossier dossier)
    {
        return new CourseDeletionInitiationDTO
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
            CourseId = course.CourseID
        };
    }

    private User GetSampleUser()
    {
        return new User
        {
            Id = Guid.NewGuid(),
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

    private CourseCreationRequest GetSampleCourseCreationRequest(Dossier dossier, Course course)
    {
        return new CourseCreationRequest
        {
            DossierId = dossier.Id,
            NewCourseId = course.Id,
            Rationale = "Why not?",
            ResourceImplication = "New prof needed",
            Comment = "Easy change to make"
        };
    }

    private CourseModificationRequest GetSampleCourseModificationRequest(Dossier dossier, Course course)
    {
        return new CourseModificationRequest
        {
            DossierId = dossier.Id,
            CourseId = course.Id,
            Rationale = "Why not?",
            ResourceImplication = "New prof needed",
            Comment = "Easy change to make"
        };
    }
}
