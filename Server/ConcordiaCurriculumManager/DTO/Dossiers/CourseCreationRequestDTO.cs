namespace ConcordiaCurriculumManager.DTO.Dossiers;

public class CourseCreationRequestDTO
{
    public required Guid Id { get; set; }

    public required Guid NewCourseId { get; set; }

    public required Guid DossierId { get; set; }

}
