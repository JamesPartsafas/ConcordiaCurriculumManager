using ConcordiaCurriculumManager.Models.Curriculum.CourseGroupings;
using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ConcordiaCurriculumManager.Repositories
{
    public interface ICourseIdentifiersRepository
    {
        public Task<CourseIdentifier?> GetCourseIdentifierByConcordiaCourseId(int concordiaCourseId);
        public Task<bool> SaveCourseIdentifier(CourseIdentifier courseIdentifier);
    }

    public class CourseIdentifiersRepository: ICourseIdentifiersRepository
    {
        private readonly CCMDbContext _dbContext;

        public CourseIdentifiersRepository(CCMDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CourseIdentifier?> GetCourseIdentifierByConcordiaCourseId(int concordiaCourseId)
        {
            return await _dbContext.CourseIdentifiers
                .Where(ci => ci.ConcordiaCourseId.Equals(concordiaCourseId))
                .FirstOrDefaultAsync();
        }

        public async Task<bool> SaveCourseIdentifier(CourseIdentifier courseIdentifier)
        {
            await _dbContext.CourseIdentifiers.AddAsync(courseIdentifier);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}

