using ConcordiaCurriculumManager.DTO.Dossiers;
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

    private CreateDossierDTO GetSampleCreateDossierDTO()
    {
        return new CreateDossierDTO
        {
            Title = "test title",
            Description = "test description"
        };
    }

    private Dossier GetSampleDossier()
    {
        return new Dossier
        {
            InitiatorId = Guid.NewGuid(),
            Published = false,
            Title = "test title",
            Description = "test description"
        };
    }

    private EditDossierDTO GetSampleEditDossierDTO()
    {
        return new EditDossierDTO
        {
            InitiatorId = Guid.NewGuid(),
            Title = "test title",
            Description = "test description"
        };
    }
}

