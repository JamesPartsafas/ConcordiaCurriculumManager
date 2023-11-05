using ConcordiaCurriculumManager.DTO.Courses;
using ConcordiaCurriculumManager.DTO.Dossiers;
﻿using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests;
using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories;
using ConcordiaCurriculumManager.Services;
using ConcordiaCurriculumManager.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public async Task GetCourseData_CourseDoesNotExist_ThrowsArgumentException()
    {
        var subject = "SOEN";
        var catalog = "490";
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((Course?)null);

        await courseService.GetCourseData(subject, catalog);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task GetCourseData_CourseIsDeleted_ThrowsArgumentException()
    {
        var subject = "SOEN";
        var catalog = "490";
        var course = GetSampleCourse();
        course.CourseState = CourseStateEnum.Deleted;
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(course);
        courseRepository.Setup(cr => cr.GetCourseByCourseIdAndLatestVersion(It.IsAny<int>())).ReturnsAsync(course);

        await courseService.GetCourseData(subject, catalog);
    }

    [TestMethod]
    public async Task GetCourseData_ValidCall_Succeeds()
    {
        var subject = "SOEN";
        var catalog = "490";
        var course = GetSampleCourse();
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(course);
        courseRepository.Setup(cr => cr.GetCourseByCourseIdAndLatestVersion(It.IsAny<int>())).ReturnsAsync(course);

        var courseData = await courseService.GetCourseData(subject, catalog);

        Assert.IsNotNull(courseData);
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
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(course);

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
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(course);
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
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(course);
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
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(course);
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
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(course);
        dossierService.Setup(cr => cr.GetDossierDetailsById(It.IsAny<Guid>())).ReturnsAsync(GetSampleDossier(user));
        courseRepository.Setup(cr => cr.SaveCourse(It.IsAny<Course>())).ReturnsAsync(true);
        dossierService.Setup(ds => ds.GetDossierForUserOrThrow(dossier.Id, user.Id)).ReturnsAsync(dossier);
        dossierService.Setup(cr => cr.SaveCourseDeletionRequest(It.IsAny<CourseDeletionRequest>())).Returns(Task.CompletedTask);

        var courseDeletionRequest = await courseService.InitiateCourseDeletion(GetSampleCourseCreationDeletionDTO(course, dossier), user.Id);

        Assert.IsNotNull(courseDeletionRequest);
    }

    [TestMethod]
    public async Task EditCourseCreationRequest_ValidInput_Succeeds()
    {
        var courseCreationRequest = GetSampleCourseCreationRequest();
        var editCourseCreationRequestDTO = GetSampleEditCourseCreationRequestDTO();
        dossierService.Setup(service => service.GetCourseCreationRequest(It.IsAny<Guid>())).ReturnsAsync(courseCreationRequest);
        dossierRepository.Setup(repository => repository.UpdateCourseCreationRequest(courseCreationRequest)).ReturnsAsync(true);

        var editCourseCreationRequest = await courseService.EditCourseCreationRequest(editCourseCreationRequestDTO);

        Assert.IsNotNull(editCourseCreationRequest);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task EditCourseCreationRequest_CourseDoesNotExist_ThrowsArgumentException()
    {
        var courseCreationRequest = GetSampleCourseCreationRequest();
        courseCreationRequest.NewCourse = null;
        var editCourseCreationRequestDTO = GetSampleEditCourseCreationRequestDTO();
        dossierService.Setup(service => service.GetCourseCreationRequest(It.IsAny<Guid>())).ReturnsAsync(courseCreationRequest);

        await courseService.EditCourseCreationRequest(editCourseCreationRequestDTO);

    }

    [TestMethod]
    public async Task EditCourseModificationRequest_ValidInput_Succeeds()
    {
        var courseModificationRequest = GetSampleCourseModificationRequest();
        var editCourseModificationRequestDTO = GetSampleEditCourseModificationRequestDTO();
        dossierService.Setup(service => service.GetCourseModificationRequest(It.IsAny<Guid>())).ReturnsAsync(courseModificationRequest);
        dossierRepository.Setup(repository => repository.UpdateCourseModificationRequest(courseModificationRequest)).ReturnsAsync(true);

        var editCourseCreationRequest = await courseService.EditCourseModificationRequest(editCourseModificationRequestDTO);

        Assert.IsNotNull(editCourseCreationRequest);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task EditCourseModificationRequest_CourseDoesNotExist_ThrowsArgumentException()
    {
        var courseModificationRequest = GetSampleCourseModificationRequest();
        courseModificationRequest.Course = null;
        var editCourseModificationRequestDTO = GetSampleEditCourseModificationRequestDTO();
        dossierService.Setup(service => service.GetCourseModificationRequest(It.IsAny<Guid>())).ReturnsAsync(courseModificationRequest);

        await courseService.EditCourseModificationRequest(editCourseModificationRequestDTO);

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
        var courseCreationRequest = GetSampleCourseCreationRequest();

        dossierService.Setup(service => service.GetCourseCreationRequest(It.IsAny<Guid>())).ReturnsAsync(courseCreationRequest);
        dossierRepository.Setup(repository => repository.DeleteCourseCreationRequest(courseCreationRequest)).ReturnsAsync(false);

        await courseService.DeleteCourseCreationRequest(Guid.NewGuid());

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    public async Task DeleteCourseCreationRequest_ValidInput_Succeeds()
    {
        var courseCreationRequest = GetSampleCourseCreationRequest();

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
        var courseModificationRequest = GetSampleCourseModificationRequest();

        dossierService.Setup(service => service.GetCourseModificationRequest(It.IsAny<Guid>())).ReturnsAsync(courseModificationRequest);
        dossierRepository.Setup(repository => repository.DeleteCourseModificationRequest(courseModificationRequest)).ReturnsAsync(false);

        await courseService.DeleteCourseModificationRequest(Guid.NewGuid());

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    public async Task DeleteCourseModificationRequest_ValidInput_Succeeds()
    {
        var courseModificationRequest = GetSampleCourseModificationRequest();

        dossierService.Setup(service => service.GetCourseModificationRequest(It.IsAny<Guid>())).ReturnsAsync(courseModificationRequest);
        dossierRepository.Setup(repository => repository.DeleteCourseModificationRequest(courseModificationRequest)).ReturnsAsync(true);

        await courseService.DeleteCourseModificationRequest(courseModificationRequest.Id);

        dossierRepository.Verify(r => r.DeleteCourseModificationRequest(courseModificationRequest));
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
            CourseState = CourseStateEnum.Accepted,
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
            Rationale = "It's necessary",
            ResourceImplication = "New prof needed",
            DossierId = dossier.Id,
            Subject = course.Subject,
            Catalog = course.Catalog
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

    private EditCourseModificationRequestDTO GetSampleEditCourseModificationRequestDTO()
    {
        return new EditCourseModificationRequestDTO
        {
            Id = Guid.NewGuid(),
            DossierId = Guid.NewGuid(),
            Rationale = "It's necessary",
            ResourceImplication = "New prof needed",
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

    private CourseCreationRequest GetSampleCourseCreationRequest()
    {
        return new CourseCreationRequest
        {
            DossierId = Guid.NewGuid(),
            NewCourseId = Guid.NewGuid(),
            NewCourse = GetSampleCourse(),
            Rationale = "It's necessary",
            ResourceImplication = "New prof needed",
            Comment = "Fun",
        };
    }

    private CourseModificationRequest GetSampleCourseModificationRequest()
    {
        return new CourseModificationRequest
        {
            DossierId = Guid.NewGuid(),
            CourseId = Guid.NewGuid(),
            Course = GetSampleCourse(),
            Rationale = "It's necessary",
            ResourceImplication = "New prof needed",
            Comment = "Fun",
        };
    }
}
