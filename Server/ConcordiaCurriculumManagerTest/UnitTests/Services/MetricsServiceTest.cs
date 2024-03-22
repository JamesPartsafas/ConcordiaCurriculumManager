using ConcordiaCurriculumManager.Repositories;
using ConcordiaCurriculumManager.Services;
using ConcordiaCurriculumManagerTest.UnitTests.UtilityFunctions;
using Moq;

namespace ConcordiaCurriculumManagerTest.UnitTests.Services;

[TestClass]
public class MetricsServiceTest
{
    private Mock<IMetricsRepository> _metricsRepository = null!;
    private MetricsService _metricServices = null!;

    [TestInitialize]
    public void Initialize()
    {
        _metricsRepository = new Mock<IMetricsRepository>();
        _metricServices = new MetricsService(_metricsRepository.Object);
    }

    [TestMethod]
    public async Task SaveHttpMetric_Calls_SaveMetricAsync_In_Repository()
    {
        var httpMetric = TestData.GetHttpMetric();
        await _metricServices.SaveHttpMetric(httpMetric);

        _metricsRepository.Verify(repo => repo.SaveHttpMetricAsync(httpMetric), Times.Once);
    }

    [TestMethod]
    public async Task GetTopHttpStatusCodes_Calls_GetTopHttpStatusFromIndexAndDate_In_Repository_With_Correct_Parameters()
    {
        int startingIndex = 0;
        DateOnly startDate = DateOnly.FromDateTime(DateTime.Now);

        await _metricServices.GetTopHttpStatusCodes(startingIndex, startDate);
        _metricsRepository.Verify(repo => repo.GetTopHttpStatusFromIndexAndDate(startingIndex, startDate), Times.Once);
    }

    [TestMethod]
    public async Task GetTopHitHttpEndpoints_Calls_GetTopHitHttpEndpoints_In_Repository_With_Correct_Parameters()
    {
        int startingIndex = 0;
        DateOnly startDate = DateOnly.FromDateTime(DateTime.Now);

        await _metricServices.GetTopHitHttpEndpoints(startingIndex, startDate);
        _metricsRepository.Verify(repo => repo.GetTopHitHttpEndpoints(startingIndex, startDate), Times.Once);
    }

    [TestMethod]
    public async Task GetTopViewedDossier_Calls_GetTopViewedDossier_In_Repository_With_Correct_Parameters()
    {
        int startingIndex = 0;
        DateOnly startDate = DateOnly.FromDateTime(DateTime.Now);

        await _metricServices.GetTopViewedDossier(startingIndex, startDate);
        _metricsRepository.Verify(repo => repo.GetTopViewedDossierFromIndexAndDate(startingIndex, startDate), Times.Once);
    }

    [TestMethod]
    public async Task GetTopDossierViewingUser_Calls_GetTopDossierViewingUser_In_Repository_With_Correct_Parameters()
    {
        int startingIndex = 0;
        DateOnly startDate = DateOnly.FromDateTime(DateTime.Now);

        await _metricServices.GetTopDossierViewingUser(startingIndex, startDate);
        _metricsRepository.Verify(repo => repo.GetTopDossierViewingUser(startingIndex, startDate), Times.Once);
    }
}
