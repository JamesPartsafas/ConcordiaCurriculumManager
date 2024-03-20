using ConcordiaCurriculumManager.Models.Metrics;
using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ConcordiaCurriculumManager.Repositories;

public interface IHttpMetricsRepository
{
    Task<List<HttpEndpointCount>> GetTopHitHttpEndpoints(int startingIndex, DateOnly startDate);
    Task<List<HttpStatusCount>> GetTopHttpStatusFromIndexAndDate(int startingIndex, DateOnly startDate);
    Task<bool> SaveMetricAsync(HttpMetric httpMetrics);
}

public class HttpMetricsRepository : IHttpMetricsRepository
{
    private readonly CCMDbContext _dbContext;

    public HttpMetricsRepository(CCMDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> SaveMetricAsync(HttpMetric httpMetric)
    {
        await _dbContext.AddAsync(httpMetric);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<List<HttpStatusCount>> GetTopHttpStatusFromIndexAndDate(int startingIndex, DateOnly startDate)
    {
        return await _dbContext.HttpMetrics
         .Where(m => m.CreatedDate >= startDate.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc))
         .GroupBy(m => m.ResponseStatusCode)
         .Select(g => new HttpStatusCount
         {
             HttpStatus = g.Key,
             Count = g.Count()
         })
         .OrderByDescending(x => x.Count)
         .Skip(startingIndex)
         .Take(10)
         .ToListAsync();
    }

    public async Task<List<HttpEndpointCount>> GetTopHitHttpEndpoints(int startingIndex, DateOnly startDate)
    {
        return await _dbContext.HttpMetrics
         .Where(m => m.CreatedDate >= startDate.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc))
         .GroupBy(m => new { m.Endpoint, m.Controller })
         .Select(g => new HttpEndpointCount
         {
             Endpoint = g.Key.Endpoint,
             Controller = g.Key.Controller,
             FullPath = $"{g.Key.Controller}/{g.Key.Endpoint}",
             Count = g.Count()
         })
         .OrderByDescending(x => x.Count)
         .Skip(startingIndex)
         .Take(10)
         .ToListAsync();
    }
}
