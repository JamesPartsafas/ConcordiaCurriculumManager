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

        public IList<ApprovalHistory> ApprovalHistories { get; set; } = new List<ApprovalHistory>();

        public required DossierDiscussion Discussion { get; set; }

        public void MarkAsRejected(User user)
        {
            if (State != DossierStateEnum.InReview)
                throw new BadRequestException("A dossier that is not currently in review cannot be rejected");

            var currentStage = ApprovalStages.Where(stage => stage.IsCurrentStage).First();

            currentStage.IsCurrentStage = false;

            var currentOrderIndex = (ApprovalHistories.Max(h => (int?)h.OrderIndex)) ?? 0;

            ApprovalHistories.Add(new ApprovalHistory 
            {
                DossierId = Id,
                GroupId = currentStage.GroupId,
                UserId = user.Id,
                OrderIndex = currentOrderIndex + 1,
                Action = ActionEnum.Reject
            });

            State = DossierStateEnum.Rejected;
        }

        public void MarkAsReturned(User user)
        {
            if (IsInInitialStageOfReviewPipeline())
                throw new BadRequestException("The dossier cannot be returned as it is still in the initial stage of its approval pipeline");

            var currentStage = ApprovalStages.Where(stage => stage.IsCurrentStage).First();

            currentStage.IsCurrentStage = false;

            var previousStage = ApprovalStages.Where(stage => stage.StageIndex == currentStage.StageIndex - 1).First();
            previousStage.IsCurrentStage = true;

            var currentOrderIndex = (ApprovalHistories.Max(h => (int?)h.OrderIndex)) ?? 0;

            ApprovalHistories.Add(new ApprovalHistory
            {
                DossierId = Id,
                GroupId = currentStage.GroupId,
                UserId = user.Id,
                OrderIndex = currentOrderIndex + 1,
                Action = ActionEnum.Return
            });
        }

        public void MarkAsForwarded(User user)
        {
            if (IsInFinalStageOfReviewPipeline())
                throw new BadRequestException("The dossier cannot be forwarded as it is already in the final stage of its approval pipeline");

            var currentStage = ApprovalStages.Where(stage => stage.IsCurrentStage).First();

            currentStage.IsCurrentStage = false;

            var nextStage = ApprovalStages.Where(stage => stage.StageIndex == currentStage.StageIndex + 1).First();
            nextStage.IsCurrentStage = true;

            var currentOrderIndex = (ApprovalHistories.Max(h => (int?)h.OrderIndex)) ?? 0;

            ApprovalHistories.Add(new ApprovalHistory
            {
                DossierId = Id,
                GroupId = currentStage.GroupId,
                UserId = user.Id,
                OrderIndex = currentOrderIndex + 1,
                Action = ActionEnum.Forward
            });
        }

        public void MarkAsAccepted(ICollection<CourseVersion> currentVersions, User user)
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

            var currentOrderIndex = (ApprovalHistories.Max(h => (int?)h.OrderIndex)) ?? 0;

            ApprovalHistories.Add(new ApprovalHistory
            {
                DossierId = Id,
                GroupId = currentStage.GroupId,
                UserId = user.Id,
                OrderIndex = currentOrderIndex + 1,
                Action = ActionEnum.Accept
            });
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

        public CourseGroupingRequest CreateCourseGroupingDeletionRequest(CourseGroupingModificationRequestDTO dto)
        {
            VerifyDossierDoesNotContainDuplicateGroupingRequests(dto.CourseGrouping);

            var grouping = CourseGroupingRequest.CreateCourseGroupingDeletionRequestFromDTO(dto);
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

            CourseGroupingRequests.Remove(request);

            return request;
        }

        public bool IsDossierCreatingCourse(int concordiaCourseId)
        {
            var request = CourseCreationRequests.Where(request => request.NewCourse!.CourseID.Equals(concordiaCourseId)).FirstOrDefault();

            if (request is null)
                return false;

            return true;
        }

        public bool IsDossierCreatingGrouping(Guid commonIdentifier)
        {
            var request = CourseGroupingRequests.Where(request =>
                request.CourseGrouping!.CommonIdentifier.Equals(commonIdentifier) && request.RequestType.Equals(RequestType.CreationRequest))
                .FirstOrDefault();

            if (request is null)
                return false;

            return true;
        }

        public bool IsModifiedParentGroupingReferencingCourse(Guid parentCommonId, int courseId)
        {
            var request = CourseGroupingRequests.Where(request =>
                request.CourseGrouping!.CommonIdentifier.Equals(parentCommonId) && request.RequestType.Equals(RequestType.ModificationRequest))
                .FirstOrDefault();

            if (request is null)
                return true;

            return request.CourseGrouping!.CourseIdentifiers.Any(identifier => identifier.ConcordiaCourseId.Equals(courseId));
        }

        public bool IsModifiedParentGroupingReferencingChildGrouping(Guid parentCommonId, Guid childCommonId)
        {
            var request = CourseGroupingRequests.Where(request =>
                request.CourseGrouping!.CommonIdentifier.Equals(parentCommonId) && request.RequestType.Equals(RequestType.ModificationRequest))
                .FirstOrDefault();

            if (request is null)
                return true;

            return request.CourseGrouping!.SubGroupings.Any(subgrouping => subgrouping.CommonIdentifier.Equals(childCommonId));
        }

        public bool IsDossierDeletingCourse(int concordiaCourseId)
        {
            var request = CourseDeletionRequests.Where(request => request.Course!.CourseID.Equals(concordiaCourseId)).FirstOrDefault();

            if (request is null)
                return false;

            return true;
        }

        public bool IsDossierDeletingGrouping(Guid commonIdentifier)
        {
            var request = CourseGroupingRequests.Where(request => 
                request.CourseGrouping!.CommonIdentifier.Equals(commonIdentifier) && request.RequestType.Equals(RequestType.DeletionRequest))
                .FirstOrDefault();

            if (request is null)
                return false;

            return true;
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

