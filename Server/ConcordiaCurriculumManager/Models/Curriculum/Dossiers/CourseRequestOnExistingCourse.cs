using ConcordiaCurriculumManager.Filters.Exceptions;

namespace ConcordiaCurriculumManager.Models.Curriculum.Dossiers;

public class CourseRequestOnExistingCourse : CourseRequest
{
    public required Guid CourseId { get; set; }

    public Course? Course { get; set; }

    protected int GetNextCourseVersion(ICollection<CourseVersion> currentVersions)
    {
        if (Course == null) throw new NotFoundException($"The request for {CourseId} is not correctly loaded");

        CourseVersion currentVersion = currentVersions
            .Where(cv => cv.Subject == Course.Subject && cv.Catalog == Course.Catalog)
            .First();

        return currentVersion.Version + 1;
    }
}
