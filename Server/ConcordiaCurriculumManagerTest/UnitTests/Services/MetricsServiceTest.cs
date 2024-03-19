using ConcordiaCurriculumManager.Repositories;
using ConcordiaCurriculumManager.Services;
using ConcordiaCurriculumManagerTest.UnitTests.UtilityFunctions;
using Moq;

namespace ConcordiaCurriculumManagerTest.UnitTests.Services;

[TestClass]
public class MetricsServiceTest
{
    private Mock<IHttpMetricsRepository> _httpMetricsRepository = null!;
    private MetricsService _metricServices = null!;

    [TestInitialize]
    public void Initialize()
    {
        _httpMetricsRepository = new Mock<IHttpMetricsRepository>();
        _metricServices = new MetricsService(_httpMetricsRepository.Object);
    }

    [TestMethod]
    public async Task SaveHttpMetric_Calls_SaveMetricAsync_In_Repository()
    {
        var httpMetric = TestData.GetHttpMetric();
        await _metricServices.SaveHttpMetric(httpMetric);

        _httpMetricsRepository.Verify(repo => repo.SaveMetricAsync(httpMetric), Times.Once);
    }

    [TestMethod]
    public async Task GetTopHttpStatusCodes_Calls_GetTopHttpStatusFromIndexAndDate_In_Repository_With_Correct_Parameters()
    {
        int startingIndex = 0;
        DateOnly startDate = DateOnly.FromDateTime(DateTime.Now);

        await _metricServices.GetTopHttpStatusCodes(startingIndex, startDate);
        _httpMetricsRepository.Verify(repo => repo.GetTopHttpStatusFromIndexAndDate(startingIndex, startDate), Times.Once);
    }

    [TestMethod]
    public async Task GetTopHitHttpEndpoints_Calls_GetTopHitHttpEndpoints_In_Repository_With_Correct_Parameters()
    {
        int startingIndex = 0;
        DateOnly startDate = DateOnly.FromDateTime(DateTime.Now);

        await _metricServices.GetTopHitHttpEndpoints(startingIndex, startDate);
        _httpMetricsRepository.Verify(repo => repo.GetTopHitHttpEndpoints(startingIndex, startDate), Times.Once);
    }
}
