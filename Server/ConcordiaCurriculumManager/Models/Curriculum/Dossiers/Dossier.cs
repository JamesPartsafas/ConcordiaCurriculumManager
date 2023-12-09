using ConcordiaCurriculumManager.DTO.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Models.Users;
using NpgsqlTypes;

namespace ConcordiaCurriculumManager.Models.Curriculum.Dossiers
{

    public class Dossier : BaseModel
    {
        public User? Initiator { get; set; }

        public required Guid InitiatorId { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public required DossierStateEnum State { get; set; }

        public List<CourseCreationRequest> CourseCreationRequests { get; set; } = new List<CourseCreationRequest>();

        public List<CourseModificationRequest> CourseModificationRequests { get; set; } = new List<CourseModificationRequest>();

        public List<CourseDeletionRequest> CourseDeletionRequests { get; set; } = new List<CourseDeletionRequest>();

        public IList<ApprovalStage> ApprovalStages { get; set; } = new List<ApprovalStage>();

        public void MarkAsRejected()
        {
            if (State != DossierStateEnum.InReview)
                throw new BadRequestException("A dossier that is not currently in review cannot be rejected");

            State = DossierStateEnum.Rejected;
        }

        public IList<ApprovalStage> PrepareForPublishing(DossierSubmissionDTO dto)
        {
            VerifyPublishStateIsConsistentOrThrow();

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

            this.State = DossierStateEnum.InReview;

            return stages;
        }

        private void VerifyPublishStateIsConsistentOrThrow()
        {
            if (State == DossierStateEnum.InReview) throw new BadRequestException("The dossier has already been submitted for review");
            if (State == DossierStateEnum.Rejected) throw new BadRequestException("The dossier has already been submitted for review and was rejected");
            if (State == DossierStateEnum.Approved) throw new BadRequestException("The dossier has already been submitted for review and was approved");
        }
    }

    public enum DossierStateEnum
    {
        [PgName(nameof(Created))]
        Created,

        [PgName(nameof(InReview))]
        InReview,

        [PgName(nameof(Rejected))]
        Rejected,

        [PgName(nameof(Approved))]
        Approved
    }
}

