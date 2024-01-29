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
    public Task<CourseGroupingRequest> InitiateCourseGroupingModification(CourseGroupingModificationRequestDTO dto);
    public Task DeleteCourseGroupingRequest(Guid dossierId, Guid requestId);
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

    private async Task VerifyCourseGroupingExists(CourseGroupingModificationInputDTO dto)
    {
        var grouping = await _courseGroupingRepository.GetCourseGroupingByCommonIdentifier(dto.CommonIdentifier);
        if (grouping is null)
            throw new BadRequestException($"The course grouping with the identifier {dto.CommonIdentifier} does not exist");
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
}
