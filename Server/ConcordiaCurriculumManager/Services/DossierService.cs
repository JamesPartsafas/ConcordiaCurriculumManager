using ConcordiaCurriculumManager.DTO.Dossiers;
using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories;
using NetTopologySuite.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ConcordiaCurriculumManager.Models.Curriculum.CourseGroupings;
using System.Linq;

namespace ConcordiaCurriculumManager.Services;
public interface IDossierService
{
    public Task<List<Dossier>> GetDossiersByID(Guid ID);
    public Task<Dossier> CreateDossierForUser(CreateDossierDTO dossier, User user);
    public Task<Dossier> EditDossier(EditDossierDTO dossier, Guid dossierId);
    public Task DeleteDossier(Guid dossierId);
    public Task<Dossier?> GetDossierDetailsById(Guid id);
    public Task<Dossier> GetDossierDetailsByIdOrThrow(Guid id);
    public Task SaveCourseCreationRequest(CourseCreationRequest courseCreationRequest);
    public Task SaveCourseModificationRequest(CourseModificationRequest courseModificationRequest);
    public Task SaveCourseDeletionRequest(CourseDeletionRequest courseDeletionRequest);
    public Task<CourseCreationRequest> GetCourseCreationRequest(Guid courseRequestId);
    public Task<CourseModificationRequest> GetCourseModificationRequest(Guid courseRequestId);
    public Task<CourseDeletionRequest> GetCourseDeletionRequest(Guid courseRequestId);
    public Task<IList<User>> GetCurrentlyReviewingGroupMasters(Guid dossierId);
    public Task<DossierReport> GetDossierReportByDossierId(Guid dossierId);
    public Task<IList<Dossier>> GetDossiersRequiredReview(Guid userId);
    public Task<CourseChanges> GetChangesAcrossAllDossiers();
    public Task<IList<Dossier>> SearchDossiers(string? title, DossierStateEnum? state, Guid? groupId);
    public Task VerifyDossierStateIsValid(Dossier dossier);
}

public class DossierService : IDossierService
{
    private readonly ILogger<DossierService> _logger;
    private readonly IDossierRepository _dossierRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly ICourseGroupingRepository _courseGroupingRepository;
    private readonly ICourseGroupingService _courseGroupingService;

    public DossierService(
        ILogger<DossierService> logger,
        IDossierRepository dossierRepository,
        ICourseRepository courseRepository,
        ICourseGroupingRepository courseGroupingRepository,
        ICourseGroupingService courseGroupingService)
    {
        _logger = logger;
        _dossierRepository = dossierRepository;
        _courseRepository = courseRepository;
        _courseGroupingRepository = courseGroupingRepository;
        _courseGroupingService = courseGroupingService;
    }

    public async Task<List<Dossier>> GetDossiersByID(Guid ID)
    {
        var dossiers = await _dossierRepository.GetDossiersByID(ID);

        if (dossiers.Count == 0)
        {
            throw new NotFoundException($"Dossiers with Id {ID} does not exists");
        }

        return dossiers;
    }

    public async Task<Dossier> CreateDossierForUser(CreateDossierDTO d, User user)
    {
        var dossierId = Guid.NewGuid();
        var dossier = new Dossier
        {
            Id = dossierId,
            InitiatorId = user.Id,
            Title = d.Title,
            Description = d.Description,
            State = DossierStateEnum.Created,
            Discussion = new()
            {
                DossierId = dossierId
            }
        };

        bool dossierCreated = await _dossierRepository.SaveDossier(dossier);
        if (!dossierCreated)
        {
            throw new Exception($"Error creating ${typeof(Dossier)} ${dossier.Id}");
        }

        _logger.LogInformation($"Created {typeof(Dossier)} {dossier.Id}");
        return dossier;
    }

    public async Task<Dossier> EditDossier(EditDossierDTO d, Guid dossierId)
    {
        var dossier = await _dossierRepository.GetDossierByDossierId(dossierId) ?? throw new ArgumentException("The dossier does not exist.");
        dossier.Title = d.Title;
        dossier.Description = d.Description;

        bool editedDossier = await _dossierRepository.UpdateDossier(dossier);
        if (!editedDossier)
        {
            throw new Exception($"Error editing {typeof(Dossier)} {dossier.Id}");
        }

        _logger.LogInformation($"Edited {typeof(Dossier)} {dossier.Id}");
        return dossier;
    }


    public async Task DeleteDossier(Guid dossierId)
    {
        var dossier = await _dossierRepository.GetDossierByDossierId(dossierId) ?? throw new ArgumentException("The dossier does not exist.");
        bool editedDossier = await _dossierRepository.DeleteDossier(dossier);
        if (!editedDossier)
        {
            throw new Exception($"Error deleting {typeof(Dossier)} {dossier.Id}");
        }

        _logger.LogInformation($"Deleted {typeof(Dossier)} {dossier.Id}");
    }

    public async Task<Dossier?> GetDossierDetailsById(Guid id) => await _dossierRepository.GetDossierByDossierId(id);

    public async Task<Dossier> GetDossierDetailsByIdOrThrow(Guid id) => await _dossierRepository.GetDossierByDossierId(id)
        ?? throw new NotFoundException("The dossier does not exist.");

    public async Task SaveCourseCreationRequest(CourseCreationRequest courseCreationRequest)
    {
        bool requestCreated = await _dossierRepository.SaveCourseCreationRequest(courseCreationRequest);
        if (!requestCreated)
        {
            throw new Exception($"Error creating {typeof(CourseCreationRequest)} {courseCreationRequest.Id}");
        }

        _logger.LogInformation($"Created {typeof(CourseCreationRequest)} {courseCreationRequest.Id}");
    }

    public async Task SaveCourseModificationRequest(CourseModificationRequest courseModificationRequest)
    {
        bool requestCreated = await _dossierRepository.SaveCourseModificationRequest(courseModificationRequest);
        if (!requestCreated)
        {
            throw new Exception($"Error creating {typeof(CourseModificationRequest)} {courseModificationRequest.Id}");
        }

        _logger.LogInformation($"Created {typeof(CourseModificationRequest)} {courseModificationRequest.Id}");
    }

    public async Task SaveCourseDeletionRequest(CourseDeletionRequest courseDeletionRequest)
    {
        bool requestCreated = await _dossierRepository.SaveCourseDeletionRequest(courseDeletionRequest);
        if (!requestCreated)
        {
            throw new Exception($"Error creating {typeof(CourseDeletionRequest)} {courseDeletionRequest.Id}");
        }

        _logger.LogInformation($"Created {typeof(CourseDeletionRequest)} {courseDeletionRequest.Id}");
    }

    public async Task<CourseCreationRequest> GetCourseCreationRequest(Guid courseRequestId)
    {
        var courseCreationRequest = await _dossierRepository.GetCourseCreationRequest(courseRequestId) ?? throw new NotFoundException($"Error retrieving the course creation request with id {typeof(CourseCreationRequest)} {courseRequestId}");
        return courseCreationRequest;
    }

    public async Task<CourseModificationRequest> GetCourseModificationRequest(Guid courseRequestId)
    {
        var courseModificationRequest = await _dossierRepository.GetCourseModificationRequest(courseRequestId) ?? throw new NotFoundException($"Error retrieving the course modification request with id ${typeof(CourseModificationRequest)} ${courseRequestId}");
        return courseModificationRequest;
    }

    public async Task<CourseDeletionRequest> GetCourseDeletionRequest(Guid courseRequestId)
    {
        var courseDeletionRequest = await _dossierRepository.GetCourseDeletionRequest(courseRequestId) ?? throw new NotFoundException($"Error retrieving the course deletion request with id ${typeof(CourseDeletionRequest)} ${courseRequestId}");
        return courseDeletionRequest;
    }

    public async Task<IList<User>> GetCurrentlyReviewingGroupMasters(Guid dossierId)
    {
        return await _dossierRepository.GetCurrentlyReviewingGroupMasters(dossierId);
    }

    public async Task<DossierReport> GetDossierReportByDossierId(Guid dossierId)
    {
        var dossier = await _dossierRepository.GetDossierReportByDossierId(dossierId) ?? throw new NotFoundException("The dossier does not exist.");
        var oldCourses = new List<Course>();
        var oldGroupings = new List<CourseGrouping>();

        foreach (var request in dossier.CourseModificationRequests)
        {
            var course = await _courseRepository.GetCourseWithSupportingFilesBySubjectAndCatalog(request.Course!.Subject, request.Course.Catalog);
            if (course == null) continue;
            oldCourses.Add(course);
        }

        foreach (var request in dossier.CourseGroupingRequests)
        {
            await _courseGroupingService.QueryRelatedCourseGroupingData(request.CourseGrouping!, false);

            if (!request.IsModificationRequest()) continue;

            CourseGrouping grouping;
            Guid commonId = request.CourseGrouping!.CommonIdentifier;
            try
            {
                grouping = await _courseGroupingService.GetCourseGroupingByCommonIdentifier(commonId, false);
            }
            catch (NotFoundException e)
            {
                _logger.LogWarning($"Course grouping with common ID {commonId} not found in dossier report: {e.ToString()}");
                continue;
            }

            oldGroupings.Add(grouping);
        }

        return new DossierReport { Dossier = dossier, OldCourses = oldCourses, OldGroupings = oldGroupings };
    }

    public async Task<IList<Dossier>> GetDossiersRequiredReview(Guid userId)
    {
        return await _dossierRepository.GetDossiersRequiredReview(userId);
    }

    public async Task<CourseChanges> GetChangesAcrossAllDossiers()
    {
        var courses = await _dossierRepository.GetChangesAcrossAllDossiers();
        var oldCourses = new List<Course>();

        foreach (var course in courses)
        {
            if (course.CourseModificationRequest is null) continue;
            var oldCourse = await _courseRepository.GetPublishedVersion(course.Subject, course.Catalog);
            if (oldCourse == null) continue;
            oldCourses.Add(oldCourse);
        }
        var courseCreationRequests = new List<CourseCreationRequest>();
        var courseModificationRequests = new List<CourseModificationRequest>();
        var courseDeletionRequests = new List<CourseDeletionRequest>();

        foreach (var course in courses)
        {
            if (course.CourseCreationRequest is not null)
            {
                var request = course.CourseCreationRequest;
                course.CourseCreationRequest = null;
                request.NewCourse = course;
                courseCreationRequests.Add(request);
            }

            if (course.CourseModificationRequest is not null)
            {
                var request = course.CourseModificationRequest;
                course.CourseModificationRequest = null;
                request.Course = course;
                courseModificationRequests.Add(request);
            }

            if (course.CourseDeletionRequest is not null)
            {
                var request = course.CourseDeletionRequest;
                course.CourseDeletionRequest = null;
                request.Course = course;
                courseDeletionRequests.Add(request);
            }
        }

        return new CourseChanges
        {
            CourseCreationRequests = courseCreationRequests,
            CourseModificationRequests = courseModificationRequests,
            CourseDeletionRequests = courseDeletionRequests,
            OldCourses = oldCourses
        };
    }

    public async Task<IList<Dossier>> SearchDossiers(string? title, DossierStateEnum? state, Guid? groupId)
    {
        if (state != null && !state.Equals(DossierStateEnum.InReview) && groupId is not null)
        {
            throw new InvalidInputException("A dossier cannot be in an approval stage if its state is not under review.");
        }
        return await _dossierRepository.SearchDossiers(title, state, groupId);
    }

    /// <summary>
    /// Validity implies:
    /// 1: For any grouping creation or modification request,
    /// all its subgroupings and courses either have their current version be accepted or are being created in the dossier.
    /// In addition, the dossier must not have any deletion request for any of its subgroupings or courses.
    /// 2: For any grouping deletion request, no grouping may have this grouping as a subgrouping, unless this parent grouping
    /// is being modified or deleted in the dossier to remove this subgrouping reference.
    /// 3: For any course deletion request, no grouping may reference this course, unless the grouping is being deleted or modified within
    /// the dossier to remove this course reference.
    /// </summary>
    /// <param name="dossier"></param>
    public async Task VerifyDossierStateIsValid(Dossier dossier)
    {
        foreach (var groupingRequest in dossier.CourseGroupingRequests)
        {
            await VerifyGroupingRequestIsValid(dossier, groupingRequest);
        }

        foreach (var courseDeletionRequest in dossier.CourseDeletionRequests)
        {
            await VerifyCourseDeletionRequestIsValid(dossier, courseDeletionRequest);
        }
    }

    private async Task VerifyGroupingRequestIsValid(Dossier dossier, CourseGroupingRequest request)
    {
        var grouping = request.CourseGrouping;
        if (grouping == null)
            return;

        if (request.RequestType.Equals(RequestType.CreationRequest) || request.RequestType.Equals(RequestType.ModificationRequest))
        {
            await VerifyGroupingCreationAndModificationRequestIsValid(dossier, grouping);
        }
        else
        {
            await VerifyGroupingDeletionRequestIsValid(dossier, grouping);
        }
    }

    private async Task VerifyGroupingCreationAndModificationRequestIsValid(Dossier dossier, CourseGrouping grouping)
    {
        VerifyNoInvalidDeletions(dossier, grouping);

        await VerifySubComponentsExist(dossier, grouping);
    }

    private void VerifyNoInvalidDeletions(Dossier dossier, CourseGrouping grouping)
    {
        // Verify no subgroupings are being deleted
        foreach (var subgrouping in grouping.SubGroupings)
        {
            if (dossier.IsDossierDeletingGrouping(subgrouping.CommonIdentifier))
                throw new BadRequestException($"The grouping {grouping.Name} and a deletion request for its subgrouping cannot exist in the same dossier");
        }

        // Verify no courses are being deleted from grouping
        foreach (var courseIdentifier in grouping.CourseIdentifiers)
        {
            if (dossier.IsDossierDeletingCourse(courseIdentifier.ConcordiaCourseId))
                throw new BadRequestException($"The grouping {grouping.Name} and a deletion request for its course cannot exist in the same dossier");
        }
    }

    private async Task VerifySubComponentsExist(Dossier dossier, CourseGrouping grouping)
    {
        // Verify subgroupings are being created or are already accepted
        foreach (var subgrouping in grouping.SubGroupings)
        {
            // Check if subgrouping is being created in dossier
            if (dossier.IsDossierCreatingGrouping(subgrouping.CommonIdentifier))
                continue;

            // Subgrouping is not being created in dossier, so check if subgrouping is already accepted.
            var queriedSubgrouping = await _courseGroupingRepository.GetCourseGroupingByCommonIdentifier(subgrouping.CommonIdentifier);
            if (queriedSubgrouping != null)
                continue;

            throw new BadRequestException($"The grouping {grouping.Name} references a subgrouping that does not exist");
        }

        // Verify courses in grouping are being created or are already accepted
        foreach (var courseIdentifier in grouping.CourseIdentifiers)
        {
            // Check if course is being created in dossier
            if (dossier.IsDossierCreatingCourse(courseIdentifier.ConcordiaCourseId))
                continue;

            // Course is not being created in dossier, so check if course is already accepted.
            var queriedCourse = await _courseRepository.GetCourseByCourseIdAndLatestVersion(courseIdentifier.ConcordiaCourseId);
            if (queriedCourse != null)
                continue;

            throw new BadRequestException($"The grouping {grouping.Name} references a course that does not exist");
        }
    }

    private async Task VerifyGroupingDeletionRequestIsValid(Dossier dossier, CourseGrouping grouping)
    {
        // Verify no parent groupings reference the grouping to be deleted, unless the parent is itself being deleted or modified
        var parentGroupings = await _courseGroupingRepository.GetCourseGroupingsContainingSubgrouping(grouping);
        foreach (var parentGrouping in parentGroupings)
        {
            if (dossier.IsDossierDeletingGrouping(parentGrouping.CommonIdentifier))
                continue;

            if (!dossier.IsModifiedParentGroupingReferencingChildGrouping(parentGrouping.CommonIdentifier, grouping.CommonIdentifier))
                continue;

            throw new BadRequestException($"The grouping {grouping.Name} cannot be deleted while its parent grouping {parentGrouping.Name} still references it");
        }
    }

    private async Task VerifyCourseDeletionRequestIsValid(Dossier dossier, CourseDeletionRequest request)
    {
        // Verify no parent groupings reference the course to be deleted, unless the parent is itself being deleted or modified
        var parentGroupings = await _courseGroupingRepository.GetCourseGroupingsContainingCourse(request.Course!);
        foreach (var parentGrouping in parentGroupings)
        {
            if (dossier.IsDossierDeletingGrouping(parentGrouping.CommonIdentifier))
                continue;

            if (!dossier.IsModifiedParentGroupingReferencingCourse(parentGrouping.CommonIdentifier, request.Course!.CourseID))
                continue;

            throw new BadRequestException($"The course {request.Course!.Title} cannot be deleted while its parent grouping {parentGrouping.Name} still references it");
        }
    }
}
