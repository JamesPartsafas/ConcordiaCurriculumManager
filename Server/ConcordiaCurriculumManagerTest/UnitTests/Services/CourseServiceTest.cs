using ConcordiaCurriculumManager.DTO.Dossiers;
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
    private Mock<IDossierRepository> dossierRepository = null!;
    private Mock<ILogger<CourseService>> logger = null!;
    private CourseService courseService = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        logger = new Mock<ILogger<CourseService>>();
        courseRepository = new Mock<ICourseRepository>();
        dossierRepository = new Mock<IDossierRepository>();

        courseService = new CourseService(logger.Object, courseRepository.Object, dossierRepository.Object);
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
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(GetSampleCourse());

        await courseService.InitiateCourseCreation(GetSampleCourseCreationInitiationDTO(), GetSampleUser(), Guid.NewGuid());
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task InitiateCourseCreation_DossierDoesNotExist_LogsAndThrowsException()
    {
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((Course?)null);

        await courseService.InitiateCourseCreation(GetSampleCourseCreationInitiationDTO(), GetSampleUser(), Guid.NewGuid());

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task InitiateCourseCreation_DossierDoesNotBelongToUser_LogsAndThrowsException()
    {
        var user = GetSampleUser();
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((Course?)null);
        dossierRepository.Setup(cr => cr.GetDossierByDossierId(It.IsAny<Guid>())).ReturnsAsync(GetSampleDossier(user));

        await courseService.InitiateCourseCreation(GetSampleCourseCreationInitiationDTO(), GetSampleUser(), Guid.NewGuid());

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));

    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task InitiateCourseCreation_CourseDoesNotAdd_LogsAndThrowsException()
    {
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((Course?)null);
        courseRepository.Setup(cr => cr.GetMaxCourseId()).ReturnsAsync(5);
        courseRepository.Setup(cr => cr.SaveCourse(It.IsAny<Course>())).ReturnsAsync(false);

        await courseService.InitiateCourseCreation(GetSampleCourseCreationInitiationDTO(), GetSampleUser(), Guid.NewGuid());

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task InitiateCourseCreation_DossierDoesNotSave_LogsAndThrowsException()
    {
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((Course?)null);
        courseRepository.Setup(cr => cr.GetMaxCourseId()).ReturnsAsync(5);
        courseRepository.Setup(cr => cr.SaveCourse(It.IsAny<Course>())).ReturnsAsync(true);
        dossierRepository.Setup(cr => cr.SaveCourseCreationRequest(It.IsAny<CourseCreationRequest>())).ReturnsAsync(false);

        await courseService.InitiateCourseCreation(GetSampleCourseCreationInitiationDTO(), GetSampleUser(), Guid.NewGuid());

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    public async Task InitiateCourseCreation_ValidInput_Succeeds()
    {
        var user = GetSampleUser();
        courseRepository.Setup(cr => cr.GetCourseBySubjectAndCatalog(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((Course?)null);
        courseRepository.Setup(cr => cr.GetMaxCourseId()).ReturnsAsync(5);
        courseRepository.Setup(cr => cr.SaveCourse(It.IsAny<Course>())).ReturnsAsync(true);
        dossierRepository.Setup(cr => cr.SaveCourseCreationRequest(It.IsAny<CourseCreationRequest>())).ReturnsAsync(true);
        dossierRepository.Setup(cr => cr.GetDossierByDossierId(It.IsAny<Guid>())).ReturnsAsync(GetSampleDossier(user));
        dossierRepository.Setup(cr => cr.SaveDossier(It.IsAny<Dossier>())).ReturnsAsync(true);
        

        var dossier = await courseService.InitiateCourseCreation(GetSampleCourseCreationInitiationDTO(), user, user.Id);

        Assert.AreEqual(user.Id, dossier.InitiatorId);
    }

    private Course GetSampleCourse()
    {
        return new Course
        {
            Id = Guid.NewGuid(),
            CourseID = 1000,
            Subject = "SOEN",
            Catalog = "490",
            Title = "Capstone",
            Description = "Curriculum manager building simulator",
            CreditValue = "6",
            PreReqs = "SOEN 390",
            Career = CourseCareerEnum.UGRD,
            EquivalentCourses = "",
            CourseState = CourseStateEnum.NewCourseProposal,
            Version = 1,
            Published = true,
            CourseComponents = (List<CourseComponent>)ComponentCodeMapping.GetComponentCodeMapping(
                new ComponentCodeEnum[] { ComponentCodeEnum.LEC, ComponentCodeEnum.CON }
            )
        };
    }

    private CourseCreationInitiationDTO GetSampleCourseCreationInitiationDTO()
    {
        return new CourseCreationInitiationDTO
        {
            Subject = "SOEN",
            Catalog = "490",
            Title = "Capstone",
            Description = "Curriculum manager building simulator",
            CreditValue = "6",
            PreReqs = "SOEN 390",
            Career = CourseCareerEnum.UGRD,
            EquivalentCourses = "",
            ComponentCodes = new List<ComponentCodeEnum> { ComponentCodeEnum.LEC, ComponentCodeEnum.CON }
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
            CourseCreationRequests = new List<CourseCreationRequest> { new CourseCreationRequest { } }
        };
    }
}
