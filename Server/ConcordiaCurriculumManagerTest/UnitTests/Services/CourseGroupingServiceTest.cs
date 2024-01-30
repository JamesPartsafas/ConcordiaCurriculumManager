using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManager.Middleware.Exceptions;
using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Curriculum.CourseGroupings;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Repositories;
using ConcordiaCurriculumManager.Services;
using ConcordiaCurriculumManagerTest.UnitTests.UtilityFunctions;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordiaCurriculumManagerTest.UnitTests.Services;

[TestClass]
public class CourseGroupingServiceTest
{
    private Mock<ILogger<CourseGroupingService>> logger = null!;
    private Mock<ICourseRepository> courseRepository = null!;
    private Mock<ICourseGroupingRepository> courseGroupingRepository = null!;
    private Mock<IDossierService> dossierService = null!;

    private CourseGroupingService courseGroupingService = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        logger = new Mock<ILogger<CourseGroupingService>>();
        courseRepository = new Mock<ICourseRepository>();
        courseGroupingRepository = new Mock<ICourseGroupingRepository>();
        dossierService = new Mock<IDossierService>();

        courseGroupingService = new CourseGroupingService(
            logger.Object,
            courseRepository.Object,
            courseGroupingRepository.Object,
            dossierService.Object
        );
    }

    [TestMethod]
    public async Task GetCourseGrouping_WithMultipleLevels_QueriesRecursively()
    {
        var grouping = TestData.GetSampleCourseGrouping();
        var subgrouping = grouping.SubGroupings.First();
        var course = grouping.Courses.First();
        grouping.SubGroupings = new List<CourseGrouping>();
        grouping.Courses = new List<Course>();
        subgrouping.Courses = new List<Course>();

        courseGroupingRepository.Setup(cgr => cgr.GetCourseGroupingById(grouping.Id)).ReturnsAsync(grouping);
        courseGroupingRepository.Setup(cgr => cgr.GetCourseGroupingByCommonIdentifier(subgrouping.CommonIdentifier)).ReturnsAsync(subgrouping);
        courseRepository.Setup(cr => cr.GetCoursesByConcordiaCourseIds(It.IsAny<IList<int>>())).ReturnsAsync(new List<Course> { { course } } );

        var output = await courseGroupingService.GetCourseGrouping(grouping.Id);

        Assert.AreEqual(course.Id, grouping.Courses.First().Id);
        Assert.AreEqual(course.Id, subgrouping.Courses.First().Id);
        Assert.AreEqual(subgrouping.CommonIdentifier, grouping.SubGroupings.First().CommonIdentifier);
        Assert.AreEqual(subgrouping.CommonIdentifier, grouping.SubGroupingReferences.First().ChildGroupCommonIdentifier);

        courseGroupingRepository.Verify(mock => mock.GetCourseGroupingById(It.IsAny<Guid>()), Times.Once());
        courseGroupingRepository.Verify(mock => mock.GetCourseGroupingByCommonIdentifier(It.IsAny<Guid>()), Times.Once());
        courseRepository.Verify(mock => mock.GetCoursesByConcordiaCourseIds(It.IsAny<List<int>>()), Times.Exactly(2));
    }

    [TestMethod]
    public async Task GetCourseGrouping_WithLowestLevel_QueriesOnlyOnce()
    {
        var grouping = TestData.GetSampleCourseGrouping().SubGroupings.First();
        var course = grouping.Courses.First();
        grouping.Courses = new List<Course>();

        courseGroupingRepository.Setup(cgr => cgr.GetCourseGroupingById(grouping.Id)).ReturnsAsync(grouping);
        courseRepository.Setup(cr => cr.GetCoursesByConcordiaCourseIds(It.IsAny<IList<int>>())).ReturnsAsync(new List<Course> { { course } });

        var output = await courseGroupingService.GetCourseGrouping(grouping.Id);

        Assert.AreEqual(course.Id, grouping.Courses.First().Id);
        Assert.AreEqual(0, grouping.SubGroupings.Count);

        courseGroupingRepository.Verify(mock => mock.GetCourseGroupingById(It.IsAny<Guid>()), Times.Once());
        courseGroupingRepository.Verify(mock => mock.GetCourseGroupingByCommonIdentifier(It.IsAny<Guid>()), Times.Never());
        courseRepository.Verify(mock => mock.GetCoursesByConcordiaCourseIds(It.IsAny<List<int>>()), Times.Once());
    }

    [TestMethod]
    public async Task GetCourseGroupingBySchool_WithValidSchool_QueriesOnlyOnce()
    {
        var grouping = TestData.GetSampleCourseGrouping();
        var groupings = new List<CourseGrouping> { { grouping } };

        courseGroupingRepository.Setup(cgr => cgr.GetCourseGroupingsBySchool(grouping.School)).ReturnsAsync(groupings);

        var output = await courseGroupingService.GetCourseGroupingsBySchoolNonRecursive(grouping.School);

        courseGroupingRepository.Verify(mock => mock.GetCourseGroupingsBySchool(It.IsAny<SchoolEnum>()), Times.Once());
    }

    [TestMethod]
    public async Task GetCourseGroupingLikeName_WithValidName_QueriesOnlyOnce()
    {
        var grouping = TestData.GetSampleCourseGrouping();
        var groupings = new List<CourseGrouping> { { grouping } };

        courseGroupingRepository.Setup(cgr => cgr.GetCourseGroupingsLikeName(grouping.Name)).ReturnsAsync(groupings);

        var output = await courseGroupingService.GetCourseGroupingsLikeName(grouping.Name);

        courseGroupingRepository.Verify(mock => mock.GetCourseGroupingsLikeName(It.IsAny<string>()), Times.Once());
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidInputException))]
    public async Task GetCourseGroupingLikeName_WithEmptyName_ThrowsInvalidInputException()
    {
        var output = await courseGroupingService.GetCourseGroupingsLikeName(" ");
    }

    [TestMethod]
    [ExpectedException(typeof(BadRequestException))]
    public async Task InitiateCourseGroupingModification_WithNonexistentCourse_Throws()
    {
        var dto = TestData.GetSampleCourseGroupingModificationRequestDTO();

        courseGroupingRepository.Setup(cgr => cgr.GetCourseGroupingByCommonIdentifier(dto.CourseGrouping.CommonIdentifier)).ReturnsAsync((CourseGrouping)null!);

        await courseGroupingService.InitiateCourseGroupingModification(dto);
    }

    [TestMethod]
    [ExpectedException(typeof(ServiceUnavailableException))]
    public async Task InitiateCourseGroupingModification_FailsToSave_Throws()
    {
        var dto = TestData.GetSampleCourseGroupingModificationRequestDTO();

        courseGroupingRepository.Setup(cgr => cgr.GetCourseGroupingByCommonIdentifier(dto.CourseGrouping.CommonIdentifier))
            .ReturnsAsync(TestData.GetSampleCourseGrouping());
        dossierService.Setup(ds => ds.GetDossierDetailsByIdOrThrow(dto.DossierId)).ReturnsAsync(TestData.GetSampleDossier());
        courseGroupingRepository.Setup(cgr => cgr.SaveCourseGroupingRequest(It.IsAny<CourseGroupingRequest>())).ReturnsAsync(false);

        await courseGroupingService.InitiateCourseGroupingModification(dto);
    }

    [TestMethod]
    [ExpectedException(typeof(BadRequestException))]
    public async Task InitiateCourseGroupingModification_WithDuplicateRequest_Throws()
    {
        var dto = TestData.GetSampleCourseGroupingModificationRequestDTO();
        var dossier = TestData.GetSampleDossier();
        var request = TestData.GetSampleCourseGroupingRequest();
        request.CourseGrouping!.CommonIdentifier = dto.CourseGrouping.CommonIdentifier;

        dossier.CourseGroupingRequests.Add(request);

        courseGroupingRepository.Setup(cgr => cgr.GetCourseGroupingByCommonIdentifier(dto.CourseGrouping.CommonIdentifier))
            .ReturnsAsync(TestData.GetSampleCourseGrouping());
        dossierService.Setup(ds => ds.GetDossierDetailsByIdOrThrow(dto.DossierId)).ReturnsAsync(dossier);

        await courseGroupingService.InitiateCourseGroupingModification(dto);
    }

    [TestMethod]
    public async Task InitiateCourseGroupingModification_WithValidData_Succeeds()
    {
        var dto = TestData.GetSampleCourseGroupingModificationRequestDTO();
        var dossier = TestData.GetSampleDossier();

        var requestsInDossier = dossier.CourseGroupingRequests.Count();

        courseGroupingRepository.Setup(cgr => cgr.GetCourseGroupingByCommonIdentifier(dto.CourseGrouping.CommonIdentifier))
            .ReturnsAsync(TestData.GetSampleCourseGrouping());
        dossierService.Setup(ds => ds.GetDossierDetailsByIdOrThrow(dto.DossierId)).ReturnsAsync(dossier);
        courseGroupingRepository.Setup(cgr => cgr.SaveCourseGroupingRequest(It.IsAny<CourseGroupingRequest>())).ReturnsAsync(true);

        var request = await courseGroupingService.InitiateCourseGroupingModification(dto);

        Assert.AreEqual(requestsInDossier + 1, dossier.CourseGroupingRequests.Count());
        Assert.AreEqual(RequestType.ModificationRequest, request.RequestType);
        Assert.AreEqual(CourseGroupingStateEnum.CourseGroupingChangeProposal, request.CourseGrouping!.State);
    }

    [TestMethod]
    [ExpectedException(typeof(ServiceUnavailableException))]
    public async Task EditCourseGroupingModification_FailsToQuery_Throws()
    {
        var dto = TestData.GetSampleCourseGroupingModificationRequestDTO();
        var originalRequestId = Guid.NewGuid();

        courseGroupingRepository.Setup(cgr => cgr.GetCourseGroupingRequestById(originalRequestId)).ReturnsAsync((CourseGroupingRequest)null!);

        await courseGroupingService.EditCourseGroupingModification(originalRequestId, dto);
    }

    [TestMethod]
    [ExpectedException(typeof(ServiceUnavailableException))]
    public async Task EditCourseGroupingModification_FailsToQueryIncludedGrouping_Throws()
    {
        var dto = TestData.GetSampleCourseGroupingModificationRequestDTO();
        var request = TestData.GetSampleCourseGroupingRequest();
        request.CourseGrouping = null;

        courseGroupingRepository.Setup(cgr => cgr.GetCourseGroupingRequestById(request.Id)).ReturnsAsync(request);

        await courseGroupingService.EditCourseGroupingModification(request.Id, dto);
    }

    [TestMethod]
    [ExpectedException(typeof(BadRequestException))]
    public async Task EditCourseGroupingModification_WithMismatchedCommonIdentifier_Throws()
    {
        var dto = TestData.GetSampleCourseGroupingModificationRequestDTO();
        var request = TestData.GetSampleCourseGroupingRequest();
        request.DossierId = dto.DossierId;
        dto.CourseGrouping.CommonIdentifier = Guid.NewGuid();
        request.CourseGrouping!.CommonIdentifier = Guid.NewGuid();

        courseGroupingRepository.Setup(cgr => cgr.GetCourseGroupingRequestById(request.Id)).ReturnsAsync(request);

        await courseGroupingService.EditCourseGroupingModification(request.Id, dto);
    }

    [TestMethod]
    [ExpectedException(typeof(BadRequestException))]
    public async Task EditCourseGroupingModification_WithMismatchedDossierId_Throws()
    {
        var dto = TestData.GetSampleCourseGroupingModificationRequestDTO();
        var request = TestData.GetSampleCourseGroupingRequest();
        request.DossierId = Guid.NewGuid();
        dto.DossierId = Guid.NewGuid();
        dto.CourseGrouping.CommonIdentifier = request.CourseGrouping!.CommonIdentifier;

        courseGroupingRepository.Setup(cgr => cgr.GetCourseGroupingRequestById(request.Id)).ReturnsAsync(request);

        await courseGroupingService.EditCourseGroupingModification(request.Id, dto);
    }

    [TestMethod]
    [ExpectedException(typeof(BadRequestException))]
    public async Task DeleteCourseGroupingRequest_RequestDoesNotExist_Throws()
    {
        var dossier = TestData.GetSampleDossier();
        dossier.CourseGroupingRequests = new List<CourseGroupingRequest>();

        dossierService.Setup(ds => ds.GetDossierDetailsByIdOrThrow(It.IsAny<Guid>())).ReturnsAsync(dossier);

        await courseGroupingService.DeleteCourseGroupingRequest(Guid.NewGuid(), Guid.NewGuid());
    }

    [TestMethod]
    [ExpectedException(typeof(ServiceUnavailableException))]
    public async Task DeleteCourseGroupingRequest_FailsToDelete_Throws()
    {
        var dossier = TestData.GetSampleDossier();
        var request = TestData.GetSampleCourseGroupingRequest();
        dossier.CourseGroupingRequests.Add(request);

        dossierService.Setup(ds => ds.GetDossierDetailsByIdOrThrow(dossier.Id)).ReturnsAsync(dossier);
        courseGroupingRepository.Setup(cgr => cgr.DeleteCourseGroupingRequest(request)).ReturnsAsync(false);

        await courseGroupingService.DeleteCourseGroupingRequest(dossier.Id, request.Id);
    }

    [TestMethod]
    public async Task DeleteCourseGroupingRequest_WithRequest_Succeeds()
    {
        var dossier = TestData.GetSampleDossier();
        var request = TestData.GetSampleCourseGroupingRequest();
        dossier.CourseGroupingRequests.Add(request);

        dossierService.Setup(ds => ds.GetDossierDetailsByIdOrThrow(dossier.Id)).ReturnsAsync(dossier);
        courseGroupingRepository.Setup(cgr => cgr.DeleteCourseGroupingRequest(request)).ReturnsAsync(true);

        await courseGroupingService.DeleteCourseGroupingRequest(dossier.Id, request.Id);

        courseGroupingRepository.Verify(mock => mock.DeleteCourseGroupingRequest(request), Times.Once());
    }
}
