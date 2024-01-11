using ConcordiaCurriculumManager.DTO.Courses;
using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.OutputDTOs;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;
using System;
namespace ConcordiaCurriculumManager.DTO.Dossiers
{
    public class DossierReportDTO
    {
        public required Guid InitiatorId { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public required DossierStateEnum State { get; set; }

        public IList<CourseCreationRequestCourseDetailsDTO> CourseCreationRequests { get; set; } = new List<CourseCreationRequestCourseDetailsDTO>();

        public IList<CourseModificationRequestCourseDetailsDTO> CourseModificationRequests { get; set; } = new List<CourseModificationRequestCourseDetailsDTO>();

        public IList<CourseDeletionRequestCourseDetailsDTO> CourseDeletionRequests { get; set; } = new List<CourseDeletionRequestCourseDetailsDTO>();

        public IList<ApprovalStage> ApprovalStages { get; set; } = new List<ApprovalStage>();

        public IList<CourseDataDTO> OldCourses { get; set; } = new List<CourseDataDTO>();
    }
}

