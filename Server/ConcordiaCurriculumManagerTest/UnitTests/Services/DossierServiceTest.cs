using ConcordiaCurriculumManager.DTO.Dossiers;
using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories;
using ConcordiaCurriculumManager.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace ConcordiaCurriculumManagerTest.UnitTests.Services;

[TestClass]
public class DossierServiceTest
{
    private Mock<IDossierRepository> dossierRepository = null!;
    private Mock<ILogger<DossierService>> logger = null!;
    private DossierService dossierService = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        logger = new Mock<ILogger<DossierService>>();
        dossierRepository = new Mock<IDossierRepository>();

        dossierService = new DossierService(logger.Object, dossierRepository.Object);
    }

    [TestMethod]
    public async Task GetDossiersByID_ValidCall_QueriesRepo()
    {
        dossierRepository.Setup(d => d.GetDossiersByID(It.IsAny<Guid>())).ReturnsAsync(new List<Dossier>() { GetSampleDossier(GetSampleUser()) });
        await dossierService.GetDossiersByID(GetSampleUser().Id);

        dossierRepository.Verify(d => d.GetDossiersByID(GetSampleUser().Id));
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task CreateDossierForUser_DossierDoesNotSave_LogsAndThrowsException()
    {
        dossierRepository.Setup(d => d.SaveDossier(It.IsAny<Dossier>())).ReturnsAsync(false);

        await dossierService.CreateDossierForUser(GetSampleCreateDossierDTO(), GetSampleUser());

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    public async Task CreateDossierForUser_ValidInput_Succeeds()
    {
        dossierRepository.Setup(d => d.SaveDossier(It.IsAny<Dossier>())).ReturnsAsync(true);
        var user = GetSampleUser();

        var dossier = await dossierService.CreateDossierForUser(GetSampleCreateDossierDTO(), user);

        Assert.AreEqual(user.Id, dossier.InitiatorId);
    }

    [TestMethod]
    public async Task RetrieveDossierDetails_ValidInput_Succeeds()
    {
        var dossier = GetSampleDossier();
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
        var dossier = GetSampleDossier();
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

        await dossierService.EditDossier(GetSampleEditDossierDTO(), Guid.NewGuid());
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task Edit_DossierDoesNotUpdate_LogsAndThrowsException()
    {
        var user = GetSampleUser();
        var dossier = new Dossier
        {
            InitiatorId = user.Id,
            Title = "test title",
            Description = "test description",
            Published = false
        };

        var editDossier = new EditDossierDTO
        {
            InitiatorId = user.Id,
            Title = "test title modified",
            Description = "test description modified"
        };


        dossierRepository.Setup(d => d.GetDossierByDossierId(It.IsAny<Guid>())).ReturnsAsync(dossier);
        dossierRepository.Setup(d => d.UpdateDossier(It.IsAny<Dossier>())).ReturnsAsync(false);

        await dossierService.EditDossier(GetSampleEditDossierDTO(), Guid.NewGuid());

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    public async Task EditDossierForUser_ValidInput_Succeeds()
    {
        var user = GetSampleUser();
        var dossier = new Dossier
        {
            InitiatorId = user.Id,
            Title = "test title",
            Description = "test description",
            Published = false
        };

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
        var user = GetSampleUser();
        var dossier = new Dossier
        {
            InitiatorId = user.Id,
            Title = "test title",
            Description = "test description",
            Published = false
        };

        dossierRepository.Setup(d => d.GetDossierByDossierId(It.IsAny<Guid>())).ReturnsAsync(dossier);
        dossierRepository.Setup(d => d.DeleteDossier(It.IsAny<Dossier>())).ReturnsAsync(false);

        await dossierService.DeleteDossier(Guid.NewGuid());

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    public async Task DeleteDossierValidInput_Succeeds()
    {
        var user = GetSampleUser();
        var dossier = new Dossier
        {
            Id = Guid.NewGuid(),
            InitiatorId = user.Id,
            Title = "test title",
            Description = "test description",
            Published = false
        };

        var deletedDossier = dossier.Id;

        dossierRepository.Setup(d => d.GetDossierByDossierId(It.IsAny<Guid>())).ReturnsAsync(dossier);
        dossierRepository.Setup(d => d.DeleteDossier(It.IsAny<Dossier>())).ReturnsAsync(true);

        await dossierService.DeleteDossier(deletedDossier);

        dossierRepository.Verify(r => r.DeleteDossier(dossier));
    }

    [TestMethod]
    public async Task GetDossierForUserOrThrow_ValidInput_ReturnsDossier()
    {
        var user = GetSampleUser();
        var dossier = GetSampleDossier(user);

        dossierRepository.Setup(d => d.GetDossierByDossierId(user.Id)).ReturnsAsync(dossier);

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
        var user = GetSampleUser();
        var dossier = GetSampleDossier();
        dossierRepository.Setup(d => d.GetDossierByDossierId(It.IsAny<Guid>())).ReturnsAsync(dossier);

        await dossierService.GetDossierForUserOrThrow(Guid.NewGuid(), Guid.NewGuid());

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    public async Task SaveCourseCreationRequest_ValidInput_Saves()
    {
        dossierRepository.Setup(d => d.SaveCourseCreationRequest(It.IsAny<CourseCreationRequest>())).ReturnsAsync(true);

        await dossierService.SaveCourseCreationRequest(GetSampleCourseCreationRequest());
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task SaveCourseCreationRequest_InvalidInput_Throws()
    {
        dossierRepository.Setup(d => d.SaveCourseCreationRequest(It.IsAny<CourseCreationRequest>())).ReturnsAsync(false);

        await dossierService.SaveCourseCreationRequest(GetSampleCourseCreationRequest());

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    public async Task SaveCourseModificationRequest_ValidInput_Saves()
    {
        dossierRepository.Setup(d => d.SaveCourseModificationRequest(It.IsAny<CourseModificationRequest>())).ReturnsAsync(true);

        await dossierService.SaveCourseModificationRequest(GetSampleCourseModificationRequest());
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public async Task SaveCourseModificationRequest_InvalidInput_Throws()
    {
        dossierRepository.Setup(d => d.SaveCourseModificationRequest(It.IsAny<CourseModificationRequest>())).ReturnsAsync(false);

        await dossierService.SaveCourseModificationRequest(GetSampleCourseModificationRequest());

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    public async Task GetCourseCreationRequest_ValidInput_ReturnsCourseCreationRequest()
    {
        var courseCreationRequest = GetSampleCourseCreationRequest();
        dossierRepository.Setup(d => d.GetCourseCreationRequest(It.IsAny<Guid>())).ReturnsAsync(courseCreationRequest);

        await dossierService.GetCourseCreationRequest(courseCreationRequest.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public async Task GetCourseCreationRequest_InvalidInput_Throws()
    {
        dossierRepository.Setup(d => d.GetCourseCreationRequest(It.IsAny<Guid>())).ReturnsAsync((CourseCreationRequest)null!);

        await dossierService.GetCourseCreationRequest(GetSampleCourseCreationRequest().Id);

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    [TestMethod]
    public async Task GetCourseModificationRequest_ValidInput_ReturnsCourseCreationRequest()
    {
        var courseModificationRequest = GetSampleCourseModificationRequest();
        dossierRepository.Setup(d => d.GetCourseModificationRequest(It.IsAny<Guid>())).ReturnsAsync(courseModificationRequest);

        await dossierService.GetCourseModificationRequest(courseModificationRequest.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public async Task GetCourseModificationRequest_InvalidInput_Throws()
    {
        dossierRepository.Setup(d => d.GetCourseModificationRequest(It.IsAny<Guid>())).ReturnsAsync((CourseModificationRequest)null!);

        await dossierService.GetCourseModificationRequest(GetSampleCourseModificationRequest().Id);

        logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
    }

    private static User GetSampleUser()
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

    private static CreateDossierDTO GetSampleCreateDossierDTO()
    {
        return new CreateDossierDTO
        {
            Title = "test title",
            Description = "test description"
        };
    }

    private static Dossier GetSampleDossier()
    {
        return new Dossier
        {
            InitiatorId = Guid.NewGuid(),
            Published = false,
            Title = "test title",
            Description = "test description"
        };
    }

    private static Dossier GetSampleDossier(User user)
    {
        var dossier = GetSampleDossier();
        dossier.InitiatorId = user.Id;

        return dossier;
    }

    private static EditDossierDTO GetSampleEditDossierDTO()
    {
        return new EditDossierDTO
        {
            InitiatorId = Guid.NewGuid(),
            Title = "test title",
            Description = "test description"
        };
    }

    private static CourseCreationRequest GetSampleCourseCreationRequest()
    {
        return new CourseCreationRequest()
        {
            DossierId = Guid.NewGuid(),
            Rationale = "Very reasonable",
            ResourceImplication = "Very expensive",
            Comment = "Lots of problems here",
            NewCourseId = Guid.NewGuid()
        };
    }

    private static CourseModificationRequest GetSampleCourseModificationRequest()
    {
        return new CourseModificationRequest()
        {
            DossierId = Guid.NewGuid(),
            Rationale = "Very reasonable",
            ResourceImplication = "Very expensive",
            Comment = "Lots of problems here",
            CourseId = Guid.NewGuid()
        };
    }
}

