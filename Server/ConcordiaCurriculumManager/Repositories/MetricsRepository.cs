using ConcordiaCurriculumManager.Models.Metrics;
using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ConcordiaCurriculumManager.Repositories;

public interface IMetricsRepository
{
    Task<List<HttpEndpointCount>> GetTopHitHttpEndpoints(int startingIndex, DateOnly startDate);
    Task<List<HttpStatusCount>> GetTopHttpStatusFromIndexAndDate(int startingIndex, DateOnly startDate);
    Task<bool> SaveHttpMetricAsync(HttpMetric httpMetrics);
    Task<bool> SaveDossierMetricAsync(DossierMetric httpMetrics);
    Task<List<DossierViewCount>> GetTopViewedDossierFromIndexAndDate(int startingIndex, DateOnly startDate);
    Task<List<UserDossierViewedCount>> GetTopDossierViewingUser(int startingIndex, DateOnly startDate);
}

public class MetricsRepository : IMetricsRepository
{
    private readonly CCMDbContext _dbContext;

    public MetricsRepository(CCMDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> SaveHttpMetricAsync(HttpMetric httpMetric)
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

    public async Task<bool> SaveDossierMetricAsync(DossierMetric dossierMetrics)
    {
        await _dbContext.AddAsync(dossierMetrics);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<List<DossierViewCount>> GetTopViewedDossierFromIndexAndDate(int startingIndex, DateOnly startDate)
    {
        var tempDossierViews = _dbContext.DossierMetrics
           .Where(d => d.CreatedDate >= startDate.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc))
           .GroupBy(d => d.DossierId)
           .Select(g => new
           {
               DossierId = g.Key,
               Count = g.Count()
           })
           .OrderByDescending(x => x.Count)
           .Skip(startingIndex)
           .Take(10)
           .ToList();

        var result = new List<DossierViewCount>();
        foreach (var tempDossierView in tempDossierViews)
        {
            var dossier = await _dbContext.Dossiers.FindAsync(tempDossierView.DossierId);
            result.Add(new DossierViewCount
            {
                DossierId = tempDossierView.DossierId,
                Dossier = dossier,
                Count = tempDossierView.Count
            });
        }

        return result;
    }

    public async Task<List<UserDossierViewedCount>> GetTopDossierViewingUser(int startingIndex, DateOnly startDate)
    {
        var tempUserDossierViews = _dbContext.DossierMetrics
           .Where(d => d.CreatedDate >= startDate.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc))
           .GroupBy(d => d.UserId)
           .Select(g => new
           {
               UserId = g.Key,
               Count = g.Count()
           })
           .OrderByDescending(x => x.Count)
           .Skip(startingIndex)
           .Take(10)
           .ToList();

        var result = new List<UserDossierViewedCount>();
        foreach (var tempUserDossierView in tempUserDossierViews)
        {
            var dossier = await _dbContext.Users.FindAsync(tempUserDossierView.UserId);
            result.Add(new UserDossierViewedCount
            {
                UserId = tempUserDossierView.UserId,
                User = dossier,
                Count = tempUserDossierView.Count
            });
        }

        return result;
    }
}
