using ConcordiaCurriculumManager.Models.Metrics;
using ConcordiaCurriculumManager.Repositories;

namespace ConcordiaCurriculumManager.Services;

public interface IMetricsService
{
    Task<List<UserDossierViewedCount>> GetTopDossierViewingUser(int fromIndex, DateOnly startDate);
    Task<List<HttpEndpointCount>> GetTopHitHttpEndpoints(int startingIndex, DateOnly startDate);
    Task<List<HttpStatusCount>> GetTopHttpStatusCodes(int startingIndex, DateOnly startDate);
    Task<List<DossierViewCount>> GetTopViewedDossier(int startingIndex, DateOnly startDate);
    Task<bool> SaveDossierMetric(DossierMetric dossierMetric);
    Task<bool> SaveHttpMetric(HttpMetric httpMetrics);
}

public class MetricsService : IMetricsService
{
    private readonly IMetricsRepository _metricsRepository;

    public MetricsService(IMetricsRepository httpMetricsRepository)
    {
        _metricsRepository = httpMetricsRepository;
    }

    public async Task<bool> SaveHttpMetric(HttpMetric httpMetrics) => await _metricsRepository.SaveHttpMetricAsync(httpMetrics);

    public async Task<List<HttpStatusCount>> GetTopHttpStatusCodes(int startingIndex, DateOnly startDate) => await _metricsRepository.GetTopHttpStatusFromIndexAndDate(startingIndex, startDate);
    
    public async Task<List<HttpEndpointCount>> GetTopHitHttpEndpoints(int startingIndex, DateOnly startDate) => await _metricsRepository.GetTopHitHttpEndpoints(startingIndex, startDate);

    public async Task<bool> SaveDossierMetric(DossierMetric dossierMetric) => await _metricsRepository.SaveDossierMetricAsync(dossierMetric);

    public async Task<List<DossierViewCount>> GetTopViewedDossier(int startingIndex, DateOnly startDate) => await _metricsRepository.GetTopViewedDossierFromIndexAndDate(startingIndex, startDate);

    public async Task<List<UserDossierViewedCount>> GetTopDossierViewingUser(int fromIndex, DateOnly startDate) => await _metricsRepository.GetTopDossierViewingUser(fromIndex, startDate);
}
