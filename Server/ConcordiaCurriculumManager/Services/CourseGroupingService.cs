using ConcordiaCurriculumManager.DTO.CourseGrouping;
using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.CourseGroupingRequests;
using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManager.Middleware.Exceptions;
using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Curriculum.CourseGroupings;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Repositories;
using System.Linq;

namespace ConcordiaCurriculumManager.Services;

public interface ICourseGroupingService
{
    public Task<CourseGrouping> GetCourseGrouping(Guid groupingId);
    public Task<CourseGrouping> GetCourseGroupingByCommonIdentifier(Guid commonId);
    public Task<ICollection<CourseGrouping>> GetCourseGroupingsBySchoolNonRecursive(SchoolEnum school);
    public Task<ICollection<CourseGrouping>> GetCourseGroupingsLikeName(string name);
    public Task<CourseGroupingRequest> InitiateCourseGroupingCreation(CourseGroupingCreationRequestDTO dto);
    public Task<CourseGroupingRequest> InitiateCourseGroupingModification(CourseGroupingModificationRequestDTO dto);
    public Task<CourseGroupingRequest> InitiateCourseGroupingDeletion(CourseGroupingModificationRequestDTO dto);
    public Task<CourseGroupingRequest> EditCourseGroupingCreation(Guid originalRequestId, CourseGroupingCreationRequestDTO dto);
    public Task<CourseGroupingRequest> EditCourseGroupingModification(Guid originalRequestId, CourseGroupingModificationRequestDTO dto);
    public Task<CourseGroupingRequest> EditCourseGroupingDeletion(Guid originalRequestId, CourseGroupingModificationRequestDTO dto);
    public Task DeleteCourseGroupingRequest(Guid dossierId, Guid requestId);
    public Task<CourseGrouping> PublishCourseGrouping(Guid commonIdentifier);
    public Task<IDictionary<Guid, int>> GetGroupingVersions(Dossier dossier);
}

public class CourseGroupingService : ICourseGroupingService
{
    private readonly ILogger<CourseGroupingService> _logger;
    private readonly ICourseRepository _courseRepository;
    private readonly ICourseGroupingRepository _courseGroupingRepository;
    private readonly IDossierService _dossierService;

    public CourseGroupingService(
        ILogger<CourseGroupingService> logger,
        ICourseRepository courseRepository,
        ICourseGroupingRepository courseGroupingRepository,
        IDossierService dossierService)
    {
        _logger = logger;
        _courseRepository = courseRepository;
        _courseGroupingRepository = courseGroupingRepository;
        _dossierService = dossierService;
    }

    public async Task<CourseGrouping> GetCourseGrouping(Guid groupingId)
    {
        CourseGrouping grouping = await _courseGroupingRepository.GetCourseGroupingById(groupingId)
            ?? throw new NotFoundException($"The course grouping with ID {groupingId} was not found.");

        await QueryRelatedCourseGroupingData(grouping);

        return grouping;
    }

    public async Task<CourseGrouping> GetCourseGroupingByCommonIdentifier(Guid commonId)
    {
        CourseGrouping grouping = await _courseGroupingRepository.GetCourseGroupingByCommonIdentifier(commonId)
            ?? throw new NotFoundException($"The course grouping with common ID {commonId} was not found.");

        await QueryRelatedCourseGroupingData(grouping);

        return grouping;
    }

    public async Task<ICollection<CourseGrouping>> GetCourseGroupingsBySchoolNonRecursive(SchoolEnum school) =>
        await _courseGroupingRepository.GetCourseGroupingsBySchool(school);

    public async Task<ICollection<CourseGrouping>> GetCourseGroupingsLikeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidInputException("The course grouping name cannot be empty.");
        }

        return await _courseGroupingRepository.GetCourseGroupingsLikeName(name);
    }

    private async Task QueryRelatedCourseGroupingData(CourseGrouping grouping)
    {
        grouping.Courses = await GetCoursesFromIdentifiers(grouping.CourseIdentifiers);

        foreach (var subGroupingReference in grouping.SubGroupingReferences)
        {
            var subGrouping = await GetCourseGroupingByCommonIdentifier(subGroupingReference.ChildGroupCommonIdentifier);
            grouping.SubGroupings.Add(subGrouping);
        }
    }

    private async Task<IList<Course>> GetCoursesFromIdentifiers(ICollection<CourseIdentifier> identifiers)
    {
        IList<int> courseIds = identifiers.Select(id => id.ConcordiaCourseId).ToList();

        return await _courseRepository.GetCoursesByConcordiaCourseIds(courseIds);
    }


    public async Task<CourseGroupingRequest> InitiateCourseGroupingCreation(CourseGroupingCreationRequestDTO dto)
    {
        var dossier = await _dossierService.GetDossierDetailsByIdOrThrow(dto.DossierId);

        var grouping = dossier.CreateCourseGroupingCreationRequest(dto);

        var groupingSaved = await _courseGroupingRepository.SaveCourseGroupingRequest(grouping);

        if (groupingSaved)
            _logger.LogInformation($"New course grouping creation created for dossier {dossier.Id} with Id {grouping.Id}");
        else
        {
            _logger.LogError($"New course grouping creation for dossier {dossier.Id} failed to save");
            throw new ServiceUnavailableException("The course grouping creation could not be saved");
        }

        return grouping;
    }

    public async Task<CourseGroupingRequest> InitiateCourseGroupingModification(CourseGroupingModificationRequestDTO dto)
    {
        await VerifyCourseGroupingExists(dto.CourseGrouping);

        var dossier = await _dossierService.GetDossierDetailsByIdOrThrow(dto.DossierId);

        var grouping = dossier.CreateCourseGroupingModificationRequest(dto);

        var groupingSaved = await _courseGroupingRepository.SaveCourseGroupingRequest(grouping);

        if (groupingSaved)
            _logger.LogInformation($"New course grouping modification created for dossier {dossier.Id} with Id {grouping.Id}");
        else
        {
            _logger.LogError($"New course grouping modification for dossier {dossier.Id} failed to save");
            throw new ServiceUnavailableException("The course grouping modification could not be saved");
        }

        return grouping;
    }

    public async Task<CourseGroupingRequest> InitiateCourseGroupingDeletion(CourseGroupingModificationRequestDTO dto)
    {
        await VerifyCourseGroupingExists(dto.CourseGrouping);

        var dossier = await _dossierService.GetDossierDetailsByIdOrThrow(dto.DossierId);

        var grouping = dossier.CreateCourseGroupingDeletionRequest(dto);

        var groupingSaved = await _courseGroupingRepository.SaveCourseGroupingRequest(grouping);

        if (groupingSaved)
            _logger.LogInformation($"New course grouping deletion created for dossier {dossier.Id} with Id {grouping.Id}");
        else
        {
            _logger.LogError($"New course grouping deletion for dossier {dossier.Id} failed to save");
            throw new ServiceUnavailableException("The course grouping deletion could not be saved");
        }

        return grouping;
    }

    private async Task VerifyCourseGroupingExists(CourseGroupingModificationInputDTO dto)
    {
        var grouping = await _courseGroupingRepository.GetCourseGroupingByCommonIdentifier(dto.CommonIdentifier);
        if (grouping is null)
            throw new BadRequestException($"The course grouping with the identifier {dto.CommonIdentifier} does not exist");
    }

    public async Task<CourseGroupingRequest> EditCourseGroupingCreation(Guid originalRequestId, CourseGroupingCreationRequestDTO dto)
    {
        var request = await _courseGroupingRepository.GetCourseGroupingRequestById(originalRequestId);
        if (request == null || request.CourseGrouping == null)
            throw new ServiceUnavailableException("The course grouping request could not be edited");

        await DeleteCourseGroupingRequest(dto.DossierId, originalRequestId);

        return await InitiateCourseGroupingCreation(dto);
    }

    public async Task<CourseGroupingRequest> EditCourseGroupingModification(Guid originalRequestId, CourseGroupingModificationRequestDTO dto)
    {
        await VerifyEditRequestsMatchOrThrow(originalRequestId, dto);

        await DeleteCourseGroupingRequest(dto.DossierId, originalRequestId);

        return await InitiateCourseGroupingModification(dto);
    }

    public async Task<CourseGroupingRequest> EditCourseGroupingDeletion(Guid originalRequestId, CourseGroupingModificationRequestDTO dto)
    {
        await VerifyEditRequestsMatchOrThrow(originalRequestId, dto);

        await DeleteCourseGroupingRequest(dto.DossierId, originalRequestId);

        return await InitiateCourseGroupingDeletion(dto);
    }

    private async Task VerifyEditRequestsMatchOrThrow(Guid originalRequestId, CourseGroupingModificationRequestDTO dto)
    {
        var request = await _courseGroupingRepository.GetCourseGroupingRequestById(originalRequestId);
        if (request == null || request.CourseGrouping == null)
            throw new ServiceUnavailableException("The course grouping request could not be edited");

        if (request.CourseGrouping.CommonIdentifier.Equals(dto.CourseGrouping.CommonIdentifier)
            && request.DossierId.Equals(dto.DossierId))
            return;

        throw new BadRequestException("The common identifier of the grouping to be modified does not match the one passed into the system");
    }

    public async Task DeleteCourseGroupingRequest(Guid dossierId, Guid requestId)
    {
        var dossier = await _dossierService.GetDossierDetailsByIdOrThrow(dossierId);

        var request = dossier.GetGroupingRequestForDeletion(requestId);

        var requestDeleted = await _courseGroupingRepository.DeleteCourseGroupingRequest(request);

        if (requestDeleted)
            _logger.LogInformation($"The course grouping request in dossier {dossier.Id} with Id {requestId} was deleted");
        else
        {
            _logger.LogError($"Course grouping {requestId} for dossier {dossier.Id} failed to be deleted");
            throw new ServiceUnavailableException("The course grouping could not be deleted");
        }
    }

    public async Task<CourseGrouping> PublishCourseGrouping(Guid commonIdentifier)
    {
        var newCourseGrouping = await _courseGroupingRepository.GetCourseGroupingByCommonIdentifierAnyState(commonIdentifier) ?? throw new NotFoundException($"The course grouping with common ID {commonIdentifier} was not found.");
        var oldCourseGrouping = await _courseGroupingRepository.GetPublishedVersion(commonIdentifier);

        if (newCourseGrouping.Published)
        {
            return newCourseGrouping;
        }

        await QueryRelatedCourseGroupingData(newCourseGrouping);
        newCourseGrouping.MarkAsPublished();

        if (oldCourseGrouping is not null)
        {
            oldCourseGrouping.MarkAsUnpublished();
            await _courseGroupingRepository.UpdateCourseGrouping(oldCourseGrouping);
        }
        else
        {
            _logger.LogInformation($"The course grouping with common ID {commonIdentifier} does not have an old published course grouping. This could be the first time it is published");
        }

        await _courseGroupingRepository.UpdateCourseGrouping(newCourseGrouping);
        _logger.LogInformation($"A new course grouping version {newCourseGrouping.Version} with common ID {commonIdentifier} was published.");

        return newCourseGrouping;
    }

    public async Task<IDictionary<Guid, int>> GetGroupingVersions(Dossier dossier)
    {
        var currentVersions = new Dictionary<Guid, int>();
        foreach (var groupingRequest in dossier.CourseGroupingRequests)
        {
            if (groupingRequest == null) continue;
            var grouping = groupingRequest.CourseGrouping;
            if (grouping == null) continue;

            int version = 0;

            var currentGrouping = await _courseGroupingRepository.GetCourseGroupingByCommonIdentifierAnyState(grouping.CommonIdentifier);
            if (currentGrouping != null && currentGrouping.Version != null)
                version = (int)currentGrouping.Version;

            currentVersions.Add(grouping.CommonIdentifier, version);
        }

        return currentVersions;
    }
}
