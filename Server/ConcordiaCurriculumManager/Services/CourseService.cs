using ConcordiaCurriculumManager.DTO.Courses;
using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.InputDTOs;
using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Curriculum.CourseGroupings;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

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
    public Task<ICollection<CourseVersion>> GetCourseVersions(Dossier dossier);
    public Task<Course> GetCourseByIdAsync(Guid id);
    public Task<IEnumerable<Course>> GetCoursesBySubjectAsync(string subjectCode);
    public Task<Course> PublishCourse(string subject, string catalog);
}

public class CourseService : ICourseService
{
    private readonly ILogger<CourseService> _logger;
    private readonly ICourseRepository _courseRepository;
    private readonly IDossierService _dossierService;
    private readonly IDossierRepository _dossierRepository;
    private readonly ICourseGroupingRepository _courseGroupingRepository;
    private readonly ICourseIdentifiersRepository _courseIdentifiersRepository;

    private const char NonBreakingSpaceChar = '\u00A0';

    public CourseService(
        ILogger<CourseService> logger,
        ICourseRepository courseRepository,
        IDossierService dossierService,
        IDossierRepository dossierRepository,
        ICourseGroupingRepository courseGroupingRepository,
        ICourseIdentifiersRepository courseIdentifierRepository)
    {
        _logger = logger;
        _courseRepository = courseRepository;
        _dossierService = dossierService;
        _dossierRepository = dossierRepository;
        _courseGroupingRepository = courseGroupingRepository;
        _courseIdentifiersRepository = courseIdentifierRepository;
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

        var isDuplicate = await _dossierRepository.CheckIfCourseRequestExists(initiation.DossierId, initiation.Subject, initiation.Catalog);

        if (isDuplicate)
        {
            throw new BadRequestException("A course request for " + initiation.Subject + " " + initiation.Catalog + " already exists in this dossier.");
        }

        Dossier dossier = await _dossierService.GetDossierDetailsByIdOrThrow(initiation.DossierId);

        var courseInProposal = await _courseRepository.GetCourseInProposalBySubjectAndCatalog(initiation.Subject, initiation.Catalog);

        int concordiaCourseId;
        if (courseFromDb != null) concordiaCourseId = courseFromDb.CourseID;
        else if (courseInProposal != null) concordiaCourseId = courseInProposal.CourseID;
        else concordiaCourseId = (await _courseRepository.GetMaxCourseId()) + 1;

        var course = Course.CreateCourseFromDTOData(initiation, concordiaCourseId, null);

        if (courseInProposal is null && courseFromDb is null)
        {
            var courseIdentifier = new CourseIdentifier
            {
                Id = Guid.NewGuid(),
                ConcordiaCourseId = course.CourseID
            };
            await _courseIdentifiersRepository.SaveCourseIdentifier(courseIdentifier);
        }

        await SaveCourseForUserOrThrow(course, userId);

        var courseCreationRequest = new CourseCreationRequest
        {
            Id = Guid.NewGuid(),
            NewCourseId = course.Id,
            DossierId = dossier.Id,
            Rationale = initiation.Rationale,
            ResourceImplication = initiation.ResourceImplication,
            Comment = initiation.Comment,
            Conflict = string.Empty,
        };

        await _dossierService.SaveCourseCreationRequest(courseCreationRequest);

        return courseCreationRequest;
    }

    public async Task<CourseModificationRequest> InitiateCourseModification(CourseModificationInitiationDTO modification, Guid userId)
    {
        var isDuplicate = await _dossierRepository.CheckIfCourseRequestExists(modification.DossierId, modification.Subject, modification.Catalog);

        if (isDuplicate)
        {
            throw new BadRequestException("A course request for " + modification.Subject + " " + modification.Catalog + " already exists in this dossier.");
        }

        var oldCourse = await GetCourseDataOrThrowOnDeleted(modification.Subject, modification.Catalog);

        Dossier dossier = await _dossierService.GetDossierDetailsByIdOrThrow(modification.DossierId);

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
            Conflict = GenerateConflictMessageFromReferencedCourses(oldCourse)
        };

        await _dossierService.SaveCourseModificationRequest(courseModificationRequest);

        return courseModificationRequest;
    }

    public async Task<CourseDeletionRequest> InitiateCourseDeletion(CourseDeletionInitiationDTO deletion, Guid userId)
    {
        var isDuplicate = await _dossierRepository.CheckIfCourseRequestExists(deletion.DossierId, deletion.Subject, deletion.Catalog);

        if (isDuplicate)
        {
            throw new BadRequestException("A course request for " + deletion.Subject + " " + deletion.Catalog + " already exists in this dossier.");
        }

        var oldCourse = await GetCourseDataOrThrowOnDeleted(deletion.Subject, deletion.Catalog);

        await VerifyCourseIsNotInCourseGroupingOrThrow(oldCourse);

        Dossier dossier = await _dossierService.GetDossierDetailsByIdOrThrow(deletion.DossierId);

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
            Conflict = GenerateConflictMessageFromReferencedCourses(oldCourse)
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

    private string GenerateConflictMessageFromReferencedCourses(Course course)
    {
        if (course.CourseReferenced is null || course.CourseReferenced.Count == 0)
        {
            return string.Empty;
        }

        var courseNames = new List<string>();
        foreach (var reference in course.CourseReferenced)
        {
            var courseReferencing = reference.CourseReferencing;

            if (reference.State.Equals(CourseReferenceEnum.OutOfDate))
            {
                continue;
            }

            courseNames.Add($"{courseReferencing.Subject} {courseReferencing.Catalog}");
        }

        var courses = courseNames.Count == 1 ? courseNames.First() : string.Join(", ", courseNames.Take(courseNames.Count - 1)) + ", and " + courseNames.Last();
        return $"Altering this course can have an impact on {courses}";
    }

    public async Task<ICollection<CourseVersion>> GetCourseVersions(Dossier dossier)
    {
        IEnumerable<Course?> courses = new List<Course>();
        courses = courses.Concat(dossier.CourseCreationRequests.Select(ccr => ccr.NewCourse).ToList());
        courses = courses.Concat(dossier.CourseModificationRequests.Select(cmr => cmr.Course).ToList());
        courses = courses.Concat(dossier.CourseDeletionRequests.Select(cdr => cdr.Course).ToList());

        ICollection<CourseVersion> currentCourseVersions = new List<CourseVersion>();
        foreach (var course in courses)
        {
            if (course == null) continue;

            int? version = await _courseRepository.GetCurrentCourseVersion(course.Subject, course.Catalog);
            version ??= 0;

            currentCourseVersions.Add(new CourseVersion { Subject = course.Subject, Catalog = course.Catalog, Version = (int)version });
        }

        return currentCourseVersions;
    }

    private async Task VerifyCourseIsNotInCourseGroupingOrThrow(Course course)
    {
        var grouping = await _courseGroupingRepository.GetCourseGroupingContainingCourse(course);

        if (grouping != null)
            throw new BadRequestException(
                $"A deletion request cannot be made for {course.Subject} {course.Catalog} as it is part of the {grouping.Name} course grouping"
            );
    }

    public async Task<Course> GetCourseByIdAsync(Guid id)
    {
        var course = await _courseRepository.GetCourseByIdAsync(id);
        if (course == null)
        {
            throw new NotFoundException($"Course with ID: {id} was not found.");
        }

        return course;
    }

    public async Task<IEnumerable<Course>> GetCoursesBySubjectAsync(string subjectCode)
    {
        return await _courseRepository.GetCoursesBySubjectAsync(subjectCode);
    }

    public async Task<Course> PublishCourse(string subject, string catalog)
    {
        var newCourse = await _courseRepository.GetCourseBySubjectAndCatalog(subject, catalog) ?? throw new NotFoundException($"The course {subject}" + $"{catalog} was not found.");
        var oldCourse = await _courseRepository.GetPublishedVersion(subject, catalog) ?? throw new NotFoundException($"The course {subject}" + $"{catalog} was not found.");

        newCourse.MarkAsPublished();
        oldCourse.MarkAsUnpublished();

        await _courseRepository.UpdateCourse(newCourse);
        await _courseRepository.UpdateCourse(oldCourse);

        var courseSubjectAndCatalog = ExtractReferencedCourseSubjectAndCatalog(newCourse);

        bool result = newCourse.CourseState.Equals(CourseStateEnum.Deleted)
            ? await _courseRepository.InvalidateAllCourseReferences(oldCourse.Id)
            : await _courseRepository.UpdateCourseReferences(oldCourse, newCourse, courseSubjectAndCatalog);

        if (!result)
        {
            _logger.LogError($"Failed to update the course references for {subject}-{catalog} with Id {newCourse.Id}");
        }

        return newCourse;
    }

    private List<(string subject, string catalog)> ExtractReferencedCourseSubjectAndCatalog(Course course)
    {
        var pattern = @"[a-zA-Z]{3,4}[, ]?\d{3}[a-zA-Z]?";
        var possibleInput = new List<string>
        {
            course.Description,
            course.CourseNotes ?? "",
            course.PreReqs,
            course.EquivalentCourses ?? ""
        };

        var regexResult = possibleInput.SelectMany(input =>
        {
            input = input.Replace(NonBreakingSpaceChar, ' ');
            var result = Regex.Matches(input, pattern);
            return result.Select(r => r.Value);
        }).Select(r =>
        {
            var indexOfCatalog = r.IndexOfAny("0123456789".ToCharArray());
            return (r[..indexOfCatalog].Trim(), r[indexOfCatalog..].Trim());
        }).DistinctBy(r => $"{r.Item1} {r.Item2}").ToList();

        return regexResult;
    }
}
