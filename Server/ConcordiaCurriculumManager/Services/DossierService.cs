using ConcordiaCurriculumManager.DTO.Dossiers;
using ConcordiaCurriculumManager.Models.Curriculum;
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
    public Task<Dossier> EditDossier(EditDossierDTO dossier, User user);
    public Task DeleteDossier(Guid ID, User user);
    public Task<Dossier> GetDossierDetailsById(Guid id);
    public Task<Dossier> GetDossierForUserOrThrow(Guid dossierId, Guid userId);
    public Task SaveCourseCreationRequest(CourseCreationRequest courseCreationRequest);
    public Task SaveCourseModificationRequest(CourseModificationRequest courseModificationRequest);
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
        return await _dossierRepository.GetDossiersByID(ID);
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
            _logger.LogWarning($"Error creating ${typeof(Dossier)} ${dossier.Id}");
            throw new Exception("Error creating the dossier");
        }
        _logger.LogInformation($"Created ${typeof(Dossier)} ${dossier.Id}");

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
            _logger.LogWarning($"Error editing ${typeof(Dossier)} ${dossier.Id}");
            throw new Exception("Error editing the dossier");
        }
        
        _logger.LogInformation($"Edited ${typeof(Dossier)} ${dossier.Id}");
        return dossier;
    }


    public async Task DeleteDossier(Guid dossierId)
    {
        var dossier = await _dossierRepository.GetDossierByDossierId(dossierId) ?? throw new ArgumentException("The dossier does not exist.");
        bool editedDossier = await _dossierRepository.DeleteDossier(dossier);
        if (!editedDossier)
        {
            _logger.LogWarning($"Error deleting ${typeof(Dossier)} ${dossier.Id}");
            throw new Exception("Error deleting the dossier");
        }
        _logger.LogInformation($"Deleted ${typeof(Dossier)} ${dossier.Id}");
    }

    public async Task<Dossier> GetDossierDetailsById(Guid id)
    {
        var dossierDetails = await _dossierRepository.GetDossierByDossierId(id);
        if (dossierDetails == null)
        {
            _logger.LogWarning($"Error retrieving the dossier ${typeof(Dossier)} ${id}: does not exist");
            throw new ArgumentException("Dossier could not be found.");
        }

        return dossierDetails;
    }

    public async Task<Dossier> GetDossierForUserOrThrow(Guid dossierId, Guid userId)
    {
        var dossier = await GetDossierDetailsById(dossierId);
        if (dossier.InitiatorId != userId)
        {
            _logger.LogWarning($"Error retrieving the dossier ${typeof(Dossier)} ${dossier.Id}: does not belong to user ${typeof(User)} ${userId}");
            throw new Exception("Error retrieving the dossier: does not belong to the user");
        }

        return dossier;
    }

    public async Task SaveCourseCreationRequest(CourseCreationRequest courseCreationRequest)
    {
        bool requestCreated = await _dossierRepository.SaveCourseCreationRequest(courseCreationRequest);
        if (!requestCreated)
        {
            _logger.LogWarning($"Error creating ${typeof(CourseCreationRequest)} ${courseCreationRequest.Id}");
            throw new Exception("Error creating the request");
        }
        _logger.LogInformation($"Created ${typeof(CourseCreationRequest)} ${courseCreationRequest.Id}");
    }

    public async Task SaveCourseModificationRequest(CourseModificationRequest courseModificationRequest)
    {
        bool requestCreated = await _dossierRepository.SaveCourseModificationRequest(courseModificationRequest);
        if (!requestCreated)
        {
            _logger.LogWarning($"Error creating ${typeof(CourseModificationRequest)} ${courseModificationRequest.Id}");
            throw new Exception("Error creating the request");
        }
        _logger.LogInformation($"Created ${typeof(CourseModificationRequest)} ${courseModificationRequest.Id}");
    }
    public async Task<Dossier?> GetDossierDetailsById(Guid id) => await _dossierRepository.GetDossierByDossierId(id);
}

