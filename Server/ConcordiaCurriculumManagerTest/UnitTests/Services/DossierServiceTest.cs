using Castle.Core.Logging;
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
    }
}

