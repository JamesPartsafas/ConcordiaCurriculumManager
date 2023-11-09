using System;
namespace ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests
{
    public class CourseModificationRequestDTO : CourseRequestDTO
    {
        public required Guid CourseId { get; set; }
    }
}
