using ConcordiaCurriculumManager.DTO.Courses;
using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.InputDTOs;
using ConcordiaCurriculumManager.Filters.Exceptions;
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
    public Task<CourseDeletionRequest> InitiateCourseDeletion(CourseDeletionInitiationDTO deletion, Guid userId);
    public Task<Course> GetCourseDataOrThrowOnDeleted(string subject, string catalog);
    public Task<CourseCreationRequest?> EditCourseCreationRequest(EditCourseCreationRequestDTO edit);
    public Task<CourseModificationRequest?> EditCourseModificationRequest(EditCourseModificationRequestDTO edit);
    public Task<CourseDeletionRequest?> EditCourseDeletionRequest(EditCourseDeletionRequestDTO edit);
    public Task DeleteCourseCreationRequest(Guid courseRequestId);
    public Task DeleteCourseModificationRequest(Guid courseRequestId);
    public Task DeleteCourseDeletionRequest(Guid courseRequestId);
    public Task<Course> GetCourseDataWithSupportingFilesOrThrowOnDeleted(string subject, string catalog);
}

public class CourseService : ICourseService
{
    private readonly ILogger<CourseService> _logger;
    private readonly ICourseRepository _courseRepository;
    private readonly IDossierService _dossierService;
    private readonly IDossierRepository _dossierRepository;

    public CourseService(ILogger<CourseService> logger, ICourseRepository courseRepository, IDossierService dossierService, IDossierRepository dossierRepository)
    {
        _logger = logger;
        _courseRepository = courseRepository;
        _dossierService = dossierService;
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

    public async Task<Course> GetCourseDataOrThrowOnDeleted(string subject, string catalog)
    {
        var course = await _courseRepository.GetCourseBySubjectAndCatalog(subject, catalog) 
            ?? throw new InvalidInputException($"The course {subject}-{catalog} does not exist.");

        if (course.CourseState == CourseStateEnum.Deleted)
        {
            throw new BadRequestException($"The course {subject}-{catalog} is deleted.");
        }

        return course;
    }

    public async Task<Course> GetCourseDataWithSupportingFilesOrThrowOnDeleted(string subject, string catalog)
    {
        var course = await _courseRepository.GetCourseWithSupportingFilesBySubjectAndCatalog(subject, catalog)
            ?? throw new InvalidInputException($"The course {subject}-{catalog} does not exist.");

        if (course.CourseState == CourseStateEnum.Deleted)
        {
            throw new BadRequestException($"The course {subject}-{catalog} is deleted.");
        }

        return course;
    }

    public async Task<CourseCreationRequest> InitiateCourseCreation(CourseCreationInitiationDTO initiation, Guid userId)
    {
        var courseFromDb = await _courseRepository.GetCourseBySubjectAndCatalog(initiation.Subject, initiation.Catalog);
        if (courseFromDb != null && courseFromDb.CourseState == CourseStateEnum.Accepted)
        {
            throw new BadRequestException("The course already exists and is accepted");
        }

        Dossier dossier = await _dossierService.GetDossierForUserOrThrow(initiation.DossierId, userId);

        var course = Course.CreateCourseFromDTOData(initiation, (await _courseRepository.GetMaxCourseId()) + 1, null);

        await SaveCourseForUserOrThrow(course, userId);

        var courseCreationRequest = new CourseCreationRequest 
        {
            Id = Guid.NewGuid(),
            NewCourseId = course.Id,
            DossierId = dossier.Id,
            Rationale = initiation.Rationale,
            ResourceImplication = initiation.ResourceImplication,
            Comment = initiation.Comment,
            Conflict = "Autogenerated comment" // TODO: Correctly autogenerate comment to indicate potential issues
        };

        await _dossierService.SaveCourseCreationRequest(courseCreationRequest);

        return courseCreationRequest;
    }

    public async Task<CourseModificationRequest> InitiateCourseModification(CourseModificationInitiationDTO modification, Guid userId)
    {
        var oldCourse = await GetCourseDataOrThrowOnDeleted(modification.Subject, modification.Catalog);

        Dossier dossier = await _dossierService.GetDossierForUserOrThrow(modification.DossierId, userId);

        var newModifiedCourse = Course.CreateCourseFromDTOData(modification, oldCourse.CourseID, null);

        await SaveCourseForUserOrThrow(newModifiedCourse, userId);

        var courseModificationRequest = new CourseModificationRequest
        {
            Id = Guid.NewGuid(),
            CourseId = newModifiedCourse.Id,
            DossierId = dossier.Id,
            Rationale = modification.Rationale,
            ResourceImplication = modification.ResourceImplication,
            Comment = modification.Comment,
            Conflict = "Autogenerated comment" // TODO: Correctly autogenerate comment to indicate potential issues
        };

        await _dossierService.SaveCourseModificationRequest(courseModificationRequest);

        return courseModificationRequest;
    }

    public async Task<CourseDeletionRequest> InitiateCourseDeletion(CourseDeletionInitiationDTO deletion, Guid userId)
    {
        var oldCourse = await GetCourseDataOrThrowOnDeleted(deletion.Subject, deletion.Catalog);

        Dossier dossier = await _dossierService.GetDossierForUserOrThrow(deletion.DossierId, userId);

        var newDeletedCourse = Course.CloneCourseForDeletionRequest(oldCourse);

        await SaveCourseForUserOrThrow(newDeletedCourse, userId);

        var courseDeletionRequest = new CourseDeletionRequest
        {
            Id = Guid.NewGuid(),
            CourseId = newDeletedCourse.Id,
            DossierId = dossier.Id,
            Rationale = deletion.Rationale,
            ResourceImplication = deletion.ResourceImplication,
            Comment = deletion.Comment,
            Conflict = "Autogenerated comment" // TODO: Correctly autogenerate comment to indicate potential issues
        };

        await _dossierService.SaveCourseDeletionRequest(courseDeletionRequest);

        return courseDeletionRequest;
    }

    public async Task<CourseCreationRequest?> EditCourseCreationRequest(EditCourseCreationRequestDTO edit)
    { 
        var courseCreationRequest = await _dossierService.GetCourseCreationRequest(edit.Id);

        courseCreationRequest.EditRequestData(edit);

        var newCourse = courseCreationRequest.NewCourse ?? throw new NotFoundException($"The course request {edit.Id} does not exist.");

        newCourse.ModifyCourseFromDTOData(edit);

        await _dossierRepository.UpdateCourseCreationRequest(courseCreationRequest);

        return courseCreationRequest;
    }

    public async Task<CourseModificationRequest?> EditCourseModificationRequest(EditCourseModificationRequestDTO edit)
    {
        var courseModificationRequest = await _dossierService.GetCourseModificationRequest(edit.Id);

        courseModificationRequest.EditRequestData(edit);

        var newCourse = courseModificationRequest.Course ?? throw new NotFoundException($"The course request {edit.Id} does not exist.");

        newCourse.ModifyCourseFromDTOData(edit);

        await _dossierRepository.UpdateCourseModificationRequest(courseModificationRequest);

        return courseModificationRequest;
    }

    public async Task<CourseDeletionRequest?> EditCourseDeletionRequest(EditCourseDeletionRequestDTO edit)
    {
        var courseDeletionRequest = await _dossierService.GetCourseDeletionRequest(edit.Id);

        courseDeletionRequest.EditRequestData(edit);

        await _dossierRepository.UpdateCourseDeletionRequest(courseDeletionRequest);

        return courseDeletionRequest;
    }

    public async Task DeleteCourseCreationRequest(Guid courseRequestId)
    {
        var courseCreationRequest = await _dossierService.GetCourseCreationRequest(courseRequestId);
        bool result = await _dossierRepository.DeleteCourseCreationRequest(courseCreationRequest);
        if (!result)
        {
            throw new Exception($"Error deleting ${typeof(CourseCreationRequest)} ${courseCreationRequest.Id}");
        }
        
        _logger.LogInformation($"Deleted ${typeof(CourseCreationRequest)} ${courseCreationRequest.Id}");
    }

    public async Task DeleteCourseModificationRequest(Guid courseRequestId)
    {
        var courseModificationRequest = await _dossierService.GetCourseModificationRequest(courseRequestId);
        bool result = await _dossierRepository.DeleteCourseModificationRequest(courseModificationRequest);
        if (!result)
        {
            throw new Exception($"Error deleting ${typeof(CourseModificationRequest)} ${courseModificationRequest.Id}");
        }

        _logger.LogInformation($"Deleted ${typeof(CourseModificationRequest)} ${courseModificationRequest.Id}");
    }

    public async Task DeleteCourseDeletionRequest(Guid courseRequestId)
    {
        var courseDeletionRequest = await _dossierService.GetCourseDeletionRequest(courseRequestId);
        bool result = await _dossierRepository.DeleteCourseDeletionRequest(courseDeletionRequest);
        if (!result)
        {
            throw new Exception($"Error deleting ${typeof(CourseDeletionRequest)} ${courseDeletionRequest.Id}");
        }

        _logger.LogInformation($"Deleted ${typeof(CourseDeletionRequest)} ${courseDeletionRequest.Id}");
    }

    private async Task SaveCourseForUserOrThrow(Course course, Guid userId)
    {
        bool courseCreated = await _courseRepository.SaveCourse(course);
        if (!courseCreated)
        {
            throw new Exception($"Error inserting ${typeof(Course)} ${course.Id} by {typeof(User)} ${userId}");
        }
        _logger.LogInformation($"Inserted ${typeof(Course)} ${course.Id} by {typeof(User)} ${userId}");
    }
}
