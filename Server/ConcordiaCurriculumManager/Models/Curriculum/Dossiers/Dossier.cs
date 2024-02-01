using ConcordiaCurriculumManager.DTO.CourseGrouping;
using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.CourseGroupingRequests;
using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.InputDTOs;
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

        public IList<CourseGroupingRequest> CourseGroupingRequests { get; set; } = new List<CourseGroupingRequest>();

        public IList<ApprovalStage> ApprovalStages { get; set; } = new List<ApprovalStage>();

        public required DossierDiscussion Discussion { get; set; }

        public void MarkAsRejected()
        {
            if (State != DossierStateEnum.InReview)
                throw new BadRequestException("A dossier that is not currently in review cannot be rejected");

            var currentStage = ApprovalStages.Where(stage => stage.IsCurrentStage).First();

            currentStage.IsCurrentStage = false;

            State = DossierStateEnum.Rejected;
        }

        public void MarkAsReturned()
        {
            if (IsInInitialStageOfReviewPipeline())
                throw new BadRequestException("The dossier cannot be returned as it is still in the initial stage of its approval pipeline");

            var currentStage = ApprovalStages.Where(stage => stage.IsCurrentStage).First();

            currentStage.IsCurrentStage = false;

            var previousStage = ApprovalStages.Where(stage => stage.StageIndex == currentStage.StageIndex - 1).First();
            previousStage.IsCurrentStage = true;
        }

        public void MarkAsForwarded()
        {
            if (IsInFinalStageOfReviewPipeline())
                throw new BadRequestException("The dossier cannot be forwarded as it is already in the final stage of its approval pipeline");

            var currentStage = ApprovalStages.Where(stage => stage.IsCurrentStage).First();

            currentStage.IsCurrentStage = false;

            var nextStage = ApprovalStages.Where(stage => stage.StageIndex == currentStage.StageIndex + 1).First();
            nextStage.IsCurrentStage = true;
        }

        public void MarkAsAccepted(ICollection<CourseVersion> currentVersions)
        {
            if (!IsInFinalStageOfReviewPipeline())
                throw new BadRequestException("The dossier cannot be accepted as it is not in the final stage of its approval pipeline");

            var currentStage = ApprovalStages.Where(stage => stage.IsCurrentStage).First();

            currentStage.IsCurrentStage = false;

            State = DossierStateEnum.Approved;

            foreach (var creationRequest in CourseCreationRequests)
                creationRequest.MarkAsAccepted(currentVersions);

            foreach (var modificationRequest in CourseModificationRequests)
                modificationRequest.MarkAsAccepted(currentVersions);

            foreach (var deletionRequest in CourseDeletionRequests)
                deletionRequest.MarkAsDeleted(currentVersions);
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

        public bool IsInInitialStageOfReviewPipeline()
        {
            VerifyStagesAreLoadedOrThrow();

            var initialStage = ApprovalStages.Where(stage => stage.IsInitialStage()).First();

            return initialStage.IsCurrentStage;
        }

        public bool IsInFinalStageOfReviewPipeline()
        {
            VerifyStagesAreLoadedOrThrow();

            var finalStage = ApprovalStages.Where(stage => stage.IsFinalStage).First();

            return finalStage.IsCurrentStage;
        }

        private void VerifyStagesAreLoadedOrThrow()
        {
            if (ApprovalStages.Count == 0)
                throw new BadRequestException($"The approval stages have not been loaded for the dossier {Id}");
        }

        public CourseGroupingRequest CreateCourseGroupingCreationRequest(CourseGroupingCreationRequestDTO dto)
        {
            var grouping = CourseGroupingRequest.CreateCourseGroupingCreationRequestFromDTO(dto);
            CourseGroupingRequests.Add(grouping);

            return grouping;
        }

        public CourseGroupingRequest CreateCourseGroupingModificationRequest(CourseGroupingModificationRequestDTO dto)
        {
            VerifyDossierDoesNotContainDuplicateGroupingRequests(dto.CourseGrouping);

            var grouping = CourseGroupingRequest.CreateCourseGroupingModificationRequestFromDTO(dto);
            CourseGroupingRequests.Add(grouping);

            return grouping;
        }

        private void VerifyDossierDoesNotContainDuplicateGroupingRequests(CourseGroupingModificationInputDTO dto)
        {
            if (CourseGroupingRequests.Any(request => request.CourseGrouping!.CommonIdentifier.Equals(dto.CommonIdentifier)))
                throw new BadRequestException("The dossier already contains a request for this course grouping");
        }

        public CourseGroupingRequest GetGroupingRequestForDeletion(Guid requestId)
        {
            var request = CourseGroupingRequests.Where(request => request.Id.Equals(requestId)).FirstOrDefault();

            if (request is null)
                throw new BadRequestException($"The course grouping request with Id {requestId} does not exist");

            return request;
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

