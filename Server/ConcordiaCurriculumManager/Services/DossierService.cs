using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Repositories;


namespace ConcordiaCurriculumManager.Services;

public interface IDossierService
{
    public Task<List<Dossier>> GetDossiersByID(Guid ID);
}

public class DossierService : IDossierService
{
    private readonly IDossierRepository _dossierRepository;

    public DossierService(IDossierRepository dossierRepository)
    {
        _dossierRepository = dossierRepository;

    }

    public async Task<List<Dossier>> GetDossiersByID(Guid ID)
    { 
        return await _dossierRepository.GetDossiersByID(ID);
    }
}

