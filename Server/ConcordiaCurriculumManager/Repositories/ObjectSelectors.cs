using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Models.Users;
using System.Data;
using System.Linq.Expressions;

namespace ConcordiaCurriculumManager.Repositories;

public static class ObjectSelectors
{
    public static Expression<Func<Role, Role>> RoleSelector() => role => new Role
    {
        UserRole = role.UserRole
    };

    public static Expression<Func<User, User>> UserSelector() => user => new User
    {
        Id = user.Id,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Email = user.Email,
        Password = user.Password,
        Roles = (List<Role>)user.Roles.Select(role => new Role
        {
            UserRole = role.UserRole
        }),
        Groups = (List<Group>)user.Groups.Select(group => new Group
        {
            Id = group.Id,
            Name = group.Name
        }),
        MasteredGroups = (List<Group>)user.MasteredGroups.Select(group => new Group
        {
            Id = group.Id,
            Name = group.Name
        })
    };

    public static Expression<Func<Group, Group>> GroupSelector() => group => new Group
    {
        Id = group.Id,
        Name = group.Name,
        Members = (List<User>)group.Members.Select(user => new User
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Password = user.Password,
            Roles = (List<Role>)user.Roles.Select(role => new Role
            {
                UserRole = role.UserRole
            })
        }),
        GroupMasters = (List<User>)group.GroupMasters.Select(user => new User
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Password = user.Password,
            Roles = (List<Role>)user.Roles.Select(role => new Role
            {
                UserRole = role.UserRole
            })
        })
    };

    public static Expression<Func<Dossier, Dossier>> DossierSelector() => dossier => new Dossier
    {
        Id = dossier.Id,
        CreatedDate = dossier.CreatedDate,
        ModifiedDate = dossier.ModifiedDate,
        Title = dossier.Title,
        Description = dossier.Description,
        State = dossier.State,
        InitiatorId = dossier.InitiatorId,
        CourseCreationRequests = (List<CourseCreationRequest>)dossier.CourseCreationRequests.Select(request => new CourseCreationRequest
        {
            Id = request.Id,
            CreatedDate = request.CreatedDate,
            ModifiedDate = request.ModifiedDate,
            NewCourseId = request.NewCourseId,
            NewCourse = request.NewCourse,
            DossierId = request.DossierId,
            ResourceImplication = request.ResourceImplication,
            Rationale = request.Rationale,
            Comment = request.Comment
        }),
        CourseModificationRequests = (List<CourseModificationRequest>)dossier.CourseModificationRequests.Select(request => new CourseModificationRequest
        {
            Id = request.Id,
            CreatedDate = request.CreatedDate,
            ModifiedDate = request.ModifiedDate,
            CourseId = request.CourseId,
            Course = request.Course,
            DossierId = request.DossierId,
            ResourceImplication = request.ResourceImplication,
            Rationale = request.Rationale,
            Comment = request.Comment
        }),
        CourseDeletionRequests = (List<CourseDeletionRequest>)dossier.CourseDeletionRequests.Select(request => new CourseDeletionRequest
        {
            Id = request.Id,
            CreatedDate = request.CreatedDate,
            ModifiedDate = request.ModifiedDate,
            CourseId = request.CourseId,
            Course = request.Course,
            DossierId = request.DossierId,
            ResourceImplication = request.ResourceImplication,
            Rationale = request.Rationale,
            Comment = request.Comment
        }),
        ApprovalStages = (List<ApprovalStage>)dossier.ApprovalStages.Select(stage => new ApprovalStage
        {
            Id = stage.Id,
            GroupId = stage.GroupId,
            DossierId = stage.DossierId,
            StageIndex = stage.StageIndex,
            IsCurrentStage = stage.IsCurrentStage,
            IsFinalStage = stage.IsFinalStage
        })
    };

    public static Expression<Func<Course, Course>> CourseSelector() => course => new Course
    {
        Id = course.Id,
        CreatedDate = course.CreatedDate,
        ModifiedDate = course.ModifiedDate,
        CourseID = course.CourseID,
        Subject = course.Subject,
        Catalog = course.Catalog,
        Title = course.Title,
        Description = course.Description,
        CourseNotes = course.CourseNotes,
        CreditValue = course.CreditValue,
        PreReqs = course.PreReqs,
        Career = course.Career,
        EquivalentCourses = course.EquivalentCourses,
        CourseState = course.CourseState,
        Version = course.Version,
        Published = course.Published,
        CourseCourseComponents = course.CourseCourseComponents != null ? (ICollection<CourseCourseComponent>)course.CourseCourseComponents.Select(component => new CourseCourseComponent
        {
            Id = component.Id,
            CreatedDate = component.CreatedDate,
            ModifiedDate = component.ModifiedDate,
            CourseComponent = component.CourseComponent,
            ComponentCode = component.ComponentCode,
            CourseId = component.CourseId,
            HoursPerWeek = component.HoursPerWeek
        }) : null,
        CourseCreationRequest = course.CourseCreationRequest != null ? new CourseCreationRequest
        {
            Id = course.CourseCreationRequest.Id,
            CreatedDate = course.CourseCreationRequest.CreatedDate,
            ModifiedDate = course.CourseCreationRequest.ModifiedDate,
            NewCourseId = course.CourseCreationRequest.NewCourseId,
            NewCourse = course.CourseCreationRequest.NewCourse,
            DossierId = course.CourseCreationRequest.DossierId,
            ResourceImplication = course.CourseCreationRequest.ResourceImplication,
            Rationale = course.CourseCreationRequest.Rationale,
            Comment = course.CourseCreationRequest.Comment
        } : null,
        CourseModificationRequest = course.CourseModificationRequest != null ? new CourseModificationRequest
        {
            Id = course.CourseModificationRequest.Id,
            CreatedDate = course.CourseModificationRequest.CreatedDate,
            ModifiedDate = course.CourseModificationRequest.ModifiedDate,
            CourseId = course.CourseModificationRequest.CourseId,
            Course = course.CourseModificationRequest.Course,
            DossierId = course.CourseModificationRequest.DossierId,
            ResourceImplication = course.CourseModificationRequest.ResourceImplication,
            Rationale = course.CourseModificationRequest.Rationale,
            Comment = course.CourseModificationRequest.Comment
        } : null,
        CourseDeletionRequest = course.CourseDeletionRequest != null ? new CourseDeletionRequest
        {
            Id = course.CourseDeletionRequest.Id,
            CreatedDate = course.CourseDeletionRequest.CreatedDate,
            ModifiedDate = course.CourseDeletionRequest.ModifiedDate,
            CourseId = course.CourseDeletionRequest.CourseId,
            Course = course.CourseDeletionRequest.Course,
            DossierId = course.CourseDeletionRequest.DossierId,
            ResourceImplication = course.CourseDeletionRequest.ResourceImplication,
            Rationale = course.CourseDeletionRequest.Rationale,
            Comment = course.CourseDeletionRequest.Comment
        } : null,
        CourseReferenced = course.CourseReferenced != null ? (ICollection<CourseReference>)course.CourseReferenced.Select(courseRefenced => new CourseReference
        {
            Id = courseRefenced.Id,
            CreatedDate = courseRefenced.CreatedDate,
            ModifiedDate = courseRefenced.ModifiedDate,
            CourseReferencingId = courseRefenced.CourseReferencingId,
            CourseReferencing = courseRefenced.CourseReferencing,
            CourseReferencedId = courseRefenced.CourseReferencedId,
            CourseReferenced = courseRefenced.CourseReferenced
        }) : null,
        CourseReferencing = course.CourseReferencing != null ? (ICollection<CourseReference>)course.CourseReferencing.Select(courseRefencing => new CourseReference
        {
            Id = courseRefencing.Id,
            CreatedDate = courseRefencing.CreatedDate,
            ModifiedDate = courseRefencing.ModifiedDate,
            CourseReferencingId = courseRefencing.CourseReferencingId,
            CourseReferencing = courseRefencing.CourseReferencing,
            CourseReferencedId = courseRefencing.CourseReferencedId,
            CourseReferenced = courseRefencing.CourseReferenced
        }) : null,
    };
}
