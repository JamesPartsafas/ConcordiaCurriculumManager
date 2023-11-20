using ConcordiaCurriculumManager.DTO.Courses;
using System;
namespace ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.OutputDTOs
{
    public class CourseCreationRequestCourseDetailsDTO : CourseCreationRequestDTO
    {
        public required CourseDataDTO NewCourse { get; set; }
    }
}

