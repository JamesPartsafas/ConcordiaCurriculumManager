using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Models.Users;
using System.Data;
using System.Linq.Expressions;

namespace ConcordiaCurriculumManager.Repositories;

public static class ObjectSelectors
{
    public static Expression<Func<User, User>> UserSelector() => user => new User
    {
        Id = user.Id,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Email = user.Email,
        Password = user.Password,
        ResetPasswordToken = user.ResetPasswordToken,
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
}
