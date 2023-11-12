using System;
namespace ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.InputDTOs
{
    public class EditCourseModificationRequestDTO : CourseInitiationBaseDataDTO
    {
        public required Guid Id { get; set; }
    }
}

