using ConcordiaCurriculumManager.DTO.Dossiers;
using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories;

namespace ConcordiaCurriculumManager.Services;
public interface IDossierService
{
    public Task<List<Dossier>> GetDossiersByID(Guid ID);
    public Task<Dossier> CreateDossierForUser(CreateDossierDTO dossier, User user);
    public Task<Dossier> EditDossier(EditDossierDTO dossier, Guid dossierId);
    public Task DeleteDossier(Guid dossierId);
    public Task<Dossier?> GetDossierDetailsById(Guid id);
    public Task<Dossier> GetDossierDetailsByIdOrThrow(Guid id);
    public Task<Dossier> GetDossierForUserOrThrow(Guid dossierId, Guid userId);
    public Task SaveCourseCreationRequest(CourseCreationRequest courseCreationRequest);
    public Task SaveCourseModificationRequest(CourseModificationRequest courseModificationRequest);
    public Task SaveCourseDeletionRequest(CourseDeletionRequest courseDeletionRequest);
    public Task<CourseCreationRequest> GetCourseCreationRequest(Guid courseRequestId);
    public Task<CourseModificationRequest> GetCourseModificationRequest(Guid courseRequestId);
}

public class DossierService : IDossierService
{
    private readonly ILogger<DossierService> _logger;
    private readonly IDossierRepository _dossierRepository;

    public DossierService(ILogger<DossierService> logger, IDossierRepository dossierRepository)
    {
        _logger = logger;
        _dossierRepository = dossierRepository;
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

    public async Task<Dossier> CreateDossierForUser(CreateDossierDTO d, User user) {
        var dossier = new Dossier
        {
            Id = Guid.NewGuid(),
            InitiatorId = user.Id,
            Title = d.Title,
            Description = d.Description,
            Published = false
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

    public async Task<Dossier> GetDossierForUserOrThrow(Guid dossierId, Guid userId)
    {
        var dossier = await GetDossierDetailsById(dossierId) ?? throw new ArgumentException("The dossier does not exist.");
        if (dossier.InitiatorId != userId)
        {
            throw new BadRequestException($"Error retrieving the dossier {typeof(Dossier)} {dossier.Id}: does not belong to the user");
        }

        return dossier;
    }

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
}
