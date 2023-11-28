using ConcordiaCurriculumManager.Models.Curriculum;
using System;
namespace ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.InputDTOs
{
    public class CourseModificationInitiationDTO : CourseRequestInitiationDTO
    {
        public required int CourseId { get; set; }

        public override CourseStateEnum GetAssociatedCourseState() => CourseStateEnum.CourseChangeProposal;
    }
}
