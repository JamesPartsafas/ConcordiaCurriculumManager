using AutoMapper;
using ConcordiaCurriculumManager.Controllers;
using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.InputDTOs;
using ConcordiaCurriculumManager.Repositories;
using ConcordiaCurriculumManager.Services;
using ConcordiaCurriculumManagerTest.UnitTests.UtilityFunctions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConcordiaCurriculumManagerTest.UnitTests.Controller;

[TestClass]
public class DossierReviewControllerTest
{
    private Mock<IDossierReviewService> dossierReviewService = null!;
    private DossierReviewController dossierReviewController = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        dossierReviewService = new Mock<IDossierReviewService>();

        dossierReviewController = new DossierReviewController(dossierReviewService.Object);
    }

    [TestMethod]
    public async Task RejectDossier_ValidCall_ReturnsOk()
    {
        dossierReviewService.Setup(drs => drs.RejectDossier(It.IsAny<Guid>())).Returns(Task.CompletedTask);
        var dossierId = Guid.NewGuid();

        var actionResult = await dossierReviewController.RejectDossier(dossierId);

        dossierReviewService.Verify(mock => mock.RejectDossier(dossierId), Times.Once());
    }

    [TestMethod]
    public async Task ForwardDossier_ValidCall_ReturnsOk()
    {
        dossierReviewService.Setup(drs => drs.ForwardDossier(It.IsAny<Guid>())).Returns(Task.CompletedTask);
        var dossierId = Guid.NewGuid();

        var actionResult = await dossierReviewController.ForwardDossier(dossierId);

        dossierReviewService.Verify(mock => mock.ForwardDossier(dossierId), Times.Once());
    }
}
