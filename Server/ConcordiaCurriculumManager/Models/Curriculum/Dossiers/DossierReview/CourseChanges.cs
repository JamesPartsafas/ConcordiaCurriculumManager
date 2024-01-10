namespace ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;

public class CourseChanges
{
    public required IList<CourseCreationRequest> CourseCreationRequests {get; set;}
    public required IList<CourseModificationRequest> CourseModificationRequests { get; set; }
    public required IList<CourseDeletionRequest> CourseDeletionRequests { get; set; }
    public required IList<Course> OldCourses { get; set; }
}
