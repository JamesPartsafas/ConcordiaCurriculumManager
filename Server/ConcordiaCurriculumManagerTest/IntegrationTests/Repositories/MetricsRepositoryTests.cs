using Microsoft.EntityFrameworkCore;
using ConcordiaCurriculumManager.Repositories;
using ConcordiaCurriculumManager.Models.Metrics;
using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using ConcordiaCurriculumManagerTest.UnitTests.UtilityFunctions;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;

namespace ConcordiaCurriculumManagerTest.IntegrationTests.Repositories;

[TestClass]
public class MetricsRepositoryTests
{
    private static CCMDbContext dbContext = null!;
    private IMetricsRepository metricRepository = null!;

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
        metricRepository = new MetricsRepository(dbContext);
    }

    [TestCleanup]
    public void TestCleanup()
    {
        dbContext.HttpMetrics.RemoveRange(dbContext.HttpMetrics);
        dbContext.DossierMetrics.RemoveRange(dbContext.DossierMetrics);
        dbContext.SaveChanges();
    }

    [TestMethod]
    public async Task SaveHttpMetricAsync_ShouldSaveMetricToDatabase()
    {
        var httpMetric = TestData.GetHttpMetric();

        await metricRepository.SaveHttpMetricAsync(httpMetric);

        var savedMetric = await dbContext.HttpMetrics.FirstOrDefaultAsync();
        Assert.IsNotNull(savedMetric);
    }

    [TestMethod]
    public async Task GetTopHttpStatusFromIndexAndDate_ShouldReturnTopHttpStatusCodesFromDatabase()
    {
        var startDate = DateOnly.FromDateTime(DateTime.UtcNow);
        const int startingIndex = 0;

        var firstMetric = TestData.GetHttpMetric();
        var secondMetric = TestData.GetHttpMetric();
        var thirdMetric = TestData.GetHttpMetric();

        firstMetric.ResponseStatusCode = 204;
        firstMetric.CreatedDate = DateTime.UtcNow;

        secondMetric.ResponseStatusCode = 204;
        secondMetric.CreatedDate = DateTime.UtcNow.AddDays(1);

        thirdMetric.ResponseStatusCode = 400;
        thirdMetric.CreatedDate = DateTime.UtcNow.AddDays(-1);

        dbContext.HttpMetrics.AddRange(new List<HttpMetric> { firstMetric, secondMetric, thirdMetric });
        await dbContext.SaveChangesAsync();

        var topHttpStatusCodes = await metricRepository.GetTopHttpStatusFromIndexAndDate(startingIndex, startDate);

        Assert.IsNotNull(topHttpStatusCodes);
        Assert.AreEqual(1, topHttpStatusCodes.Count);
        Assert.AreEqual(2, topHttpStatusCodes.First().Count);
    }

    [TestMethod]
    public async Task GetTopHitHttpEndpoints_ShouldReturnTopHitHttpEndpointsFromDatabase()
    {
        var startDate = DateOnly.FromDateTime(DateTime.UtcNow);
        const int startingIndex = 0;

        var firstMetric = TestData.GetHttpMetric();
        var secondMetric = TestData.GetHttpMetric();
        var thirdMetric = TestData.GetHttpMetric();

        firstMetric.Controller = "Controller";
        firstMetric.Endpoint = "Teapot";
        firstMetric.CreatedDate = DateTime.UtcNow;

        secondMetric.Controller = "Controller";
        secondMetric.Endpoint = "Teapot";
        secondMetric.CreatedDate = DateTime.UtcNow.AddDays(1);

        thirdMetric.Controller = "AnotherController";
        thirdMetric.Endpoint = "AnotherTeapot";
        thirdMetric.CreatedDate = DateTime.UtcNow.AddDays(-1);

        dbContext.HttpMetrics.AddRange(new List<HttpMetric> { firstMetric, secondMetric, thirdMetric });
        await dbContext.SaveChangesAsync();

        var topHitHttpEndpoints = await metricRepository.GetTopHitHttpEndpoints(startingIndex, startDate);

        Assert.IsNotNull(topHitHttpEndpoints);
        Assert.AreEqual(1, topHitHttpEndpoints.Count); 
        Assert.AreEqual("Teapot", topHitHttpEndpoints.First().Endpoint);
        Assert.AreEqual("Controller", topHitHttpEndpoints.First().Controller);
        Assert.AreEqual("Controller/Teapot", topHitHttpEndpoints.First().FullPath);
    }

    [TestMethod]
    public async Task SaveDossierMetricAsync_ShouldSaveMetricToDatabase()
    {
        var dossierId = TestData.GetDossierMetric();

        await metricRepository.SaveDossierMetricAsync(dossierId);

        var savedMetric = await dbContext.DossierMetrics.FirstOrDefaultAsync();
        Assert.IsNotNull(savedMetric);
    }

    [TestMethod]
    public async Task GetTopViewedDossierFromIndexAndDatee_ShouldReturnTopViewedDossiersFromDatabase()
    {
        var startDate = DateOnly.FromDateTime(DateTime.UtcNow);
        const int startingIndex = 0;

        var firstMetric = TestData.GetDossierMetric();
        var secondMetric = TestData.GetDossierMetric();
        var thirdMetric = TestData.GetDossierMetric();

        firstMetric.CreatedDate = DateTime.UtcNow;

        secondMetric.Dossier = firstMetric.Dossier;
        secondMetric.DossierId = firstMetric.DossierId;
        secondMetric.CreatedDate = DateTime.UtcNow.AddDays(1);

        thirdMetric.CreatedDate = DateTime.UtcNow.AddDays(-1);

        dbContext.Users.AddRange(new List<User> { firstMetric.User!, secondMetric.User!, thirdMetric.User! });
        dbContext.Dossiers.AddRange(new List<Dossier> { firstMetric.Dossier!, thirdMetric.Dossier! });
        dbContext.DossierMetrics.AddRange(new List<DossierMetric> { firstMetric, secondMetric, thirdMetric });
        await dbContext.SaveChangesAsync();

        var topViewedDossierCodes = await metricRepository.GetTopViewedDossierFromIndexAndDate(startingIndex, startDate);

        Assert.IsNotNull(topViewedDossierCodes);
        Assert.AreEqual(1, topViewedDossierCodes.Count);
        Assert.AreEqual(2, topViewedDossierCodes.First().Count);
    }

    [TestMethod]
    public async Task GetTopDossierViewingUser_ShouldReturnTopDossierViewingUserFromDatabase()
    {
        var startDate = DateOnly.FromDateTime(DateTime.UtcNow);
        const int startingIndex = 0;

        var firstMetric = TestData.GetDossierMetric();
        var secondMetric = TestData.GetDossierMetric();
        var thirdMetric = TestData.GetDossierMetric();

        firstMetric.CreatedDate = DateTime.UtcNow;

        secondMetric.User = firstMetric.User;
        secondMetric.UserId = firstMetric.UserId;
        secondMetric.CreatedDate = DateTime.UtcNow.AddDays(1);

        thirdMetric.CreatedDate = DateTime.UtcNow.AddDays(-1);

        dbContext.Users.AddRange(new List<User> { firstMetric.User!, thirdMetric.User! });
        dbContext.Dossiers.AddRange(new List<Dossier> { firstMetric.Dossier!, secondMetric.Dossier!, thirdMetric.Dossier! });
        dbContext.DossierMetrics.AddRange(new List<DossierMetric> { firstMetric, secondMetric, thirdMetric });
        await dbContext.SaveChangesAsync();

        var topViewedDossierCodes = await metricRepository.GetTopDossierViewingUser(startingIndex, startDate);

        Assert.IsNotNull(topViewedDossierCodes);
        Assert.AreEqual(1, topViewedDossierCodes.Count);
        Assert.AreEqual(2, topViewedDossierCodes.First().Count);
    }
}

