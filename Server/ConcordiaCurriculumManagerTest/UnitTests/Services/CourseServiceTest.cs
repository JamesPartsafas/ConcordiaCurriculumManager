using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.InputDTOs;
using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Repositories;
using ConcordiaCurriculumManager.Services;
using ConcordiaCurriculumManagerTest.UnitTests.UtilityFunctions;
using Microsoft.Extensions.Logging;
using Moq;
using System;

namespace ConcordiaCurriculumManagerTest.UnitTests.Services;

[TestClass]
public class CourseServiceTest
{
    private Mock<ICourseRepository> courseRepository = null!;
    private Mock<IDossierService> dossierService = null!;
    private Mock<ILogger<CourseService>> logger = null!;
    private CourseService courseService = null!;
    private Mock<IDossierRepository> dossierRepository = null!;
    private Mock<ICourseGroupingRepository> courseGroupingRepository = null!;
    private Mock<ICourseIdentifiersRepository> courseIdentifierRepository = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        logger = new Mock<ILogger<CourseService>>();
        courseRepository = new Mock<ICourseRepository>();
        dossierService = new Mock<IDossierService>();
        dossierRepository = new Mock<IDossierRepository>();
        courseGroupingRepository = new Mock<ICourseGroupingRepository>();
        courseIdentifierRepository = new Mock<ICourseIdentifiersRepository>();

        courseService = new CourseService(logger.Object, courseRepository.Object, dossierService.Object, dossierRepository.Object, courseGroupingRepository.Object, courseIdentifierRepository.Object);
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
                State = CourseReferenceEnum.UpToDate
            },
            new()
            {
                CourseReferenced = course,
                CourseReferencedId = course.Id,
                CourseReferencing = course2,
                CourseReferencingId = course2.Id,
                State = CourseReferenceEnum.UpToDate
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
    [ExpectedException(typeof(BadRequestException))]
    public async Task InitiateCourseDeletion_CourseIsPartOfGrouping_Throws()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);
        var course = TestData.GetSampleCourse();
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(course);
        dossierService.Setup(cr => cr.GetDossierDetailsById(It.IsAny<Guid>())).ReturnsAsync(TestData.GetSampleDossier(user));
        courseGroupingRepository.Setup(cgr => cgr.GetCourseGroupingContainingCourse(course)).ReturnsAsync(TestData.GetSampleCourseGrouping());

        await courseService.InitiateCourseDeletion(TestData.GetSampleCourseCreationDeletionDTO(course, dossier), user.Id);
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
                State = CourseReferenceEnum.UpToDate
            },
            new()
            {
                CourseReferenced = course,
                CourseReferencedId = course.Id,
                CourseReferencing = course2,
                CourseReferencingId = course2.Id,
                State = CourseReferenceEnum.UpToDate
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

    [TestMethod]
    public async Task GetCourseByIdAsync_ValidId_ReturnsCourse()
    {
        var expectedCourse = TestData.GetSampleCourse();
        var courseId = expectedCourse.Id;

        courseRepository.Setup(repo => repo.GetCourseByIdAsync(courseId)).ReturnsAsync(expectedCourse);

        var result = await courseService.GetCourseByIdAsync(courseId);

        Assert.IsNotNull(result, "The course should not be null.");
        Assert.AreEqual(expectedCourse, result, "The retrieved course does not match the expected course.");
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException), "A NotFoundException should be thrown when the course does not exist.")]
    public async Task GetCourseByIdAsync_InvalidId_ThrowsNotFoundException()
    {
        var nonExistentCourseId = Guid.NewGuid();
        courseRepository.Setup(repo => repo.GetCourseByIdAsync(nonExistentCourseId)).ReturnsAsync((Course?)null);

        var result = await courseService.GetCourseByIdAsync(nonExistentCourseId);

        Assert.Fail("Expected a NotFoundException to be thrown.");
    }

    [TestMethod]
    public async Task GetCoursesBySubjectAsync_ReturnsCoursesForSubject()
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
            }
        };

        courseRepository.Setup(repo => repo.GetCoursesBySubjectAsync(subjectCode))
            .ReturnsAsync(courses);

        var result = await courseService.GetCoursesBySubjectAsync(subjectCode);

        courseRepository.Verify(repo => repo.GetCoursesBySubjectAsync(subjectCode), Times.Once);
        Assert.AreEqual(courses.Count, result.Count());
    }

    [TestMethod]
    public async Task PublishCourse_ValidInput_ReturnsPublishedCourse()
    {
        var newCourse = TestData.GetSampleAcceptedCourse();
        var oldCourse = TestData.GetSamplePublisheddCourse();

        courseRepository.Setup(repo => repo.GetCourseBySubjectAndCatalog(newCourse.Subject, newCourse.Catalog)).ReturnsAsync(newCourse);
        courseRepository.Setup(repo => repo.GetPublishedVersion(oldCourse.Subject, oldCourse.Catalog)).ReturnsAsync(oldCourse);
        courseRepository.Setup(repo => repo.UpdateCourse(newCourse)).ReturnsAsync(true);
        courseRepository.Setup(repo => repo.UpdateCourse(oldCourse)).ReturnsAsync(true);

        var result = await courseService.PublishCourse(newCourse.Subject, newCourse.Catalog);

        Assert.IsNotNull(result);
        Assert.AreEqual(true, result.Published);
    }

    [TestMethod]
    public async Task PublishCourse_NewCourseIsDelete_CallsInvalidateAllReferences()
    {
        var newCourse = TestData.GetSampleAcceptedCourse();
        var oldCourse = TestData.GetSamplePublisheddCourse();

        newCourse.CourseState = CourseStateEnum.Deleted;
        courseRepository.Setup(repo => repo.GetCourseBySubjectAndCatalog(newCourse.Subject, newCourse.Catalog)).ReturnsAsync(newCourse);
        courseRepository.Setup(repo => repo.GetPublishedVersion(oldCourse.Subject, oldCourse.Catalog)).ReturnsAsync(oldCourse);
        courseRepository.Setup(repo => repo.UpdateCourse(newCourse)).ReturnsAsync(true);
        courseRepository.Setup(repo => repo.UpdateCourse(oldCourse)).ReturnsAsync(true);

        var result = await courseService.PublishCourse(newCourse.Subject, newCourse.Catalog);

        Assert.IsNotNull(result);
        Assert.AreEqual(true, result.Published);
        courseRepository.Verify(m => m.InvalidateAllCourseReferences(oldCourse.Id), Times.Once);
    }

    [TestMethod]
    public async Task PublishCourse_NewCourseIsNotDelete_CallsUpdateCourseReferences()
    {
        var newCourse = TestData.GetSampleAcceptedCourse();
        var oldCourse = TestData.GetSamplePublisheddCourse();

        newCourse.CourseState = CourseStateEnum.Accepted;
        newCourse.Description = "Design of classes. Inheritance. Polymorphism. Static and dynamic binding. Abstract classes. Exception handling. File I/O. Recursion. Interfaces and inner classes. Graphical user interfaces. Generics. Collections and iterators. Lectures: three hours per week. Tutorial: two hours per week. Laboratory:one hour per week.Prerequisite: COMP 248; MATH 203 or Cegep Mathematics 103 or NYA; MATH 205 or Cegep Mathematics 203 or NYB previously or concurrently.";
        courseRepository.Setup(repo => repo.GetCourseBySubjectAndCatalog(newCourse.Subject, newCourse.Catalog)).ReturnsAsync(newCourse);
        courseRepository.Setup(repo => repo.GetPublishedVersion(oldCourse.Subject, oldCourse.Catalog)).ReturnsAsync(oldCourse);
        courseRepository.Setup(repo => repo.UpdateCourse(newCourse)).ReturnsAsync(true);
        courseRepository.Setup(repo => repo.UpdateCourse(oldCourse)).ReturnsAsync(true);

        var expectedCourseSubjectAndCatalogExtracted = new List<(string, string)>()
        {
            ("COMP", "248"),
            ("MATH", "203"),
            ("MATH", "205")
        };

        var result = await courseService.PublishCourse(newCourse.Subject, newCourse.Catalog);

        Assert.IsNotNull(result);
        Assert.AreEqual(true, result.Published);
        courseRepository.Verify(m => m.UpdateCourseReferences(oldCourse, newCourse, It.Is<List<(string, string)>>(l => expectedCourseSubjectAndCatalogExtracted.TrueForAll(c => l.Contains(c)))), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException), "A NotFoundException should be thrown when the course does not exist.")]
    public async Task PublishCourse_InvalidInput_ThrowsNotFoundException()
    {
        var course = TestData.GetSampleCourse();
        courseRepository.Setup(repo => repo.GetCourseBySubjectAndCatalog(course.Subject, course.Catalog)).ReturnsAsync((Course?)null);

        var result = await courseService.PublishCourse(course.Subject, course.Catalog);

        Assert.Fail("Expected a NotFoundException to be thrown.");
    }
}
