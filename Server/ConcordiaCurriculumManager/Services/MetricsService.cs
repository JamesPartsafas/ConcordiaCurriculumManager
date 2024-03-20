using ConcordiaCurriculumManager.Models.Metrics;
using ConcordiaCurriculumManager.Repositories;

namespace ConcordiaCurriculumManager.Services;

public interface IMetricsService
{
    Task<List<HttpEndpointCount>> GetTopHitHttpEndpoints(int startingIndex, DateOnly startDate);
    Task<List<HttpStatusCount>> GetTopHttpStatusCodes(int startingIndex, DateOnly startDate);
    Task<bool> SaveHttpMetric(HttpMetric httpMetrics);
}

public class MetricsService : IMetricsService
{
    private readonly IHttpMetricsRepository _httpMetricsRepository;

    public MetricsService(IHttpMetricsRepository httpMetricsRepository)
    {
        _httpMetricsRepository = httpMetricsRepository;
    }

    public async Task<bool> SaveHttpMetric(HttpMetric httpMetrics) => await _httpMetricsRepository.SaveMetricAsync(httpMetrics);

    public async Task<List<HttpStatusCount>> GetTopHttpStatusCodes(int startingIndex, DateOnly startDate) => await _httpMetricsRepository.GetTopHttpStatusFromIndexAndDate(startingIndex, startDate);
    public async Task<List<HttpEndpointCount>> GetTopHitHttpEndpoints(int startingIndex, DateOnly startDate) => await _httpMetricsRepository.GetTopHitHttpEndpoints(startingIndex, startDate);
}
