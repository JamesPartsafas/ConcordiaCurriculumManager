using ConcordiaCurriculumManager.DTO.Courses;
using ConcordiaCurriculumManager.DTO.Dossiers;
using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Curriculum.Dossier;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories;

namespace ConcordiaCurriculumManager.Services;

public interface ICourseService
{
    public IEnumerable<CourseCareerDTO> GetAllCourseCareers();
    public IEnumerable<CourseComponentDTO> GetAllCourseComponents();
    public Task<IEnumerable<string>> GetAllCourseSubjects();
    public Task<CourseCreationDossier> InitiateCourseCreation(CourseCreationInitiationDTO initiation, User user);
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

    public async Task<CourseCreationDossier> InitiateCourseCreation(CourseCreationInitiationDTO initiation, User user)
    {
        var exists = await _courseRepository.GetCourseBySubjectAndCatalog(initiation.Subject, initiation.Catalog) is not null;
        if (exists)
        {
            throw new ArgumentException("The course already exists");
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
            _logger.LogWarning($"Error inserting ${typeof(Course)} ${course.Id} by {typeof(User)} ${user.Id}");
            throw new Exception("Error registering the course");
        }
        _logger.LogInformation($"Inserted ${typeof(Course)} ${course.Id} by {typeof(User)} ${user.Id}");

        var dossier = new CourseCreationDossier { Id = new Guid(), InitiatorId = user.Id, NewCourseId = course.Id };
        bool dossierCreated = await _dossierRepository.SaveCourseCreationDossier(dossier);
        if (!dossierCreated)
        {
            _logger.LogWarning($"Error creating ${typeof(CourseCreationDossier)} ${dossier.Id}");
            throw new Exception("Error creating the dossier");
        }
        _logger.LogInformation($"Created ${typeof(CourseCreationDossier)} ${dossier.Id}");

        return dossier;
    }
}
