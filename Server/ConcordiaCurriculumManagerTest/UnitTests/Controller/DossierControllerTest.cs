using AutoMapper;
using ConcordiaCurriculumManager.Controllers;
using ConcordiaCurriculumManager.DTO.Dossiers;
using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Services;
using ConcordiaCurriculumManagerTest.UnitTests.UtilityFunctions;
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
            var dossier = TestData.GetSampleDossier();
            dossierService.Setup(d => d.GetDossierDetailsById(dossier.Id)).ReturnsAsync(dossier);

            var actionResult = await dossierController.GetDossierByDossierId(dossier.Id);
            var objectResult = (ObjectResult)actionResult;

            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            mapper.Verify(mock => mock.Map<DossierDetailsDTO>(dossier), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task GetDossiersByID_InvalidCall_Returns404()
        {
            dossierService.Setup(d => d.GetDossierDetailsById(It.IsAny<Guid>())).Throws(new NotFoundException());

            await dossierController.GetDossierByDossierId(Guid.NewGuid());
        }

        [TestMethod]
        public async Task EditDossier_ValidCall_ReturnsData()
        {
            var user = TestData.GetSampleUser();
            var dossier = TestData.GetSampleDossier();
            var editDossier = TestData.GetSampleEditDossierDTO();

            userService.Setup(u => u.GetCurrentUser()).ReturnsAsync(user);
            dossierService.Setup(d => d.EditDossier(editDossier, dossier.Id)).ReturnsAsync(dossier);

            var actionResult = await dossierController.EditDossier(dossier.Id, editDossier);
            var objectResult = (ObjectResult)actionResult;

            Assert.AreEqual((int)HttpStatusCode.Created, objectResult.StatusCode);
            mapper.Verify(mock => mock.Map<DossierDTO>(dossier), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task EditDossier_InvalidCall_ThrowsException()
        {
            dossierService.Setup(d => d.EditDossier(It.IsAny<EditDossierDTO>(), It.IsAny<Guid>())).Throws(new Exception());

            await dossierController.EditDossier(Guid.NewGuid(), TestData.GetSampleEditDossierDTO());
        }

        [TestMethod]
        public async Task GetDossiersByID_ValidCall_ReturnsDatatest()
        {
            var dossier = TestData.GetSampleDossier();
            dossierService.Setup(d => d.GetDossierDetailsById(dossier.Id)).ReturnsAsync(dossier);

            var actionResult = await dossierController.GetDossierByDossierId(dossier.Id);
            var objectResult = (ObjectResult)actionResult;

            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
            mapper.Verify(mock => mock.Map<DossierDetailsDTO>(dossier), Times.Once());
        }

        [TestMethod]
        public async Task DeleteDossier_ValidCall_Returns204()
        {
            var user = TestData.GetSampleUser();
            var deleteDossier = TestData.GetSampleDeleteDossierDTO();

            userService.Setup(u => u.GetCurrentUser()).ReturnsAsync(user);

            var actionResult = await dossierController.DeleteDossier(deleteDossier);
            var objectResult = (NoContentResult)actionResult;

            Assert.AreEqual((int)HttpStatusCode.NoContent, objectResult.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task DeleteDossier_InvalidCall_ThrowsException()
        {
            dossierService.Setup(d => d.DeleteDossier(It.IsAny<Guid>())).Throws(new Exception());

            var actionResult = await dossierController.DeleteDossier(TestData.GetSampleDeleteDossierDTO());
            var objectResult = (ObjectResult)actionResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, objectResult.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task DeleteDossier_ServerError_ThrowsException()
        {
            dossierService.Setup(d => d.DeleteDossier(It.IsAny<Guid>())).Throws(new Exception());

            var actionResult = await dossierController.DeleteDossier(TestData.GetSampleDeleteDossierDTO());
            var objectResult = (ObjectResult)actionResult;

            Assert.AreEqual((int)HttpStatusCode.InternalServerError, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task GetDossierReportByDossierId_ValidCall_ReturnsDossierReport()
        {
            var dossierReport = TestData.GetSampleDossierReport();
            dossierService.Setup(d => d.GetDossierReportByDossierId(It.IsAny<Guid>())).ReturnsAsync(dossierReport);

            var actionResult = await dossierController.GetDossierReportByDossierId(It.IsAny<Guid>());
            var objectResult = (ObjectResult)actionResult;

            Assert.AreEqual((int)HttpStatusCode.Created, objectResult.StatusCode);
            mapper.Verify(mock => mock.Map<DossierReportDTO>(dossierReport), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task GetDossierReportByDossierId_InvalidCall_Returns404()
        {
            dossierService.Setup(d => d.GetDossierReportByDossierId(It.IsAny<Guid>())).Throws(new NotFoundException());

            await dossierController.GetDossierReportByDossierId(Guid.NewGuid());
        }
    }
}

