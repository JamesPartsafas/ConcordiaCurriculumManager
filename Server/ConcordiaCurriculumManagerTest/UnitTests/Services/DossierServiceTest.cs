using Castle.Core.Logging;
using ConcordiaCurriculumManager.DTO.Dossiers;
using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories;
using ConcordiaCurriculumManager.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using Moq;
using System;
namespace ConcordiaCurriculumManagerTest.UnitTests.Services
{
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

            Assert.AreEqual(dossier.Id, dossierDetails.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task RetrieveDossierDetails_InvalidInput_ThrowsArgumentException()
        {
            dossierRepository.Setup(d => d.GetDossierByDossierId(It.IsAny<Guid>())).ReturnsAsync((Dossier)null!);

            await dossierService.GetDossierDetailsById(Guid.NewGuid());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task EditDossier_InvalidInput_ThrowsArgumentException()
        {
            dossierRepository.Setup(d => d.GetDossierByDossierId(It.IsAny<Guid>())).ReturnsAsync((Dossier)null!);

            await dossierService.EditDossier(GetSampleEditDossierDTO(), GetSampleUser());

        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task EditDossier_NotTheDossierOfTheUser_ThrowsArgumentException()
        {
            var user = GetSampleUser();
            var dossier = GetSampleDossier();
            dossierRepository.Setup(d => d.GetDossierByDossierId(It.IsAny<Guid>())).ReturnsAsync(dossier);
            await dossierService.EditDossier(GetSampleEditDossierDTO(), GetSampleUser());
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
                Id = Guid.NewGuid(),
                InitiatorId = user.Id,
                Title = "test title modified",
                Description = "test description modified"
            };


            dossierRepository.Setup(d => d.GetDossierByDossierId(It.IsAny<Guid>())).ReturnsAsync(dossier);
            dossierRepository.Setup(d => d.UpdateDossier(It.IsAny<Dossier>())).ReturnsAsync(false);

            await dossierService.EditDossier(GetSampleEditDossierDTO(), GetSampleUser());

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
                Id = Guid.NewGuid(),
                InitiatorId = user.Id,
                Title = "test title modified",
                Description = "test description modified"
            };
           

            dossierRepository.Setup(d => d.GetDossierByDossierId(It.IsAny<Guid>())).ReturnsAsync(dossier);
            dossierRepository.Setup(d => d.UpdateDossier(It.IsAny<Dossier>())).ReturnsAsync(true);

            var editedDossier = await dossierService.EditDossier(editDossier, user);

            Assert.AreEqual(editDossier.Title, editedDossier.Title);
            Assert.AreEqual(editDossier.Description, editedDossier.Description);

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
                Id = Guid.NewGuid(),
                InitiatorId = Guid.NewGuid(),
                Title = "test title",
                Description = "test description"
            };
        }
    }
}

