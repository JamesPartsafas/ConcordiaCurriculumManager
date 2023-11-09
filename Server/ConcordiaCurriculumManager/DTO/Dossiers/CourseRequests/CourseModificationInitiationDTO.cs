using ConcordiaCurriculumManager.Models.Curriculum;
using System;
namespace ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests
{
    public class CourseModificationInitiationDTO : CourseRequestInitiationDTO
    {
        public required int CourseId { get; set; }

        public override CourseStateEnum GetAssociatedCourseState() => CourseStateEnum.CourseChangeProposal;
    }
}
