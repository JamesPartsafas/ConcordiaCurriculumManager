using AutoMapper;
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

        public required Dictionary<ComponentCodeEnum, int?> ComponentCodes { get; set; }

        public required Dictionary<string, string> SupportingFiles { get; set; }
    }

    public class SupportingFilesResolver : IValueResolver<Course, CourseDataDTO, Dictionary<string, string>>
    {
        public Dictionary<string, string> Resolve(Course source, CourseDataDTO destination, Dictionary<string, string> destMember, ResolutionContext context)
        {
            if (source.SupportingFiles != null)
            {
                return SupportingFile.GetSupportingFilesDictionary(source.SupportingFiles);
            }
            return new Dictionary<string, string>();
        }
    }

    public class ComponentCodeResolver : IValueResolver<Course, CourseDataDTO, Dictionary<ComponentCodeEnum, int?>>
    {
        public Dictionary<ComponentCodeEnum, int?> Resolve(Course source, CourseDataDTO destination, Dictionary<ComponentCodeEnum, int?> destMember, ResolutionContext context)
        {
            if (source.CourseCourseComponents != null)
            {
                return CourseCourseComponent.GetComponentCodeEnumDictionary(source.CourseCourseComponents);
            }
            return new Dictionary<ComponentCodeEnum, int?>();
        }
    }
}

