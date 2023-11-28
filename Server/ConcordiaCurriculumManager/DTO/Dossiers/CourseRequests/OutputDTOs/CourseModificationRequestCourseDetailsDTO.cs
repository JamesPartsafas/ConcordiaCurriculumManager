using ConcordiaCurriculumManager.DTO.Courses;
using System;
namespace ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.OutputDTOs
{
    public class CourseModificationRequestCourseDetailsDTO : CourseModificationRequestDTO
    {
        public required CourseDataDTO Course { get; set; }
    }
}

