namespace ConcordiaCurriculumManager.DTO.Courses;

public class SupportingFileDTO
{
    public required Guid Id { get; set; }

    public required Guid CourseId { get; set; }

    public required string FileName { get; set; }

    public required string ContentBase64 { get; set; }
}
