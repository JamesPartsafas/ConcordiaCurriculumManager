using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories;
using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using ConcordiaCurriculumManagerTest.UnitTests.UtilityFunctions;
using Microsoft.EntityFrameworkCore;

namespace ConcordiaCurriculumManagerTest.IntegrationTests.Repositories;

[TestClass]
public class DossierRepositoryTests
{
    private static CCMDbContext dbContext = null!;
    private IDossierRepository dossierRepository = null!;

    [ClassInitialize]
    public static void ClassInitialize(TestContext _)
    {
        var options = new DbContextOptionsBuilder<CCMDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        dbContext = new CCMDbContext(options);
    }

    [ClassCleanup]
    public static void ClassCleanup() => dbContext.Dispose();

    [TestInitialize]
    public void TestInitialize()
    {
        dossierRepository = new DossierRepository(dbContext);
    }

    [TestMethod]
    public async Task GetDossierById_ValidId_ReturnsListOfDossiers()
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "fname",
            LastName = "lname",
            Email = "test@example.com",
            Password = "password"
        };

        var dossier = new Dossier
        {
            Id = Guid.NewGuid(),
            Initiator = user,
            InitiatorId = Guid.NewGuid(),
            Title = "test title",
            Description = "test description",
            State = DossierStateEnum.Created,
            Discussion = null!,
        };

        dbContext.Users.Add(user);
        dbContext.Dossiers.Add(dossier);
        await dbContext.SaveChangesAsync();

        var result = await dossierRepository.GetDossiersByID(user.Id);
        Assert.IsNotNull(result);
        Assert.AreEqual(result.Count(), 1);
        Assert.AreEqual(user.Id, dossier.InitiatorId);
    }

    [TestMethod]
    public async Task GetDossierByDossierId_ValidId_ReturnsDossier()
    {
        var dossier = TestData.GetSampleDossier();

        dbContext.Dossiers.Add(dossier);
        await dbContext.SaveChangesAsync();

        var result = await dossierRepository.GetDossierByDossierId(dossier.Id);
        Assert.IsNotNull(result);
        Assert.AreEqual(result.InitiatorId, dossier.InitiatorId);
        Assert.AreEqual(result.Title, dossier.Title);
    }

    [TestMethod]
    public async Task SaveDossier_ReturnsTrue() {
        var dossier = TestData.GetSampleDossier();

        var result = await dossierRepository.SaveDossier(dossier);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task SaveCourseCreationRequest_ReturnsTrue()
    {
        var courseCreationRequest = new CourseCreationRequest
        {
            NewCourseId = Guid.NewGuid(),
            DossierId = Guid.NewGuid(),
            Rationale = "It's important",
            ResourceImplication = "Very expensive",
            Comment = "Not complex"
        };

        var result = await dossierRepository.SaveCourseCreationRequest(courseCreationRequest);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task SaveCourseModificationRequest_ReturnsTrue()
    {
        var courseModificationRequest = new CourseModificationRequest
        {
            CourseId = Guid.NewGuid(),
            DossierId = Guid.NewGuid(),
            Rationale = "It's important",
            ResourceImplication = "Very expensive",
            Comment = "Not complex"
        };

        var result = await dossierRepository.SaveCourseModificationRequest(courseModificationRequest);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task UpdateDossier_ReturnsTrue() {
        var dossier = TestData.GetSampleDossier();
        await dossierRepository.SaveDossier(dossier);

        var newTitle = "test title modified";
        var newDescription = "test description modified";

        dossier.Title = newTitle;
        dossier.Description = newDescription;
        var result = await dossierRepository.UpdateDossier(dossier);

        Assert.AreEqual(dossier.Title, newTitle);
        Assert.AreEqual(dossier.Description, newDescription);
        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task DeleteDossier_ReturnsTrue()
    {
        var dossier = TestData.GetSampleDossier();

        dbContext.Dossiers.Add(dossier);
        await dbContext.SaveChangesAsync();

        var result = await dossierRepository.DeleteDossier(dossier);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task GetCourseCreationRequest_ReturnsCourseCreationRequest()
    {
        var courseCreationRequest = TestData.GetSampleCourseCreationRequest();

        dbContext.CourseCreationRequests.Add(courseCreationRequest);
        await dbContext.SaveChangesAsync();

        var result = await dossierRepository.GetCourseCreationRequest(courseCreationRequest.Id);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task UpdateCourseCreationRequest_ReturnsTrue()
    {
        var courseCreationRequest = TestData.GetSampleCourseCreationRequest();

        dbContext.CourseCreationRequests.Add(courseCreationRequest);
        await dbContext.SaveChangesAsync();

        var newRationale = "It's necessary modified";
        var newResourceImplication = "New prof needed modified";

        courseCreationRequest.Rationale = newRationale;
        courseCreationRequest.ResourceImplication = newResourceImplication;

        var result = await dossierRepository.UpdateCourseCreationRequest(courseCreationRequest);

        Assert.AreEqual(courseCreationRequest.Rationale, newRationale);
        Assert.AreEqual(courseCreationRequest.ResourceImplication, newResourceImplication);
        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task GetCourseModificationRequest_ReturnsCourseModificationRequest()
    {
        var courseModificationRequest = TestData.GetSampleCourseModificationRequest();

        dbContext.CourseModificationRequests.Add(courseModificationRequest);
        await dbContext.SaveChangesAsync();

        var result = await dossierRepository.GetCourseModificationRequest(courseModificationRequest.Id);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task UpdateCourseModificationRequest_ReturnsTrue()
    {
        var courseModificationRequest = TestData.GetSampleCourseModificationRequest();

        dbContext.CourseModificationRequests.Add(courseModificationRequest);
        await dbContext.SaveChangesAsync();

        var newRationale = "It's necessary modified";
        var newResourceImplication = "New prof needed modified";

        courseModificationRequest.Rationale = newRationale;
        courseModificationRequest.ResourceImplication = newResourceImplication;

        var result = await dossierRepository.UpdateCourseModificationRequest(courseModificationRequest);

        Assert.AreEqual(courseModificationRequest.Rationale, newRationale);
        Assert.AreEqual(courseModificationRequest.ResourceImplication, newResourceImplication);
        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task GetCourseDeletionRequest_ReturnsCourseDeletionRequest()
    {
        var courseDeletionRequest = TestData.GetSampleCourseDeletionRequest();

        dbContext.CourseDeletionRequests.Add(courseDeletionRequest);
        await dbContext.SaveChangesAsync();

        var result = await dossierRepository.GetCourseDeletionRequest(courseDeletionRequest.Id);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task UpdateCourseDeletionRequest_ReturnsTrue()
    {
        var courseDeletionRequest = TestData.GetSampleCourseDeletionRequest();

        dbContext.CourseDeletionRequests.Add(courseDeletionRequest);
        await dbContext.SaveChangesAsync();

        var newRationale = "It's necessary modified";
        var newResourceImplication = "New prof needed modified";

        courseDeletionRequest.Rationale = newRationale;
        courseDeletionRequest.ResourceImplication = newResourceImplication;

        var result = await dossierRepository.UpdateCourseDeletionRequest(courseDeletionRequest);

        Assert.AreEqual(courseDeletionRequest.Rationale, newRationale);
        Assert.AreEqual(courseDeletionRequest.ResourceImplication, newResourceImplication);
        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task DeleteCourseCreationRequest_ReturnsTrue()
    {
        var courseCreationRequest = TestData.GetSampleCourseCreationRequest();
        var course = TestData.GetSampleCourse();

        dbContext.CourseCreationRequests.Add(courseCreationRequest);
        dbContext.Courses.Add(course);
        await dbContext.SaveChangesAsync();

        var result = await dossierRepository.DeleteCourseCreationRequest(courseCreationRequest);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task DeleteCourseModificationRequest_ReturnsTrue()
    {
        var courseModificationRequest = TestData.GetSampleCourseModificationRequest();
        var course = TestData.GetSampleCourse();

        dbContext.CourseModificationRequests.Add(courseModificationRequest);
        dbContext.Courses.Add(course);
        await dbContext.SaveChangesAsync();

        var result = await dossierRepository.DeleteCourseModificationRequest(courseModificationRequest);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task DeleteCourseDeletionRequest_ReturnsTrue()
    {
        var courseDeletionRequest = TestData.GetSampleCourseDeletionRequest();
        var course = TestData.GetSampleCourse();

        dbContext.CourseDeletionRequests.Add(courseDeletionRequest);
        dbContext.Courses.Add(course);
        await dbContext.SaveChangesAsync();

        var result = await dossierRepository.DeleteCourseDeletionRequest(courseDeletionRequest);

        Assert.IsTrue(result);
    }
}

