using ConcordiaCurriculumManager.Filters.Exceptions;

namespace ConcordiaCurriculumManager.Models.Curriculum.Dossiers;

public class CourseCreationRequest : CourseRequest
{
    public required Guid NewCourseId { get; set; }

    public Course? NewCourse { get; set; }

    public void MarkAsAccepted()
    {
        if (NewCourse == null) throw new NotFoundException($"The creation request for {NewCourseId} is not correctly loaded");

        NewCourse.MarkAsAccepted(1);
    }
}
