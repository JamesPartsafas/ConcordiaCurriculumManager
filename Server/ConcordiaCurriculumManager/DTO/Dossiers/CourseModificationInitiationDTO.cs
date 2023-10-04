using ConcordiaCurriculumManager.Models.Curriculum;
using System;
namespace ConcordiaCurriculumManager.DTO.Dossiers
{
    public class CourseModificationInitiationDTO
    {
        public required string Subject { get; set; }

        public required string Catalog { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public required string CreditValue { get; set; }

        public required string PreReqs { get; set; }

        public required CourseCareerEnum Career { get; set; }

        public required string EquivalentCourses { get; set; }

        public required List<ComponentCodeEnum> ComponentCodes { get; set; }

        public required Guid DossierId { get; set; }

        public required int CourseId { get; set; }
    }
}

