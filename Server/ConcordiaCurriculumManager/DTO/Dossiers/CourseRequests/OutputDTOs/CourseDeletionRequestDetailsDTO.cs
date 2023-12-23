using ConcordiaCurriculumManager.DTO.Courses;
using System;
namespace ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.OutputDTOs
{
    public class CourseDeletionRequestDetailsDTO : CourseDeletionRequestDTO
    {
        public required CourseDataDTO Course { get; set; }
    }
}

