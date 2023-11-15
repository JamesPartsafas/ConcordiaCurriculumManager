using ConcordiaCurriculumManager.DTO.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Models.Users;

namespace ConcordiaCurriculumManager.Models.Curriculum.Dossiers
{

    public class Dossier : BaseModel
    {
        public User? Initiator { get; set; }

        public required Guid InitiatorId { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public required bool Published { get; set; }

        public List<CourseCreationRequest> CourseCreationRequests { get; set; } = new List<CourseCreationRequest>();

        public List<CourseModificationRequest> CourseModificationRequests { get; set; } = new List<CourseModificationRequest>();

        public List<CourseDeletionRequest> CourseDeletionRequests { get; set; } = new List<CourseDeletionRequest>();

        public IList<ApprovalStage> ApprovalStages { get; set; } = new List<ApprovalStage>();

        public IList<ApprovalStage> PrepareForPublishing(DossierSubmissionDTO dto)
        {
            List<ApprovalStage> stages = dto.GroupIds.Select((Guid groupId, int index) =>
            {
                return new ApprovalStage
                {
                    Id = Guid.NewGuid(),
                    GroupId = groupId,
                    DossierId = this.Id,
                    StageIndex = index,
                    IsCurrentStage = false,
                    IsFinalStage = false
                };
            }).ToList();

            stages.First().IsCurrentStage = true;
            stages.Last().IsFinalStage = true;

            this.Published = true;

            return stages;
        }
    }
}

