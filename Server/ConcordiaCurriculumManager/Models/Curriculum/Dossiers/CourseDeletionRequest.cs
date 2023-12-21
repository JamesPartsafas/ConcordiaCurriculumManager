namespace ConcordiaCurriculumManager.Models.Curriculum.Dossiers;

public class CourseDeletionRequest : CourseRequestOnExistingCourse
{
    public void MarkAsDeleted(ICollection<CourseVersion> currentVersions)
    {
        int nextVersion = GetNextCourseVersion(currentVersions);

        Course!.MarkAsDeleted(nextVersion);
    }
}
