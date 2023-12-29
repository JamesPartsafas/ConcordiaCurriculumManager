using ConcordiaCurriculumManager.Filters.Exceptions;

namespace ConcordiaCurriculumManager.Models.Curriculum.Dossiers;

public class CourseCreationRequest : CourseRequest
{
    public required Guid NewCourseId { get; set; }

    public Course? NewCourse { get; set; }

    public void MarkAsAccepted(ICollection<CourseVersion> currentVersions)
    {
        if (NewCourse == null) throw new NotFoundException($"The creation request for {NewCourseId} is not correctly loaded");

        var nextVersion = GetNextCourseVersion(NewCourse.Subject, NewCourse.Catalog, currentVersions);

        NewCourse.MarkAsAccepted(nextVersion);
    }

    private int GetNextCourseVersion(string subject, string catalog, ICollection<CourseVersion> currentVersions)
    {
        CourseVersion? currentVersion = currentVersions
            .Where(cv => cv.Subject == subject && cv.Catalog == catalog)
            .FirstOrDefault();

        var nextVersion = currentVersion == null ? 1 : currentVersion.Version + 1;

        return nextVersion;
    }
}
