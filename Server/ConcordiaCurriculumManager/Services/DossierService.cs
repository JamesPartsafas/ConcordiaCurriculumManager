using ConcordiaCurriculumManager.DTO.Dossiers;
using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories;
using NetTopologySuite.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
    public Task<CourseDeletionRequest> GetCourseDeletionRequest(Guid courseRequestId);
    public Task<IList<User>> GetCurrentlyReviewingGroupMasters(Guid dossierId);
    public Task<DossierReport> GetDossierReportByDossierId(Guid dossierId);
    public Task<IList<Dossier>> GetDossiersRequiredReview(Guid userId);
    public Task<CourseChanges> GetChangesAcrossAllDossiers();
}

public class DossierService : IDossierService
{
    private readonly ILogger<DossierService> _logger;
    private readonly IDossierRepository _dossierRepository;
    private readonly ICourseRepository _courseRepository;

    public DossierService(ILogger<DossierService> logger, IDossierRepository dossierRepository, ICourseRepository courseRepository)
    {
        _logger = logger;
        _dossierRepository = dossierRepository;
        _courseRepository = courseRepository;
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

    public async Task<Dossier> CreateDossierForUser(CreateDossierDTO d, User user)
    {
        var dossierId = Guid.NewGuid();
        var dossier = new Dossier
        {
            Id = dossierId,
            InitiatorId = user.Id,
            Title = d.Title,
            Description = d.Description,
            State = DossierStateEnum.Created,
            Discussion = new()
            {
                DossierId = dossierId
            }
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

    public async Task<CourseDeletionRequest> GetCourseDeletionRequest(Guid courseRequestId)
    {
        var courseDeletionRequest = await _dossierRepository.GetCourseDeletionRequest(courseRequestId) ?? throw new NotFoundException($"Error retrieving the course deletion request with id ${typeof(CourseDeletionRequest)} ${courseRequestId}");
        return courseDeletionRequest;
    }

    public async Task<IList<User>> GetCurrentlyReviewingGroupMasters(Guid dossierId)
    {
        return await _dossierRepository.GetCurrentlyReviewingGroupMasters(dossierId);
    }

    public async Task<DossierReport> GetDossierReportByDossierId(Guid dossierId)
    {
        var dossier = await _dossierRepository.GetDossierReportByDossierId(dossierId) ?? throw new NotFoundException("The dossier does not exist.");
        var oldCourses = new List<Course>();

        foreach (var request in dossier.CourseModificationRequests)
        {
            var course = await _courseRepository.GetCourseWithSupportingFilesBySubjectAndCatalog(request.Course!.Subject, request.Course.Catalog);
            if (course == null) continue;
            oldCourses.Add(course);
        }

        return new DossierReport { Dossier = dossier, OldCourses = oldCourses };
    }

    public async Task<IList<Dossier>> GetDossiersRequiredReview(Guid userId)
    {
        return await _dossierRepository.GetDossiersRequiredReview(userId);
    }

    public async Task<CourseChanges> GetChangesAcrossAllDossiers()
    {
        var courses = await _dossierRepository.GetChangesAcrossAllDossiers();
        var oldCourses = new List<Course>();

        foreach (var course in courses)
        {
            if (course.CourseModificationRequest is null) continue;
            var oldCourse = await _courseRepository.GetPublishedVersion(course.Subject, course.Catalog);
            if (oldCourse == null) continue;
            oldCourses.Add(oldCourse);
        }
        var courseCreationRequests = new List<CourseCreationRequest>();
        var courseModificationRequests = new List<CourseModificationRequest>();
        var courseDeletionRequests = new List<CourseDeletionRequest>();

        foreach (var course in courses)
        {
            if (course.CourseCreationRequest is not null)
            {
                var request = course.CourseCreationRequest;
                course.CourseCreationRequest = null;
                request.NewCourse = course;
                courseCreationRequests.Add(request);
            }

            if (course.CourseModificationRequest is not null)
            {
                var request = course.CourseModificationRequest;
                course.CourseModificationRequest = null;
                request.Course = course;
                courseModificationRequests.Add(request);
            }

            if (course.CourseDeletionRequest is not null)
            {
                var request = course.CourseDeletionRequest;
                course.CourseDeletionRequest = null;
                request.Course = course;
                courseDeletionRequests.Add(request);
            }
        }

        return new CourseChanges
        {
            CourseCreationRequests = courseCreationRequests,
            CourseModificationRequests = courseModificationRequests,
            CourseDeletionRequests = courseDeletionRequests,
            OldCourses = oldCourses
        };
    }
}
