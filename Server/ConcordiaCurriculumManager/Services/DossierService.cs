using ConcordiaCurriculumManager.DTO.Courses;
using ConcordiaCurriculumManager.DTO.Dossiers;
using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Repositories;
using System;
using System.Security.Claims;

namespace ConcordiaCurriculumManager.Services;

public interface IDossierService
{
    public Task<List<Dossier>> GetDossiersByID(Guid ID);
}

public class DossierService : IDossierService
{
    private readonly ILogger<DossierService> _logger;
    private readonly IDossierRepository _dossierRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserAuthenticationService _userAuthenticationService;
    private readonly IUserRepository _userRepository;

    public DossierService(ILogger<DossierService> logger, IDossierRepository dossierRepository, IHttpContextAccessor httpContextAccessor, IUserAuthenticationService userAuthenticationService, IUserRepository userRepository)
    {
        _logger = logger;
        _dossierRepository = dossierRepository;
        _httpContextAccessor = httpContextAccessor;
        _userAuthenticationService = userAuthenticationService;
        _userRepository = userRepository;
    }

    public async Task<List<Dossier>> GetDossiersByID(Guid ID)
    { 
        return await _dossierRepository.GetDossiersByID(ID);
    }
}

