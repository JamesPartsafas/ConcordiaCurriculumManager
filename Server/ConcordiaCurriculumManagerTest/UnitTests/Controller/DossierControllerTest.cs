using AutoMapper;
using ConcordiaCurriculumManager.Controllers;
using ConcordiaCurriculumManager.DTO.Dossiers;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;

namespace ConcordiaCurriculumManagerTest.UnitTests.Services
{
    [TestClass]
    public class DossierControllerTest
    {
        private Mock<IDossierService> dossierService = null!;
        private Mock<IUserAuthenticationService> userService = null!;
        private Mock<ILogger<DossierController>> logger = null!;
        private DossierController dossierController = null!;
        private Mock<IMapper> mapper = null!;


        [TestInitialize]
        public void TestInitialize()
        {
            logger = new Mock<ILogger<DossierController>>();
            dossierService = new Mock<IDossierService>();
            userService = new Mock<IUserAuthenticationService>();
            mapper = new Mock<IMapper>();

            dossierController = new DossierController(mapper.Object, logger.Object, userService.Object, dossierService.Object);
        }

        [TestMethod]
        public async Task GetDossiersByID_ValidCall_ReturnsData()
        {
            var dossier = GetSampleDossier();
            dossierService.Setup(d => d.GetDossierDetailsById(dossier.Id)).ReturnsAsync(dossier);

            var actionResult = await dossierController.GetDossierByDossierId(dossier.Id);
            var objectResult = (ObjectResult)actionResult;

            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            mapper.Verify(mock => mock.Map<DossierDetailsDTO>(dossier), Times.Once());
        }

        [TestMethod]
        public async Task GetDossiersByID_InvalidCall_Returns404()
        {
            dossierService.Setup(d => d.GetDossierDetailsById(It.IsAny<Guid>())).Throws(new ArgumentException());

            var actionResult = await dossierController.GetDossierByDossierId(Guid.NewGuid());
            var objectResult = (ObjectResult)actionResult;

            Assert.AreEqual((int)HttpStatusCode.NotFound, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task GetDossiersByID_ServerError_Returns500()
        {
            dossierService.Setup(d => d.GetDossierDetailsById(It.IsAny<Guid>())).Throws(new Exception());

            var actionResult = await dossierController.GetDossierByDossierId(Guid.NewGuid());
            var objectResult = (ObjectResult)actionResult;

            Assert.AreEqual((int)HttpStatusCode.InternalServerError, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task EditDossier_ValidCall_ReturnsData()
        {
            var user = GetSampleUser();
            var dossier = GetSampleDossier();
            var editDossier = GetSampleEditDossierDTO();

            userService.Setup(u => u.GetCurrentUser()).ReturnsAsync(user);
            dossierService.Setup(d => d.EditDossier(editDossier, user)).ReturnsAsync(dossier);

            var actionResult = await dossierController.EditDossier(editDossier);
            var objectResult = (ObjectResult)actionResult;

            Assert.AreEqual((int)HttpStatusCode.Created, objectResult.StatusCode);
            mapper.Verify(mock => mock.Map<DossierDTO>(dossier), Times.Once());
        }

        [TestMethod]
        public async Task EditDossier_InvalidCall_Returns400()
        {
            dossierService.Setup(d => d.EditDossier(It.IsAny<EditDossierDTO>(), It.IsAny<User>())).Throws(new ArgumentException());

            var actionResult = await dossierController.EditDossier(GetSampleEditDossierDTO());
            var objectResult = (ObjectResult)actionResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task EditDossier_ServerError_Returns500()
        {
            dossierService.Setup(d => d.EditDossier(It.IsAny<EditDossierDTO>(), It.IsAny<User>())).Throws(new Exception());

            var actionResult = await dossierController.EditDossier(GetSampleEditDossierDTO());
            var objectResult = (ObjectResult)actionResult;

            Assert.AreEqual((int)HttpStatusCode.InternalServerError, objectResult.StatusCode);
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

