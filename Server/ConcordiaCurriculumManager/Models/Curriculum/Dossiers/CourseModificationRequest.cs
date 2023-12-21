using System;
namespace ConcordiaCurriculumManager.Models.Curriculum.Dossiers
{
    public class CourseModificationRequest : CourseRequestOnExistingCourse
    {
        public void MarkAsAccepted(ICollection<CourseVersion> currentVersions)
        {
            int nextVersion = GetNextCourseVersion(currentVersions);

            Course!.MarkAsAccepted(nextVersion);
        }
    }
}

