using ConcordiaCurriculumManager.Models.Curriculum;
using System;
namespace ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests
{
    public class EditCourseCreationRequestDTO : CourseCreationInitiationDTO
    {
        public required Guid Id { get; set; }
    }
}

