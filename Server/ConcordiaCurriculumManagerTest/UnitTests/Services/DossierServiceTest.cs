using ConcordiaCurriculumManager.DTO.Dossiers;
using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories;
using ConcordiaCurriculumManager.Services;
using ConcordiaCurriculumManagerTest.UnitTests.UtilityFunctions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;

namespace ConcordiaCurriculumManagerTest.UnitTests.Services;

[TestClass]
public class DossierServiceTest
{
    private Mock<IDossierRepository> dossierRepository = null!;
    private Mock<ILogger<DossierService>> logger = null!;
    private Mock<ICourseRepository> courseRepository = null!;
    private Mock<ICourseGroupingRepository> courseGroupingRepository = null!;
    private DossierService dossierService = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        logger = new Mock<ILogger<DossierService>>();
        dossierRepository = new Mock<IDossierRepository>();
        courseRepository = new Mock<ICourseRepository>();
        courseGroupingRepository = new Mock<ICourseGroupingRepository>();

        dossierService = new DossierService(logger.Object, dossierRepository.Object, courseRepository.Object, courseGroupingRepository.Object);
    }

    [TestMethod]
    public async Task GetDossiersByID_ValidCall_QueriesRepo()
    {
        var user = TestData.GetSampleUser();
        dossierRepository.Setup(d => d.GetDossiersByID(It.IsAny<Guid>())).ReturnsAsync(new List<Dossier>() { TestData.GetSampleDossier(user) });
        await dossierService.GetDossiersByID(user.Id);

        dossierRepository.Verify(d => d.GetDossiersByID(user.Id));
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task CreateDossierForUser_DossierDoesNotSave_LogsAndThrowsException()
    {
        dossierRepository.Setup(d => d.SaveDossier(It.IsAny<Dossier>())).ReturnsAsync(false);

        await dossierService.CreateDossierForUser(TestData.GetSampleCreateDossierDTO(), TestData.GetSampleUser());

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    public async Task CreateDossierForUser_ValidInput_Succeeds()
    {
        dossierRepository.Setup(d => d.SaveDossier(It.IsAny<Dossier>())).ReturnsAsync(true);
        var user = TestData.GetSampleUser();

        var dossier = await dossierService.CreateDossierForUser(TestData.GetSampleCreateDossierDTO(), user);

        Assert.AreEqual(user.Id, dossier.InitiatorId);
    }

    [TestMethod]
    public async Task RetrieveDossierDetails_ValidInput_Succeeds()
    {
        var dossier = TestData.GetSampleDossier();
        dossierRepository.Setup(d => d.GetDossierByDossierId(It.IsAny<Guid>())).ReturnsAsync(dossier);

        var dossierDetails = await dossierService.GetDossierDetailsById(Guid.NewGuid());

        Assert.IsNotNull(dossierDetails);
        Assert.AreEqual(dossier.Id, dossierDetails.Id);
    }

    [TestMethod]
    public async Task RetrieveDossierDetails_InvalidInput_ReturnsNull()
    {
        dossierRepository.Setup(d => d.GetDossierByDossierId(It.IsAny<Guid>())).ReturnsAsync((Dossier)null!);

        var result = await dossierService.GetDossierDetailsById(Guid.NewGuid());

        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task RetrieveDossierDetailsOrThrow_ValidId_ReturnsDossier()
    {
        var dossier = TestData.GetSampleDossier();
        dossierRepository.Setup(d => d.GetDossierByDossierId(It.IsAny<Guid>())).ReturnsAsync(dossier);

        var dossierDetails = await dossierService.GetDossierDetailsByIdOrThrow(Guid.NewGuid());

        Assert.IsNotNull(dossierDetails);
        Assert.AreEqual(dossier.Id, dossierDetails.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public async Task RetrieveDossierDetailsOrThrow_InvalidId_Throws()
    {
        dossierRepository.Setup(d => d.GetDossierByDossierId(It.IsAny<Guid>())).ReturnsAsync((Dossier)null!);

        var result = await dossierService.GetDossierDetailsByIdOrThrow(Guid.NewGuid());

        Assert.IsNull(result);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task EditDossier_InvalidInput_ThrowsArgumentException()
    {
        dossierRepository.Setup(d => d.GetDossierByDossierId(It.IsAny<Guid>())).ReturnsAsync((Dossier)null!);

        await dossierService.EditDossier(TestData.GetSampleEditDossierDTO(), Guid.NewGuid());
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task Edit_DossierDoesNotUpdate_LogsAndThrowsException()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier();

        var editDossier = new EditDossierDTO
        {
            InitiatorId = user.Id,
            Title = "test title modified",
            Description = "test description modified"
        };


        dossierRepository.Setup(d => d.GetDossierByDossierId(It.IsAny<Guid>())).ReturnsAsync(dossier);
        dossierRepository.Setup(d => d.UpdateDossier(It.IsAny<Dossier>())).ReturnsAsync(false);

        await dossierService.EditDossier(TestData.GetSampleEditDossierDTO(), Guid.NewGuid());

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    public async Task EditDossierForUser_ValidInput_Succeeds()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier();

        var editDossier = new EditDossierDTO
        {
            InitiatorId = user.Id,
            Title = "test title modified",
            Description = "test description modified"
        };


        dossierRepository.Setup(d => d.GetDossierByDossierId(It.IsAny<Guid>())).ReturnsAsync(dossier);
        dossierRepository.Setup(d => d.UpdateDossier(It.IsAny<Dossier>())).ReturnsAsync(true);

        var editedDossier = await dossierService.EditDossier(editDossier, Guid.NewGuid());

        Assert.AreEqual(editDossier.Title, editedDossier.Title);
        Assert.AreEqual(editDossier.Description, editedDossier.Description);

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task DeleteDossier_DoesNotExist_ThrowsArgumentException()
    {
        dossierRepository.Setup(d => d.GetDossierByDossierId(It.IsAny<Guid>())).ReturnsAsync((Dossier?)null);

        await dossierService.DeleteDossier(Guid.NewGuid());
    }


    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task DeleteDossier_DoesNotUpdate_LogsAndThrowsException()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier();

        dossierRepository.Setup(d => d.GetDossierByDossierId(It.IsAny<Guid>())).ReturnsAsync(dossier);
        dossierRepository.Setup(d => d.DeleteDossier(It.IsAny<Dossier>())).ReturnsAsync(false);

        await dossierService.DeleteDossier(Guid.NewGuid());

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    public async Task DeleteDossierValidInput_Succeeds()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier();

        var deletedDossier = dossier.Id;

        dossierRepository.Setup(d => d.GetDossierByDossierId(It.IsAny<Guid>())).ReturnsAsync(dossier);
        dossierRepository.Setup(d => d.DeleteDossier(It.IsAny<Dossier>())).ReturnsAsync(true);

        await dossierService.DeleteDossier(deletedDossier);

        dossierRepository.Verify(r => r.DeleteDossier(dossier));
    }

    [TestMethod]
    public async Task GetDossierForUserOrThrow_ValidInput_ReturnsDossier()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier(user);

        dossierRepository.Setup(d => d.GetDossierByDossierId(dossier.Id)).ReturnsAsync(dossier);

        var returnedDossier = await dossierService.GetDossierForUserOrThrow(dossier.Id, user.Id);

        Assert.AreEqual(dossier.Id, returnedDossier.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task GetDossierForUserOrThrow_DossierNotFound_Throws()
    {
        dossierRepository.Setup(d => d.GetDossierByDossierId(It.IsAny<Guid>())).ReturnsAsync((Dossier)null!);

        await dossierService.GetDossierForUserOrThrow(Guid.NewGuid(), Guid.NewGuid());
    }

    [TestMethod]
    [ExpectedException(typeof(BadRequestException))]
    public async Task GetDossierForUserOrThrow_DossierDoesNotBelongToUser_Throws()
    {
        var user = TestData.GetSampleUser();
        var dossier = TestData.GetSampleDossier();
        dossierRepository.Setup(d => d.GetDossierByDossierId(It.IsAny<Guid>())).ReturnsAsync(dossier);

        await dossierService.GetDossierForUserOrThrow(Guid.NewGuid(), Guid.NewGuid());

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    public async Task SaveCourseCreationRequest_ValidInput_Saves()
    {
        dossierRepository.Setup(d => d.SaveCourseCreationRequest(It.IsAny<CourseCreationRequest>())).ReturnsAsync(true);

        await dossierService.SaveCourseCreationRequest(TestData.GetSampleCourseCreationRequest());
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task SaveCourseCreationRequest_InvalidInput_Throws()
    {
        dossierRepository.Setup(d => d.SaveCourseCreationRequest(It.IsAny<CourseCreationRequest>())).ReturnsAsync(false);

        await dossierService.SaveCourseCreationRequest(TestData.GetSampleCourseCreationRequest());

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    public async Task SaveCourseModificationRequest_ValidInput_Saves()
    {
        dossierRepository.Setup(d => d.SaveCourseModificationRequest(It.IsAny<CourseModificationRequest>())).ReturnsAsync(true);

        await dossierService.SaveCourseModificationRequest(TestData.GetSampleCourseModificationRequest());
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task SaveCourseModificationRequest_InvalidInput_Throws()
    {
        dossierRepository.Setup(d => d.SaveCourseModificationRequest(It.IsAny<CourseModificationRequest>())).ReturnsAsync(false);

        await dossierService.SaveCourseModificationRequest(TestData.GetSampleCourseModificationRequest());

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    public async Task GetCourseCreationRequest_ValidInput_ReturnsCourseCreationRequest()
    {
        var courseCreationRequest = TestData.GetSampleCourseCreationRequest();
        dossierRepository.Setup(d => d.GetCourseCreationRequest(It.IsAny<Guid>())).ReturnsAsync(courseCreationRequest);

        await dossierService.GetCourseCreationRequest(courseCreationRequest.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public async Task GetCourseCreationRequest_InvalidInput_Throws()
    {
        dossierRepository.Setup(d => d.GetCourseCreationRequest(It.IsAny<Guid>())).ReturnsAsync((CourseCreationRequest)null!);

        await dossierService.GetCourseCreationRequest(TestData.GetSampleCourseCreationRequest().Id);

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    public async Task GetCourseModificationRequest_ValidInput_ReturnsCourseCreationRequest()
    {
        var courseModificationRequest = TestData.GetSampleCourseModificationRequest();
        dossierRepository.Setup(d => d.GetCourseModificationRequest(It.IsAny<Guid>())).ReturnsAsync(courseModificationRequest);

        await dossierService.GetCourseModificationRequest(courseModificationRequest.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public async Task GetCourseModificationRequest_InvalidInput_Throws()
    {
        dossierRepository.Setup(d => d.GetCourseModificationRequest(It.IsAny<Guid>())).ReturnsAsync((CourseModificationRequest)null!);

        await dossierService.GetCourseModificationRequest(TestData.GetSampleCourseModificationRequest().Id);

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    public async Task GetCourseDeletionRequest_ValidInput_ReturnsCourseCreationRequest()
    {
        var courseDeletionRequest = TestData.GetSampleCourseDeletionRequest();
        dossierRepository.Setup(d => d.GetCourseDeletionRequest(It.IsAny<Guid>())).ReturnsAsync(courseDeletionRequest);

        await dossierService.GetCourseDeletionRequest(courseDeletionRequest.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public async Task GetCourseDeletionRequest_InvalidInput_Throws()
    {
        dossierRepository.Setup(d => d.GetCourseDeletionRequest(It.IsAny<Guid>())).ReturnsAsync((CourseDeletionRequest)null!);

        await dossierService.GetCourseDeletionRequest(TestData.GetSampleCourseDeletionRequest().Id);

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    public async Task GetDossierReportByDossierId_ValidInput_ReturnsDossierReport()
    {
        var dossier = TestData.GetSampleDossier();
        dossierRepository.Setup(d => d.GetDossierReportByDossierId(It.IsAny<Guid>())).ReturnsAsync(dossier);

        var dossierReport = await dossierService.GetDossierReportByDossierId(dossier.Id);

        Assert.IsNotNull(dossierReport);
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public async Task GetDossierReportByDossierId_InvalidInput_Throws()
    {
        dossierRepository.Setup(d => d.GetDossierReportByDossierId(It.IsAny<Guid>())).ReturnsAsync((Dossier)null!);

        await dossierService.GetDossierReportByDossierId(TestData.GetSampleDossier().Id);

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    public async Task GetDossiersRequiredReview_ValidInput_ReturnsDossiersRequireReview()
    {
        var dossier = TestData.GetSampleDossierInInitialStage();
        var dossiers = new List<Dossier> { dossier };
        dossierRepository.Setup(d => d.GetDossiersRequiredReview(It.IsAny<Guid>())).ReturnsAsync(dossiers);

        await dossierService.GetDossiersRequiredReview(dossier.InitiatorId);
    }

    [TestMethod]
    public async Task GetChangesAcrossAllDossiers_ValidCall_ReturnsCourseChanges()
    {
        dossierRepository.Setup(d => d.GetChangesAcrossAllDossiers()).ReturnsAsync(new List<Course> { TestData.GetSampleAcceptedCourse() });

        var courseChanges = await dossierService.GetChangesAcrossAllDossiers();

        Assert.IsNotNull(courseChanges);
    }

    [TestMethod]
    public async Task SearchDossiers_ValidCall_ReturnsDossiers()
    {
        var dossier = TestData.GetSampleDossierInInitialStage();
        dossierRepository.Setup(d => d.SearchDossiers(dossier.Title, dossier.State, dossier.Id)).ReturnsAsync(new List<Dossier> { dossier });

        await dossierService.SearchDossiers(dossier.Title, dossier.State, dossier.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidInputException))]
    public async Task SearchDossiers_InvalidCall_ThrowsInvalidInputException()
    {
        var dossier = TestData.GetSampleDossier();
        dossierRepository.Setup(d => d.SearchDossiers(dossier.Title, dossier.State, dossier.Id)).ReturnsAsync(new List<Dossier>());

        await dossierService.SearchDossiers(dossier.Title, dossier.State, dossier.Id);
    }
}

