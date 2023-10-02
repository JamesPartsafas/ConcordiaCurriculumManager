using Castle.Core.Logging;
using ConcordiaCurriculumManager.DTO.Dossiers;
using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Curriculum.Dossier;
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

            await dossierService.CreateDossierForUser(GetSampleCreateDossierDIO(), GetSampleUser());

            logger.Verify(logger => logger.LogWarning(It.IsAny<string>()));
        }

        [TestMethod]
        public async Task CreateDossierForUser_ValidInput_Succeeds()
        {
            dossierRepository.Setup(d => d.SaveDossier(It.IsAny<Dossier>())).ReturnsAsync(true);
            var user = GetSampleUser();

            var dossier = await dossierService.CreateDossierForUser(GetSampleCreateDossierDIO(), user);

            Assert.AreEqual(user.Id, dossier.InitiatorId);
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

        private CreateDossierDTO GetSampleCreateDossierDIO()
        {
            return new CreateDossierDTO
            {
                Title = "test title",
                Description = "test description"
            };
        }
    }
}

