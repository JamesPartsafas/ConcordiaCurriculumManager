using ConcordiaCurriculumManager.DTO.Courses;
using ConcordiaCurriculumManager.DTO.Dossiers;
using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories;

namespace ConcordiaCurriculumManager.Services;

public interface ICourseService
{
    public IEnumerable<CourseCareerDTO> GetAllCourseCareers();
    public IEnumerable<CourseComponentDTO> GetAllCourseComponents();
    public Task<IEnumerable<string>> GetAllCourseSubjects();
    public Task<CourseCreationRequest> InitiateCourseCreation(CourseCreationInitiationDTO initiation, Guid userId);
    public Task<CourseModificationRequest> InitiateCourseModification(CourseModificationInitiationDTO modification, Guid userId);
}

public class CourseService : ICourseService
{
    private readonly ILogger<CourseService> _logger;
    private readonly ICourseRepository _courseRepository;
    private readonly IDossierRepository _dossierRepository;

    public CourseService(ILogger<CourseService> logger, ICourseRepository courseRepository, IDossierRepository dossierRepository)
    {
        _logger = logger;
        _courseRepository = courseRepository;
        _dossierRepository = dossierRepository;
    }

    public IEnumerable<CourseCareerDTO> GetAllCourseCareers()
    {
        var list = new List<CourseCareerDTO>();
        foreach (int career in Enum.GetValues(typeof(CourseCareerEnum)))
        {
            var careerEnum = (CourseCareerEnum)career;
            list.Add(new CourseCareerDTO { CareerCode = careerEnum, CareerName = careerEnum.ToString() });
        }

        return list;
    }

    public IEnumerable<CourseComponentDTO> GetAllCourseComponents()
    {
        var componentsList = new List<CourseComponentDTO>();
        foreach (var mapping in ComponentCodeMapping.GetComponentCodeMapping().ToList())
        {
            componentsList.Add(new CourseComponentDTO { ComponentCode = mapping.Key, ComponentName = mapping.Value });
        }

        return componentsList;
    }

    public async Task<IEnumerable<string>> GetAllCourseSubjects()
    {
        return await _courseRepository.GetUniqueCourseSubjects();
    }

    public async Task<CourseCreationRequest> InitiateCourseCreation(CourseCreationInitiationDTO initiation, Guid userId)
    {
        var exists = await _courseRepository.GetCourseBySubjectAndCatalog(initiation.Subject, initiation.Catalog) is not null;
        if (exists)
        {
            throw new ArgumentException("The course already exists");
        }

        Dossier? dossier = await _dossierRepository.GetDossierByDossierId(initiation.DossierId);

        if (dossier == null)
        {
            _logger.LogWarning($"Error retrieving the dossier ${typeof(Dossier)} ${dossier?.Id}: does not exist");
            throw new Exception("Error retrieving the dossier: does not exist");
        }
        else if (dossier.InitiatorId != userId)
        {
            _logger.LogWarning($"Error retrieving the dossier ${typeof(Dossier)} ${dossier.Id}: does not belong to user ${typeof(User)} ${userId}");
            throw new Exception("Error retrieving the dossier: does not belong to the user");
        }

        int maxCourseId = await _courseRepository.GetMaxCourseId();

        var course = new Course
        {
            Id = new Guid(),
            CourseID = maxCourseId + 1,
            Subject = initiation.Subject,
            Catalog = initiation.Catalog,
            Title = initiation.Title,
            Description = initiation.Description,
            CreditValue = initiation.CreditValue,
            PreReqs = initiation.PreReqs,
            Career = initiation.Career,
            EquivalentCourses = initiation.EquivalentCourses,
            CourseState = CourseStateEnum.NewCourseProposal,
            Version = 1,
            Published = false,
            CourseComponents = (List<CourseComponent>)ComponentCodeMapping.GetComponentCodeMapping(initiation.ComponentCodes)
        };

        bool courseCreated = await _courseRepository.SaveCourse(course);
        if (!courseCreated)
        {
            _logger.LogWarning($"Error inserting ${typeof(Course)} ${course.Id} by {typeof(User)} ${userId}");
            throw new Exception("Error registering the course");
        }
        _logger.LogInformation($"Inserted ${typeof(Course)} ${course.Id} by {typeof(User)} ${userId}");

        var courseCreationRequest = new CourseCreationRequest { Id = Guid.NewGuid(), NewCourseId = course.Id, DossierId = dossier.Id };
        bool requestCreated = await _dossierRepository.SaveCourseCreationRequest(courseCreationRequest);
        if (!requestCreated)
        {
            _logger.LogWarning($"Error creating ${typeof(CourseCreationRequest)} ${courseCreationRequest.Id}");
            throw new Exception("Error creating the request");
        }
        _logger.LogInformation($"Created ${typeof(CourseCreationRequest)} ${courseCreationRequest.Id}");

        return courseCreationRequest;
    }

    public async Task<CourseModificationRequest> InitiateCourseModification(CourseModificationInitiationDTO modification, Guid userId)
    {
        var course = await _courseRepository.GetCourseByGuid(modification.Id);
        if (course == null)
        {
            throw new ArgumentException("The course does not exist.");
        }

        course.Subject = modification.Subject;
        course.Catalog = modification.Catalog;
        course.Title = modification.Title;
        course.Description = modification.Description;
        course.CreditValue = modification.CreditValue;
        course.PreReqs = modification.PreReqs;
        course.Career = modification.Career;
        course.EquivalentCourses = modification.EquivalentCourses;
        //course.CourseComponents = modification.ComponentCodes;

        Dossier? dossier = await _dossierRepository.GetDossierByDossierId(modification.DossierId);

        if (dossier == null)
        {
            _logger.LogWarning($"Error retrieving the dossier ${typeof(Dossier)} ${dossier?.Id}: does not exist");
            throw new Exception("Error retrieving the dossier: does not exist");
        }
        else if (dossier.InitiatorId != userId)
        {
            _logger.LogWarning($"Error retrieving the dossier ${typeof(Dossier)} ${dossier.Id}: does not belong to user ${typeof(User)} ${userId}");
            throw new Exception("Error retrieving the dossier: does not belong to the user");
        }

        bool courseModified = await _courseRepository.UpdateCourse(course);
        if (!courseModified)
        {
            _logger.LogWarning($"Error updating ${typeof(Course)} ${course.Id} by {typeof(User)} ${userId}");
            throw new Exception("Error updating the course");
        }
        _logger.LogInformation($"Inserted ${typeof(Course)} ${course.Id} by {typeof(User)} ${userId}");

        var courseModificationRequest = new CourseModificationRequest { Id = Guid.NewGuid(), CourseId  = course.Id, DossierId = dossier.Id};
        bool requestCreated = await _dossierRepository.SaveCourseModificationRequest(courseModificationRequest);
        if (!requestCreated)
        {
            _logger.LogWarning($"Error creating ${typeof(CourseCreationRequest)} ${courseModificationRequest.Id}");
            throw new Exception("Error creating the request");
        }
        _logger.LogInformation($"Created ${typeof(CourseCreationRequest)} ${courseModificationRequest.Id}");

        return courseModificationRequest;
    }
}
