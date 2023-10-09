using System;
namespace ConcordiaCurriculumManager.Models.Curriculum.Dossiers
{
    public class CourseModificationRequest : BaseModel
    {
        public required Guid CourseId { get; set; }

        public Course? Course { get; set; }

        public required Guid DossierId { get; set; }

        public Dossier? Dossier { get; set; }
    }
}

