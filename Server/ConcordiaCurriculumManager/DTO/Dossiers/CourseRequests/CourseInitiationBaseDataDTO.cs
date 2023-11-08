using ConcordiaCurriculumManager.Models.Curriculum;
using System;
namespace ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests
{
    public class CourseInitiationBaseDataDTO : CourseInitiationDTO
    {
        public required string Title { get; set; }

        public required string Description { get; set; }

        public required string CourseNotes { get; set; }

        public required string CreditValue { get; set; }

        public required string PreReqs { get; set; }

        public required CourseCareerEnum Career { get; set; }

        public required string EquivalentCourses { get; set; }

        public required Dictionary<ComponentCodeEnum, int?> ComponentCodes { get; set; }

        public required Dictionary<string, string> SupportingFiles { get; set; }
    }
}

