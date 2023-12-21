using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.InputDTOs;
using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Repositories;
using ConcordiaCurriculumManager.Services;
using ConcordiaCurriculumManagerTest.UnitTests.UtilityFunctions;
using Microsoft.Extensions.Logging;
using Moq;

namespace ConcordiaCurriculumManagerTest.UnitTests.Services;

[TestClass]
public class CourseServiceTest
{
    private Mock<ICourseRepository> courseRepository = null!;
    private Mock<IDossierService> dossierService = null!;
    private Mock<ILogger<CourseService>> logger = null!;
    private CourseService courseService = null!;
    private Mock<IDossierRepository> dossierRepository = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        logger = new Mock<ILogger<CourseService>>();
        courseRepository = new Mock<ICourseRepository>();
        dossierService = new Mock<IDossierService>();
        dossierRepository = new Mock<IDossierRepository>();

        courseService = new CourseService(logger.Object, courseRepository.Object, dossierService.Object, dossierRepository.Object);
    }

    [TestMethod]
    public async Task GetAllCourseSubjects_ValidCall_QueriesRepo()
    {
        await courseService.GetAllCourseSubjects();

        courseRepository.Verify(cs => cs.GetUniqueCourseSubjects());
    }

    [TestMethod]
    [ExpectedException(typeof(BadRequestException))]
    public async Task InitiateCourseCreation_CourseExists_ThrowsArgumentException()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        var dto = TestData.GetSampleCourseCreationInitiationDTO(dossier);
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(TestData.GetSampleAcceptedCourse());

        await courseService.InitiateCourseCreation(dto, user.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task InitiateCourseCreation_DossierDoesNotExist_LogsAndThrowsException()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((Course?)null);

        await courseService.InitiateCourseCreation(TestData.GetSampleCourseCreationInitiationDTO(dossier), user.Id);

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task InitiateCourseCreation_DossierDoesNotBelongToUser_LogsAndThrowsException()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((Course?)null);
        dossierService.Setup(cr => cr.GetDossierForUserOrThrow(It.IsAny<Guid>(), user.Id)).ReturnsAsync(TestData.GetSampleDossier(user));

        await courseService.InitiateCourseCreation(TestData.GetSampleCourseCreationInitiationDTO(dossier), Guid.NewGuid());

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));

    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task InitiateCourseCreation_CourseDoesNotAdd_LogsAndThrowsException()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((Course?)null);
        courseRepository.Setup(cr => cr.GetMaxCourseId()).ReturnsAsync(5);
        courseRepository.Setup(cr => cr.SaveCourse(It.IsAny<Course>())).ReturnsAsync(false);

        await courseService.InitiateCourseCreation(TestData.GetSampleCourseCreationInitiationDTO(dossier), user.Id);

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task InitiateCourseCreation_DossierDoesNotSave_LogsAndThrowsException()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((Course?)null);
        courseRepository.Setup(cr => cr.GetMaxCourseId()).ReturnsAsync(5);
        courseRepository.Setup(cr => cr.SaveCourse(It.IsAny<Course>())).ReturnsAsync(true);
        dossierService.Setup(ds => ds.GetDossierForUserOrThrow(dossier.Id, user.Id)).ReturnsAsync(dossier);
        dossierService.Setup(cr => cr.SaveCourseCreationRequest(It.IsAny<CourseCreationRequest>()))
            .ThrowsAsync(new Exception());

        await courseService.InitiateCourseCreation(TestData.GetSampleCourseCreationInitiationDTO(dossier), user.Id);
    }

    [TestMethod]
    public async Task InitiateCourseCreation_ValidInput_Succeeds()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((Course?)null);
        courseRepository.Setup(cr => cr.GetMaxCourseId()).ReturnsAsync(5);
        courseRepository.Setup(cr => cr.SaveCourse(It.IsAny<Course>())).ReturnsAsync(true);
        dossierService.Setup(cr => cr.SaveCourseCreationRequest(It.IsAny<CourseCreationRequest>()))
            .Returns(Task.CompletedTask);
        dossierService.Setup(cr => cr.GetDossierForUserOrThrow(dossier.Id, user.Id)).ReturnsAsync(dossier);

        var courseCreationRequest = await courseService.InitiateCourseCreation(TestData.GetSampleCourseCreationInitiationDTO(dossier), user.Id);

        Assert.IsNotNull(courseCreationRequest);
    }

    [TestMethod]
    public async Task InitiateCourseCreation_ValidInputWithDeletedCourse_Succeeds()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(TestData.GetSampleDeletedCourse());
        courseRepository.Setup(cr => cr.GetMaxCourseId()).ReturnsAsync(5);
        courseRepository.Setup(cr => cr.SaveCourse(It.IsAny<Course>())).ReturnsAsync(true);
        dossierService.Setup(cr => cr.SaveCourseCreationRequest(It.IsAny<CourseCreationRequest>()))
            .Returns(Task.CompletedTask);
        dossierService.Setup(cr => cr.GetDossierForUserOrThrow(dossier.Id, user.Id)).ReturnsAsync(dossier);

        var courseCreationRequest = await courseService.InitiateCourseCreation(TestData.GetSampleCourseCreationInitiationDTO(dossier), user.Id);

        Assert.IsNotNull(courseCreationRequest);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidInputException))]
    public async Task InitiateCourseModification_CourseDoesNotExists_ThrowsInvalidInputException()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        var course = TestData.GetSampleCourse();

        await courseService.InitiateCourseModification(TestData.GetSampleCourseCreationModificationDTO(course, dossier), user.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task InitiateCourseModification_DossierDoesNotExist_ThrowsException()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        var course = TestData.GetSampleCourse();
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(course);

        await courseService.InitiateCourseModification(TestData.GetSampleCourseCreationModificationDTO(course, dossier), user.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task InitiateCourseModification_DossierDoesNotBelongToUser_ThrowsException()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        var course = TestData.GetSampleCourse();
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(course);
        dossierService.Setup(cr => cr.GetDossierDetailsById(It.IsAny<Guid>())).ReturnsAsync(TestData.GetSampleDossier(user));

        await courseService.InitiateCourseModification(TestData.GetSampleCourseCreationModificationDTO(course, dossier), Guid.NewGuid());
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task InitiateCourseModification_CourseDoesNotSave_LogsAndThrowsException()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        var course = TestData.GetSampleCourse();
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(course);
        dossierService.Setup(cr => cr.GetDossierDetailsById(It.IsAny<Guid>())).ReturnsAsync(TestData.GetSampleDossier(user));
        courseRepository.Setup(cr => cr.SaveCourse(It.IsAny<Course>())).ReturnsAsync(false);

        await courseService.InitiateCourseModification(TestData.GetSampleCourseCreationModificationDTO(course, dossier), user.Id);

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task InitiateCourseModification_CourseModificationRequestDoesNotSave_LogsAndThrowsException()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        var course = TestData.GetSampleCourse();
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(course);
        dossierService.Setup(cr => cr.GetDossierDetailsById(It.IsAny<Guid>())).ReturnsAsync(TestData.GetSampleDossier(user));
        courseRepository.Setup(cr => cr.SaveCourse(It.IsAny<Course>())).ReturnsAsync(true);
        dossierService.Setup(ds => ds.GetDossierForUserOrThrow(dossier.Id, user.Id)).ReturnsAsync(dossier);
        dossierService.Setup(cr => cr.SaveCourseModificationRequest(It.IsAny<CourseModificationRequest>())).ThrowsAsync(new Exception());

        await courseService.InitiateCourseModification(TestData.GetSampleCourseCreationModificationDTO(course, dossier), user.Id);
    }

    [TestMethod]
    public async Task InitiateCourseModification_ValidInput_Succeeds()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        var course = TestData.GetSampleAcceptedCourse();
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(course);
        dossierService.Setup(cr => cr.GetDossierDetailsById(It.IsAny<Guid>())).ReturnsAsync(TestData.GetSampleDossier(user));
        courseRepository.Setup(cr => cr.SaveCourse(It.IsAny<Course>())).ReturnsAsync(true);
        dossierService.Setup(ds => ds.GetDossierForUserOrThrow(dossier.Id, user.Id)).ReturnsAsync(dossier);
        dossierService.Setup(cr => cr.SaveCourseModificationRequest(It.IsAny<CourseModificationRequest>())).Returns(Task.CompletedTask);

        var courseModificationRequest = await courseService.InitiateCourseModification(TestData.GetSampleCourseCreationModificationDTO(course, dossier), user.Id);

        Assert.IsNotNull(courseModificationRequest);
    }

    [TestMethod]
    public async Task InitiateCourseModification_WithConflictInCourse_SetsProperConflictMessage()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        var course = TestData.GetSampleAcceptedCourse();

        var course1 = TestData.GetSampleCourse();
        course1.Subject = "COMP";
        course1.Catalog = "123";

        var course2 = TestData.GetSampleCourse();
        course1.Subject = "SOEN";
        course1.Catalog = "456";

        course.CourseReferenced = new List<CourseReference>() {
            new()
            {
                CourseReferenced = course,
                CourseReferencedId = course.Id,
                CourseReferencing = course1,
                CourseReferencingId = course1.Id,
            },
            new()
            {
                CourseReferenced = course,
                CourseReferencedId = course.Id,
                CourseReferencing = course2,
                CourseReferencingId = course2.Id,
            },
        };

        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(course);
        dossierService.Setup(cr => cr.GetDossierDetailsById(It.IsAny<Guid>())).ReturnsAsync(TestData.GetSampleDossier(user));
        courseRepository.Setup(cr => cr.SaveCourse(It.IsAny<Course>())).ReturnsAsync(true);
        dossierService.Setup(ds => ds.GetDossierForUserOrThrow(dossier.Id, user.Id)).ReturnsAsync(dossier);
        dossierService.Setup(cr => cr.SaveCourseModificationRequest(It.IsAny<CourseModificationRequest>())).Returns(Task.CompletedTask);

        var courseModificationRequest = await courseService.InitiateCourseModification(TestData.GetSampleCourseCreationModificationDTO(course, dossier), user.Id);

        Assert.IsNotNull(courseModificationRequest);

        StringAssert.Contains(courseModificationRequest.Conflict, $"{course1.Subject}-{course1.Catalog}");
        StringAssert.Contains(courseModificationRequest.Conflict, $"{course2.Subject}-{course2.Catalog}");
    }

    [TestMethod]
    [ExpectedException(typeof(BadRequestException))]
    public async Task InitiateCourseModification_ValidInputWithDeletedCourse_Throws()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        var course = TestData.GetSampleDeletedCourse();
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(course);

        var courseModificationRequest = await courseService.InitiateCourseModification(TestData.GetSampleCourseCreationModificationDTO(course, dossier), user.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidInputException))]
    public async Task GetCourseData_CourseDoesNotExist_ThrowsInvalidInputException()
    {
        var subject = "SOEN";
        var catalog = "490";
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((Course?)null);

        await courseService.GetCourseDataOrThrowOnDeleted(subject, catalog);
    }

    [TestMethod]
    [ExpectedException(typeof(BadRequestException))]
    public async Task GetCourseData_CourseIsDeleted_ThrowsBadRequestException()
    {
        var subject = "SOEN";
        var catalog = "490";
        var course = TestData.GetSampleCourse();
        course.CourseState = CourseStateEnum.Deleted;
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(course);
        courseRepository.Setup(cr => cr.GetCourseByCourseIdAndLatestVersion(It.IsAny<int>())).ReturnsAsync(course);

        await courseService.GetCourseDataOrThrowOnDeleted(subject, catalog);
    }

    [TestMethod]
    public async Task GetCourseData_ValidCall_Succeeds()
    {
        var subject = "SOEN";
        var catalog = "490";
        var course = TestData.GetSampleCourse();
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(course);
        courseRepository.Setup(cr => cr.GetCourseByCourseIdAndLatestVersion(It.IsAny<int>())).ReturnsAsync(course);

        var courseData = await courseService.GetCourseDataOrThrowOnDeleted(subject, catalog);

        Assert.IsNotNull(courseData);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidInputException))]
    public async Task InitiateCourseDeletion_CourseDoesNotExists_ThrowsInvalidInputException()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        var course = TestData.GetSampleCourse();

        await courseService.InitiateCourseDeletion(TestData.GetSampleCourseCreationDeletionDTO(course, dossier), user.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task InitiateCourseDeletion_DossierDoesNotExist_LogsAndThrowsException()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        var course = TestData.GetSampleCourse();
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(course);

        await courseService.InitiateCourseDeletion(TestData.GetSampleCourseCreationDeletionDTO(course, dossier), user.Id);

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task InitiateCourseDeletion_DossierDoesNotBelongToUser_LogsAndThrowsException()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        var course = TestData.GetSampleCourse();
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(course);
        dossierService.Setup(cr => cr.GetDossierDetailsById(It.IsAny<Guid>())).ReturnsAsync(TestData.GetSampleDossier(user));

        await courseService.InitiateCourseDeletion(TestData.GetSampleCourseCreationDeletionDTO(course, dossier), Guid.NewGuid());

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));

    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task InitiateCourseDeletion_CourseDoesNotSave_LogsAndThrowsException()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        var course = TestData.GetSampleCourse();
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(course);
        dossierService.Setup(cr => cr.GetDossierDetailsById(It.IsAny<Guid>())).ReturnsAsync(TestData.GetSampleDossier(user));
        courseRepository.Setup(cr => cr.SaveCourse(It.IsAny<Course>())).ReturnsAsync(false);

        await courseService.InitiateCourseDeletion(TestData.GetSampleCourseCreationDeletionDTO(course, dossier), user.Id);

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task InitiateCourseDeletion_CourseDeletionRequestDoesNotSave_LogsAndThrowsException()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        var course = TestData.GetSampleCourse();
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(course);
        dossierService.Setup(cr => cr.GetDossierDetailsById(It.IsAny<Guid>())).ReturnsAsync(TestData.GetSampleDossier(user));
        courseRepository.Setup(cr => cr.SaveCourse(It.IsAny<Course>())).ReturnsAsync(true);
        dossierService.Setup(ds => ds.GetDossierForUserOrThrow(dossier.Id, user.Id)).ReturnsAsync(dossier);
        dossierService.Setup(cr => cr.SaveCourseDeletionRequest(It.IsAny<CourseDeletionRequest>())).ThrowsAsync(new Exception());

        await courseService.InitiateCourseDeletion(TestData.GetSampleCourseCreationDeletionDTO(course, dossier), user.Id);
    }

    [TestMethod]
    public async Task InitiateCourseDeletion_ValidInput_Succeeds()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        var course = TestData.GetSampleCourse();
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(course);
        dossierService.Setup(cr => cr.GetDossierDetailsById(It.IsAny<Guid>())).ReturnsAsync(TestData.GetSampleDossier(user));
        courseRepository.Setup(cr => cr.SaveCourse(It.IsAny<Course>())).ReturnsAsync(true);
        dossierService.Setup(ds => ds.GetDossierForUserOrThrow(dossier.Id, user.Id)).ReturnsAsync(dossier);
        dossierService.Setup(cr => cr.SaveCourseDeletionRequest(It.IsAny<CourseDeletionRequest>())).Returns(Task.CompletedTask);

        var courseDeletionRequest = await courseService.InitiateCourseDeletion(TestData.GetSampleCourseCreationDeletionDTO(course, dossier), user.Id);

        Assert.IsNotNull(courseDeletionRequest);
    }

    [TestMethod]
    public async Task InitiateCourseDeletion_ValidInput_SetsProperConflictMessage()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        var course = TestData.GetSampleCourse();

        var course1 = TestData.GetSampleCourse();
        course1.Subject = "COMP";
        course1.Catalog = "123";

        var course2 = TestData.GetSampleCourse();
        course1.Subject = "SOEN";
        course1.Catalog = "456";

        course.CourseReferenced = new List<CourseReference>() {
            new()
            {
                CourseReferenced = course,
                CourseReferencedId = course.Id,
                CourseReferencing = course1,
                CourseReferencingId = course1.Id,
            },
            new()
            {
                CourseReferenced = course,
                CourseReferencedId = course.Id,
                CourseReferencing = course2,
                CourseReferencingId = course2.Id,
            },
        };

        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(course);
        dossierService.Setup(cr => cr.GetDossierDetailsById(It.IsAny<Guid>())).ReturnsAsync(TestData.GetSampleDossier(user));
        courseRepository.Setup(cr => cr.SaveCourse(It.IsAny<Course>())).ReturnsAsync(true);
        dossierService.Setup(ds => ds.GetDossierForUserOrThrow(dossier.Id, user.Id)).ReturnsAsync(dossier);
        dossierService.Setup(cr => cr.SaveCourseDeletionRequest(It.IsAny<CourseDeletionRequest>())).Returns(Task.CompletedTask);

        var courseDeletionRequest = await courseService.InitiateCourseDeletion(TestData.GetSampleCourseCreationDeletionDTO(course, dossier), user.Id);

        Assert.IsNotNull(courseDeletionRequest);

        StringAssert.Contains(courseDeletionRequest.Conflict, $"{course1.Subject}-{course1.Catalog}");
        StringAssert.Contains(courseDeletionRequest.Conflict, $"{course2.Subject}-{course2.Catalog}");
    }

    [TestMethod]
    public async Task EditCourseCreationRequest_ValidInput_Succeeds()
    {
        var courseCreationRequest = TestData.GetSampleCourseCreationRequest();
        var editCourseCreationRequestDTO = TestData.GetSampleEditCourseCreationRequestDTO();
        dossierService.Setup(service => service.GetCourseCreationRequest(It.IsAny<Guid>())).ReturnsAsync(courseCreationRequest);
        dossierRepository.Setup(repository => repository.UpdateCourseCreationRequest(courseCreationRequest)).ReturnsAsync(true);

        var editCourseCreationRequest = await courseService.EditCourseCreationRequest(editCourseCreationRequestDTO);

        Assert.IsNotNull(editCourseCreationRequest);
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public async Task EditCourseCreationRequest_CourseDoesNotExist_ThrowsNotFoundException()
    {
        var courseCreationRequest = TestData.GetSampleCourseCreationRequest();
        courseCreationRequest.NewCourse = null;
        var editCourseCreationRequestDTO = TestData.GetSampleEditCourseCreationRequestDTO();
        dossierService.Setup(service => service.GetCourseCreationRequest(It.IsAny<Guid>())).ReturnsAsync(courseCreationRequest);

        await courseService.EditCourseCreationRequest(editCourseCreationRequestDTO);
    }

    [TestMethod]
    public async Task EditCourseModificationRequest_ValidInput_Succeeds()
    {
        var courseModificationRequest = TestData.GetSampleCourseModificationRequest();
        var editCourseModificationRequestDTO = TestData.GetSampleEditCourseModificationRequestDTO();
        dossierService.Setup(service => service.GetCourseModificationRequest(It.IsAny<Guid>())).ReturnsAsync(courseModificationRequest);
        dossierRepository.Setup(repository => repository.UpdateCourseModificationRequest(courseModificationRequest)).ReturnsAsync(true);

        var editCourseCreationRequest = await courseService.EditCourseModificationRequest(editCourseModificationRequestDTO);

        Assert.IsNotNull(editCourseCreationRequest);
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public async Task EditCourseModificationRequest_CourseDoesNotExist_ThrowsNotFoundException()
    {
        var courseModificationRequest = TestData.GetSampleCourseModificationRequest();
        courseModificationRequest.Course = null;
        var editCourseModificationRequestDTO = TestData.GetSampleEditCourseModificationRequestDTO();
        dossierService.Setup(service => service.GetCourseModificationRequest(It.IsAny<Guid>())).ReturnsAsync(courseModificationRequest);

        await courseService.EditCourseModificationRequest(editCourseModificationRequestDTO);
    }

    [TestMethod]
    public async Task EditCourseDeletionRequest_ValidInput_Succeeds()
    {
        var courseDeletionRequest = TestData.GetSampleCourseDeletionRequest();
        var editCourseDeletionRequestDTO = TestData.GetSampleEditCourseDeletionRequestDTO();
        dossierService.Setup(service => service.GetCourseDeletionRequest(It.IsAny<Guid>())).ReturnsAsync(courseDeletionRequest);
        dossierRepository.Setup(repository => repository.UpdateCourseDeletionRequest(courseDeletionRequest)).ReturnsAsync(true);

        var editCourseCreationRequest = await courseService.EditCourseDeletionRequest(editCourseDeletionRequestDTO);

        Assert.IsNotNull(editCourseCreationRequest);
    }

    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public async Task DeleteCourseCreationRequest_DoesNotExist_ThrowsNullReferenceException()
    {
        await courseService.DeleteCourseCreationRequest(Guid.NewGuid());
    }


    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task DeleteCourseCreationRequest_DoesNotDelete_LogsAndThrowsException()
    {
        var courseCreationRequest = TestData.GetSampleCourseCreationRequest();

        dossierService.Setup(service => service.GetCourseCreationRequest(It.IsAny<Guid>())).ReturnsAsync(courseCreationRequest);
        dossierRepository.Setup(repository => repository.DeleteCourseCreationRequest(courseCreationRequest)).ReturnsAsync(false);

        await courseService.DeleteCourseCreationRequest(Guid.NewGuid());

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    public async Task DeleteCourseCreationRequest_ValidInput_Succeeds()
    {
        var courseCreationRequest = TestData.GetSampleCourseCreationRequest();

        dossierService.Setup(service => service.GetCourseCreationRequest(It.IsAny<Guid>())).ReturnsAsync(courseCreationRequest);
        dossierRepository.Setup(repository => repository.DeleteCourseCreationRequest(courseCreationRequest)).ReturnsAsync(true);

        await courseService.DeleteCourseCreationRequest(courseCreationRequest.Id);

        dossierRepository.Verify(r => r.DeleteCourseCreationRequest(courseCreationRequest));
    }

    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public async Task DeleteCourseModificationRequest_DoesNotExist_ThrowsNullReferenceException()
    {
        await courseService.DeleteCourseModificationRequest(Guid.NewGuid());
    }


    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task DeleteCourseModificationRequest_DoesNotDelete_LogsAndThrowsException()
    {
        var courseModificationRequest = TestData.GetSampleCourseModificationRequest();

        dossierService.Setup(service => service.GetCourseModificationRequest(It.IsAny<Guid>())).ReturnsAsync(courseModificationRequest);
        dossierRepository.Setup(repository => repository.DeleteCourseModificationRequest(courseModificationRequest)).ReturnsAsync(false);

        await courseService.DeleteCourseModificationRequest(Guid.NewGuid());

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    public async Task DeleteCourseModificationRequest_ValidInput_Succeeds()
    {
        var courseModificationRequest = TestData.GetSampleCourseModificationRequest();

        dossierService.Setup(service => service.GetCourseModificationRequest(It.IsAny<Guid>())).ReturnsAsync(courseModificationRequest);
        dossierRepository.Setup(repository => repository.DeleteCourseModificationRequest(courseModificationRequest)).ReturnsAsync(true);

        await courseService.DeleteCourseModificationRequest(courseModificationRequest.Id);

        dossierRepository.Verify(r => r.DeleteCourseModificationRequest(courseModificationRequest));
    }

    [TestMethod]
    [ExpectedException(typeof(NullReferenceException))]
    public async Task DeleteCourseDeletionRequest_DoesNotExist_ThrowsNullReferenceException()
    {
        await courseService.DeleteCourseDeletionRequest(Guid.NewGuid());
    }


    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task DeleteCourseDeletionRequest_DoesNotDelete_LogsAndThrowsException()
    {
        var courseDeletionRequest = TestData.GetSampleCourseDeletionRequest();

        dossierService.Setup(service => service.GetCourseDeletionRequest(It.IsAny<Guid>())).ReturnsAsync(courseDeletionRequest);
        dossierRepository.Setup(repository => repository.DeleteCourseDeletionRequest(courseDeletionRequest)).ReturnsAsync(false);

        await courseService.DeleteCourseDeletionRequest(Guid.NewGuid());

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    public async Task DeleteCourseDeletionnRequest_ValidInput_Succeeds()
    {
        var courseDeletionRequest = TestData.GetSampleCourseDeletionRequest();

        dossierService.Setup(service => service.GetCourseDeletionRequest(It.IsAny<Guid>())).ReturnsAsync(courseDeletionRequest);
        dossierRepository.Setup(repository => repository.DeleteCourseDeletionRequest(courseDeletionRequest)).ReturnsAsync(true);

        await courseService.DeleteCourseDeletionRequest(courseDeletionRequest.Id);

        dossierRepository.Verify(r => r.DeleteCourseDeletionRequest(courseDeletionRequest));
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidInputException))]
    public async Task GetCourseDataWithSupportingFilesOrThrowOnDeleted_CourseDoesNotExist_ThrowsInvalidInputException()
    {
        var subject = "SOEN";
        var catalog = "490";
        courseRepository.Setup(cr => cr.GetCourseWithSupportingFilesBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((Course?)null);

        await courseService.GetCourseDataWithSupportingFilesOrThrowOnDeleted(subject, catalog);
    }

    [TestMethod]
    [ExpectedException(typeof(BadRequestException))]
    public async Task GetCourseDataWithSupportingFilesOrThrowOnDeleted_CourseIsDeleted_ThrowsBadRequestException()
    {
        var subject = "SOEN";
        var catalog = "490";
        var course = TestData.GetSampleCourse();
        course.CourseState = CourseStateEnum.Deleted;
        courseRepository.Setup(cr => cr.GetCourseWithSupportingFilesBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(course);
        courseRepository.Setup(cr => cr.GetCourseByCourseIdAndLatestVersion(It.IsAny<int>())).ReturnsAsync(course);

        await courseService.GetCourseDataWithSupportingFilesOrThrowOnDeleted(subject, catalog);
    }

    [TestMethod]
    public async Task GetCourseDataWithSupportingFilesOrThrowOnDeleted_ValidCall_Succeeds()
    {
        var subject = "SOEN";
        var catalog = "490";
        var course = TestData.GetSampleCourse();
        courseRepository.Setup(cr => cr.GetCourseWithSupportingFilesBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(course);
        courseRepository.Setup(cr => cr.GetCourseByCourseIdAndLatestVersion(It.IsAny<int>())).ReturnsAsync(course);

        var courseData = await courseService.GetCourseDataWithSupportingFilesOrThrowOnDeleted(subject, catalog);

        Assert.IsNotNull(courseData);
    }
}
