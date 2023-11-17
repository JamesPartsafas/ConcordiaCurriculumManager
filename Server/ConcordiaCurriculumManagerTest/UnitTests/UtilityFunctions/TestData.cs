using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.InputDTOs;
using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.OutputDTOs;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.DTO.Dossiers;
using ConcordiaCurriculumManager.DTO.Dossiers.DossierReview;

namespace ConcordiaCurriculumManagerTest.UnitTests.UtilityFunctions;
public static class TestData
{
    // USER DATA

    public static User GetSampleUser()
    {
        return new User
        {
            Id = new Guid(),
            FirstName = "Joe",
            LastName = "Smith",
            Email = "jsmith@ccm.com",
            Password = "Password123!"
        };
    }

    // COURSE DATA

    public static Course GetSampleCourse()
    {
        var id = Guid.NewGuid();

        return new Course
        {
            Id = id,
            CourseID = 1000,
            Subject = "SOEN",
            Catalog = "490",
            Title = "Capstone",
            Description = "Curriculum manager building simulator",
            CreditValue = "6",
            PreReqs = "SOEN 390",
            CourseNotes = "Lots of fun",
            Career = CourseCareerEnum.UGRD,
            EquivalentCourses = "",
            CourseState = CourseStateEnum.NewCourseProposal,
            Version = null,
            Published = false,
            CourseCourseComponents = CourseCourseComponent.GetComponentCodeMapping(new Dictionary<ComponentCodeEnum, int?> { { ComponentCodeEnum.LEC, 3 } }, id)
        };
    }

    public static Course GetSampleAcceptedCourse()
    {
        var course = GetSampleCourse(); ;
        course.Version = 2;
        course.CourseState = CourseStateEnum.Accepted;

        return course;
    }

    public static Course GetSampleDeletedCourse()
    {
        var course = GetSampleCourse(); ;
        course.Version = 2;
        course.CourseState = CourseStateEnum.Deleted;

        return course;
    }

    // REQUEST DATA

    public static CourseCreationInitiationDTO GetSampleCourseCreationInitiationDTO(Dossier dossier)
    {
        return new CourseCreationInitiationDTO
        {
            Subject = "SOEN",
            Catalog = "490",
            Title = "Capstone",
            Description = "Curriculum manager building simulator",
            CreditValue = "6",
            PreReqs = "SOEN 390",
            CourseNotes = "Fun",
            Rationale = "It's necessary",
            ResourceImplication = "New prof needed",
            Career = CourseCareerEnum.UGRD,
            EquivalentCourses = "",
            ComponentCodes = new Dictionary<ComponentCodeEnum, int?> { { ComponentCodeEnum.LEC, 3 } },
            SupportingFiles = new Dictionary<string, string> { { "name.pdf", "base64content" } },
            DossierId = dossier.Id,
        };
    }

    public static CourseCreationRequestDTO GetSampleCourseCreationRequestDTO(Course course, Dossier dossier)
    {
        return new CourseCreationRequestDTO
        {
            Id = Guid.NewGuid(),
            DossierId = dossier.Id,
            NewCourseId = course.Id,
            Rationale = "It's necessary",
            ResourceImplication = "New prof needed",
            Comment = "Fun",
        };
    }

    public static CourseCreationRequest GetSampleCourseCreationRequest(Dossier dossier, Course course)
    {
        return new CourseCreationRequest
        {
            DossierId = dossier.Id,
            NewCourseId = course.Id,
            Rationale = "Why not?",
            ResourceImplication = "New prof needed",
            Comment = "Easy change to make"
        };
    }

    public static CourseCreationRequest GetSampleCourseCreationRequest()
    {
        return new CourseCreationRequest
        {
            DossierId = Guid.NewGuid(),
            NewCourseId = Guid.NewGuid(),
            NewCourse = GetSampleCourse(),
            Rationale = "It's necessary",
            ResourceImplication = "New prof needed",
            Comment = "Fun",
        };
    }

    public static CourseModificationInitiationDTO GetSampleCourseCreationModificationDTO(Course course, Dossier dossier)
    {
        return new CourseModificationInitiationDTO
        {
            Subject = "SOEN",
            Catalog = "490",
            Title = "Capstone",
            Description = "Curriculum manager building simulator",
            CreditValue = "6",
            PreReqs = "SOEN 390",
            CourseNotes = "Fun",
            Rationale = "It's necessary",
            ResourceImplication = "New prof needed",
            Career = CourseCareerEnum.UGRD,
            EquivalentCourses = "",
            ComponentCodes = new Dictionary<ComponentCodeEnum, int?> { { ComponentCodeEnum.LEC, 3 } },
            SupportingFiles = new Dictionary<string, string> { { "name.pdf", "base64content" } },
            DossierId = dossier.Id,
            CourseId = course.CourseID
        };
    }

    public static CourseModificationRequestDTO GetSampleCourseModificationRequestDTO(Course course, Dossier dossier)
    {
        return new CourseModificationRequestDTO
        {
            Id = Guid.NewGuid(),
            DossierId = dossier.Id,
            CourseId = course.Id,
            Rationale = "It's necessary",
            ResourceImplication = "New prof needed",
            Comment = "Fun",
        };
    }

    public static CourseModificationRequest GetSampleCourseModificationRequest(Dossier dossier, Course course)
    {
        return new CourseModificationRequest
        {
            DossierId = dossier.Id,
            CourseId = course.Id,
            Rationale = "Why not?",
            ResourceImplication = "New prof needed",
            Comment = "Easy change to make"
        };
    }

    public static CourseModificationRequest GetSampleCourseModificationRequest()
    {
        return new CourseModificationRequest
        {
            DossierId = Guid.NewGuid(),
            CourseId = Guid.NewGuid(),
            Course = GetSampleCourse(),
            Rationale = "It's necessary",
            ResourceImplication = "New prof needed",
            Comment = "Fun",
        };
    }

    public static CourseDeletionInitiationDTO GetSampleCourseCreationDeletionDTO(Course course, Dossier dossier)
    {
        return new CourseDeletionInitiationDTO
        {
            Rationale = "It's necessary",
            ResourceImplication = "New prof needed",
            DossierId = dossier.Id,
            Subject = course.Subject,
            Catalog = course.Catalog
        };
    }

    public static CourseDeletionRequestDTO GetSampleCourseDeletionRequestDTO(Course course, Dossier dossier)
    {
        return new CourseDeletionRequestDTO
        {
            Id = Guid.NewGuid(),
            DossierId = dossier.Id,
            CourseId = course.Id,
            Rationale = "It's necessary",
            ResourceImplication = "New prof needed",
            Comment = "Fun",
        };
    }

    public static CourseDeletionRequest GetSampleCourseDeletionRequest()
    {
        return new CourseDeletionRequest
        {
            DossierId = Guid.NewGuid(),
            CourseId = Guid.NewGuid(),
            Rationale = "It's necessary",
            ResourceImplication = "New prof needed",
            Comment = "Fun",
        };
    }

    public static EditCourseCreationRequestDTO GetSampleEditCourseCreationRequestDTO()
    {
        return new EditCourseCreationRequestDTO
        {
            Id = Guid.NewGuid(),
            DossierId = Guid.NewGuid(),
            Rationale = "It's necessary",
            ResourceImplication = "New prof needed",
            Comment = "No comment",
            Subject = "SOEN Modified3",
            Catalog = "500",
            Title = "Super Capstone for Software Engineers",
            Description = "An advanced capstone project for final year software engineering students.",
            CourseNotes = "Students are required to present their project at the end of the semester.",
            CreditValue = "6.5",
            PreReqs = "SOEN 490 previously or concurrently",
            Career = CourseCareerEnum.UGRD,
            EquivalentCourses = "SOEN 499",
            ComponentCodes = new Dictionary<ComponentCodeEnum, int?> { { ComponentCodeEnum.LEC, 3 } },
            SupportingFiles = new Dictionary<string, string> { { "name.pdf", "base64content" } },
        };
    }

    public static EditCourseModificationRequestDTO GetSampleEditCourseModificationRequestDTO()
    {
        return new EditCourseModificationRequestDTO
        {
            Id = Guid.NewGuid(),
            DossierId = Guid.NewGuid(),
            Rationale = "It's necessary",
            ResourceImplication = "New prof needed",
            Comment = "No comment",
            Title = "Super Capstone for Software Engineers",
            Description = "An advanced capstone project for final year software engineering students.",
            CourseNotes = "Students are required to present their project at the end of the semester.",
            CreditValue = "6.5",
            PreReqs = "SOEN 490 previously or concurrently",
            Career = CourseCareerEnum.UGRD,
            EquivalentCourses = "SOEN 499",
            ComponentCodes = new Dictionary<ComponentCodeEnum, int?> { { ComponentCodeEnum.LEC, 3 } },
            SupportingFiles = new Dictionary<string, string> { { "name.pdf", "base64content" } },
        };
    }

    public static EditCourseDeletionRequestDTO GetSampleEditCourseDeletionRequestDTO()
    {
        return new EditCourseDeletionRequestDTO
        {
            Id = Guid.NewGuid(),
            DossierId = Guid.NewGuid(),
            Rationale = "It's necessary",
            ResourceImplication = "New prof needed",
            Comment = "No comment",
        };
    }

    // DOSSIER DATA
    public static Dossier GetSampleDossier()
    {
        return new Dossier
        {
            InitiatorId = Guid.NewGuid(),
            Published = false,
            Title = "test title",
            Description = "test description"
        };
    }

    public static Dossier GetSampleDossier(User user)
    {
        return new Dossier
        {
            Initiator = user,
            InitiatorId = user.Id,
            Title = "Dossier 1",
            Description = "Text description of a dossier.",
            Published = false,
        };
    }

    public static CreateDossierDTO GetSampleCreateDossierDTO()
    {
        return new CreateDossierDTO
        {
            Title = "test title",
            Description = "test description"
        };
    }

    public static EditDossierDTO GetSampleEditDossierDTO()
    {
        return new EditDossierDTO
        {
            InitiatorId = Guid.NewGuid(),
            Title = "test title",
            Description = "test description"
        };
    }

    public static Guid GetSampleDeleteDossierDTO()
    {
        return Guid.NewGuid();
    }

    // DOSSIER REVIEWS
    public static DossierSubmissionDTO GetSampleDossierSubmissionDTO()
    {
        return new DossierSubmissionDTO
        {
            DossierId = GetSampleDossier().Id,
            GroupIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() }
        };
    }

    // GROUPS
    public static Group GetSampleGroup()
    {
        return new Group
        {
            Id = Guid.NewGuid(),
            Name = "Senate"
        };
    }
}
