using System;
namespace ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests
{
    public class EditCourseModificationRequestDTO : CourseInitiationBaseDataDTO
    {
        public required Guid Id { get; set; }
    }
}

