using ConcordiaCurriculumManager.Models.Curriculum.Dossier;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ConcordiaCurriculumManager.Repositories;

public interface IDossierRepository
{
    Task<bool> SaveCourseCreationDossier(CourseCreationDossier dossier);
}

public class DossierRepository : IDossierRepository
{
    private readonly CCMDbContext _dbContext;

    public DossierRepository(CCMDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> SaveCourseCreationDossier(CourseCreationDossier dossier)
    {
        await _dbContext.CourseCreationDossiers.AddAsync(dossier);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }
}
