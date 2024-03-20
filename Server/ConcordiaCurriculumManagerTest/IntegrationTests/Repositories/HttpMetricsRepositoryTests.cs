using Microsoft.EntityFrameworkCore;
using ConcordiaCurriculumManager.Repositories;
using ConcordiaCurriculumManager.Models.Metrics;
using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using ConcordiaCurriculumManagerTest.UnitTests.UtilityFunctions;

namespace ConcordiaCurriculumManagerTest.IntegrationTests.Repositories;

[TestClass]
public class HttpMetricsRepositoryTests
{
    private static CCMDbContext dbContext = null!;
    private IHttpMetricsRepository httpMetricRepository = null!;

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
        httpMetricRepository = new HttpMetricsRepository(dbContext);
    }

    [TestCleanup]
    public void TestCleanup()
    {
        dbContext.HttpMetrics.RemoveRange(dbContext.HttpMetrics);
        dbContext.SaveChanges();
    }

    [TestMethod]
    public async Task SaveMetricAsync_ShouldSaveMetricToDatabase()
    {
        var httpMetric = TestData.GetHttpMetric();

        await httpMetricRepository.SaveMetricAsync(httpMetric);

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

        var topHttpStatusCodes = await httpMetricRepository.GetTopHttpStatusFromIndexAndDate(startingIndex, startDate);

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

        var topHitHttpEndpoints = await httpMetricRepository.GetTopHitHttpEndpoints(startingIndex, startDate);

        Assert.IsNotNull(topHitHttpEndpoints);
        Assert.AreEqual(1, topHitHttpEndpoints.Count); 
        Assert.AreEqual("Teapot", topHitHttpEndpoints.First().Endpoint);
        Assert.AreEqual("Controller", topHitHttpEndpoints.First().Controller);
        Assert.AreEqual("Controller/Teapot", topHitHttpEndpoints.First().FullPath);
    }
}

