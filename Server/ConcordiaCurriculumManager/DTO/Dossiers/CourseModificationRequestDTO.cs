using System;
namespace ConcordiaCurriculumManager.DTO.Dossiers
{
    public class CourseModificationRequestDTO
    {
        public required Guid Id { get; set; }

        public required Guid CourseId { get; set; }

        public required Guid DossierId { get; set; }
    }
}

