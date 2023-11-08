using ConcordiaCurriculumManager.Models.Curriculum;
using System;
namespace ConcordiaCurriculumManager.DTO.Courses
{
    public class CourseDataDTO
    {
        public required Guid Id { get; set; }

        public required int CourseID { get; set; }

        public required string Subject { get; set; }

        public required string Catalog { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public required string CreditValue { get; set; }

        public required string PreReqs { get; set; }

        public required CourseCareerEnum Career { get; set; }

        public string? EquivalentCourses { get; set; }

        public string? CourseNotes { get; set; }

        public required CourseStateEnum CourseState { get; set; }

        public ICollection<CourseCourseComponent>? CourseCourseComponents { get; set; }

        public ICollection<SupportingFile>? SupportingFiles { get; set; }
    }
}

