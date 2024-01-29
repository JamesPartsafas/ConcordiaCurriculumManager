using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests;
using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.CourseGroupingRequests;
using ConcordiaCurriculumManager.Models.Curriculum.CourseGroupings;
using NpgsqlTypes;

namespace ConcordiaCurriculumManager.Models.Curriculum.Dossiers;

public class CourseGroupingRequest : CourseRequest
{
    public required Guid CourseGroupingId { get; set; }

    public CourseGrouping? CourseGrouping { get; set; }

    public required RequestType RequestType { get; set; }

    public static CourseGroupingRequest CreateCourseGroupingCreationRequestFromDTO(CourseGroupingCreationRequestDTO dto) =>
        CreateCourseGroupingRequestFromDTO(
            dto,
            CourseGrouping.CreateCourseGroupingFromCreationDTO(Guid.NewGuid(), dto.CourseGrouping),
            RequestType.CreationRequest
         );

    public static CourseGroupingRequest CreateCourseGroupingModificationRequestFromDTO(CourseGroupingModificationRequestDTO dto) =>
        CreateCourseGroupingRequestFromDTO(
            dto,
            CourseGrouping.CreateCourseGroupingFromModificationDTO(Guid.NewGuid(), dto.CourseGrouping),
            RequestType.ModificationRequest
         );

    private static CourseGroupingRequest CreateCourseGroupingRequestFromDTO(CourseInitiationDTO dto, CourseGrouping courseGrouping, RequestType requestType)
    {
        return new CourseGroupingRequest
        {
            Id = Guid.NewGuid(),
            DossierId = dto.DossierId,
            Rationale = dto.Rationale,
            ResourceImplication = dto.ResourceImplication,
            Comment = dto.Comment,
            Conflict = "No conflict", // TODO: Implement correct conflict detection for groupings
            CourseGroupingId = courseGrouping.Id,
            CourseGrouping = courseGrouping,
            RequestType = requestType
        };
    }
}

public enum RequestType
{
    [PgName(nameof(CreationRequest))]
    CreationRequest,

    [PgName(nameof(ModificationRequest))]
    ModificationRequest,

    [PgName(nameof(DeletionRequestRequest))]
    DeletionRequestRequest
}
