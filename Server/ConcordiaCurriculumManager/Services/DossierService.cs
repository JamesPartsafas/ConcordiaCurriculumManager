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
    public Task<Dossier> GetDossierDetailsById(Guid id);
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

    public async Task<Dossier> GetDossierDetailsById(Guid id)
    {
        var dossierDetails = await _dossierRepository.GetDossierByDossierId(id);
        if (dossierDetails == null)
            throw new ArgumentException("Dossier could not be found.");

        return dossierDetails;
    }
}

