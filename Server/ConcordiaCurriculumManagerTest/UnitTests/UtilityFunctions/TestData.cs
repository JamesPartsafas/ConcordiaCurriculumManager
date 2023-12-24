using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.InputDTOs;
using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.OutputDTOs;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.DTO.Dossiers;
using ConcordiaCurriculumManager.DTO.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;

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
            Course = GetSampleCourse(),
            Rationale = "It's necessary",
            ResourceImplication = "New prof needed",
            Comment = "Fun",
        };
    }

    public static CourseDeletionRequest GetSampleCourseDeletionRequest(Dossier dossier, Course course)
    {
        return new CourseDeletionRequest
        {
            DossierId = dossier.Id,
            CourseId = course.Id,
            Rationale = "Why not?",
            ResourceImplication = "New prof needed",
            Comment = "Easy change to make"
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
            State = DossierStateEnum.Created,
            Title = "test title",
            Description = "test description"
        };
    }

    public static Dossier GetSampleDossierInInitialStage()
    {
        var dossierId = Guid.NewGuid();
        return new Dossier
        {
            Id = dossierId,
            InitiatorId = Guid.NewGuid(),
            State = DossierStateEnum.InReview,
            Title = "test title",
            Description = "test description",
            ApprovalStages = new List<ApprovalStage>
            {
                new ApprovalStage { GroupId = Guid.NewGuid(), DossierId = dossierId, StageIndex = 0, IsCurrentStage = true, IsFinalStage = false },
                new ApprovalStage { GroupId = Guid.NewGuid(), DossierId = dossierId, StageIndex = 1, IsCurrentStage = false, IsFinalStage = true },
            }
        };
    }

    public static Dossier GetSampleDossierInFinalStage()
    {
        var dossierId = Guid.NewGuid();
        return new Dossier
        {
            Id = dossierId,
            InitiatorId = Guid.NewGuid(),
            State = DossierStateEnum.InReview,
            Title = "test title",
            Description = "test description",
            ApprovalStages = new List<ApprovalStage>
            {
                new ApprovalStage { GroupId = Guid.NewGuid(), DossierId = dossierId, StageIndex = 0, IsCurrentStage = false, IsFinalStage = false },
                new ApprovalStage { GroupId = Guid.NewGuid(), DossierId = dossierId, StageIndex = 1, IsCurrentStage = true, IsFinalStage = true },
            }
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
            State = DossierStateEnum.Created,
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

    public static CourseVersion GetSampleCourseVersion()
    {
        return new CourseVersion
        {
            Subject = "SOEN",
            Catalog = "490",
            Version = 6,
        };
    }

    public static ICollection<CourseVersion> GetSampleCourseVersionCollection()
    {
        return new List<CourseVersion>
        {
            new CourseVersion
            {
                Subject = "SOEN",
                Catalog = "390",
                Version = 4,
            },
            new CourseVersion
            {
                Subject = "SOEN",
                Catalog = "248",
                Version = 6,
            },
            new CourseVersion
            {
                Subject = "ENGR",
                Catalog = "213",
                Version = 1,
            }
        };
    }

    public static ApprovalStage GetSampleApprovalStage()
    {
        return new ApprovalStage
        {
            DossierId = Guid.NewGuid(),
            GroupId = Guid.NewGuid(),
            StageIndex = 3,
            IsCurrentStage = true,
            IsFinalStage = false
        };
    }

    public static DiscussionMessage GetSampleDiscussionMessage()
    {
        return new DiscussionMessage
        {
            DossierDiscussionId = Guid.NewGuid(),
            GroupId = Guid.NewGuid(),
            AuthorId = Guid.NewGuid(),
            Message = "This is a test message"
        };
    }

    public static Dossier GetSampleDossierWithDiscussion()
    {
        var dossier = GetSampleDossier();
        dossier.State = DossierStateEnum.InReview;
        dossier.Discussion = new()
        {
            DossierId = dossier.Id
        };

        return dossier;
    }

    // DOSSIER REVIEWS DTO
    public static CreateDossierDiscussionMessageDTO GetSampleCreateDossierDiscussionMessageDTO()
    {
        return new CreateDossierDiscussionMessageDTO
        {
            Message = "This is a test message",
            GroupId = Guid.NewGuid()
        };
    }

    public static DossierDiscussionMessageDTO GetSampleDossierDiscussionMessageDTO()
    {
        return new DossierDiscussionMessageDTO
        {
            Id = Guid.NewGuid(),
            Message = "This is a test message",
            GroupId = Guid.NewGuid()
        };
    }

    public static DossierDiscussionDTO GetSampleDossierDiscussionDTO()
    {
        var message = GetSampleDossierDiscussionMessageDTO();

        return new DossierDiscussionDTO
        {
            DossierId = Guid.NewGuid(),
            Messages = new List<DossierDiscussionMessageDTO> { message }
        };
    }

    public static DossierDetailsWithDiscussionDTO GetSampleDossierDetailsWithDiscussionDTO()
    {
        var discusssion = GetSampleDossierDiscussionDTO();

        var dossier = new DossierDetailsWithDiscussionDTO
        {
            Id = Guid.NewGuid(),
            InitiatorId = Guid.NewGuid(),
            Title = "Title",
            Description = "Description",
            State = DossierStateEnum.InReview,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
            Discussion = discusssion
        };

        discusssion.DossierId = dossier.Id;

        return dossier;
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
