﻿using ConcordiaCurriculumManager.DTO.Courses;
using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests;
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
    private readonly IDossierService _dossierService;

    public CourseService(ILogger<CourseService> logger, ICourseRepository courseRepository, IDossierService dossierService)
    {
        _logger = logger;
        _courseRepository = courseRepository;
        _dossierService = dossierService;
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

        Dossier dossier = await _dossierService.GetDossierForUserOrThrow(initiation.DossierId, userId);

        var course = CreateCourseFromDTOData(initiation, (await _courseRepository.GetMaxCourseId()) + 1, 1);

        await SaveCourseForUserOrThrow(course, userId);

        var courseCreationRequest = new CourseCreationRequest 
        {
            Id = Guid.NewGuid(),
            NewCourseId = course.Id,
            DossierId = dossier.Id,
            Rationale = initiation.Rationale,
            ResourceImplication = initiation.ResourceImplication,
            Comment = "Autogenerated comment" // TODO: Correctly autogenerate comment to indicate potential issues
        };

        await _dossierService.SaveCourseCreationRequest(courseCreationRequest);

        return courseCreationRequest;
    }

    public async Task<CourseModificationRequest> InitiateCourseModification(CourseModificationInitiationDTO modification, Guid userId)
    {
        var oldCourse = await _courseRepository.GetCourseByCourseId(modification.CourseId) ?? throw new ArgumentException("The course does not exist.");

        Dossier dossier = await _dossierService.GetDossierForUserOrThrow(modification.DossierId, userId);

        var newModifiedCourse = CreateCourseFromDTOData(modification, oldCourse.CourseID, oldCourse.Version + 1);

        await SaveCourseForUserOrThrow(newModifiedCourse, userId);

        var courseModificationRequest = new CourseModificationRequest
        {
            Id = Guid.NewGuid(),
            CourseId = newModifiedCourse.Id,
            DossierId = dossier.Id,
            Rationale = modification.Rationale,
            ResourceImplication = modification.ResourceImplication,
            Comment = "Autogenerated comment" // TODO: Correctly autogenerate comment to indicate potential issues
        };

        await _dossierService.SaveCourseModificationRequest(courseModificationRequest);

        return courseModificationRequest;
    }

    private static Course CreateCourseFromDTOData(CourseRequestInitiationDTO initiation, int concordiaCourseId, int version)
    {
        var internalCourseId = Guid.NewGuid();

        return new Course
        {
            Id = internalCourseId,
            CourseID = concordiaCourseId,
            Subject = initiation.Subject,
            Catalog = initiation.Catalog,
            Title = initiation.Title,
            Description = initiation.Description,
            CourseNotes = initiation.CourseNotes,
            CreditValue = initiation.CreditValue,
            PreReqs = initiation.PreReqs,
            Career = initiation.Career,
            EquivalentCourses = initiation.EquivalentCourses,
            CourseState = initiation.GetAssociatedCourseState(),
            Version = version,
            Published = false,
            CourseCourseComponents = CourseCourseComponent.GetComponentCodeMapping(initiation.ComponentCodes, internalCourseId),
            SupportingFiles = SupportingFile.GetSupportingFileMapping(initiation.SupportingFiles, internalCourseId)
        };
    }

    private async Task SaveCourseForUserOrThrow(Course course, Guid userId)
    {
        bool courseCreated = await _courseRepository.SaveCourse(course);
        if (!courseCreated)
        {
            _logger.LogWarning($"Error inserting ${typeof(Course)} ${course.Id} by {typeof(User)} ${userId}");
            throw new Exception("Error registering the course");
        }
        _logger.LogInformation($"Inserted ${typeof(Course)} ${course.Id} by {typeof(User)} ${userId}");
    }
}
