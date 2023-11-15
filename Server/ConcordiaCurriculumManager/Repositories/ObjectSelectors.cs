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
        Published = dossier.Published,
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
}
