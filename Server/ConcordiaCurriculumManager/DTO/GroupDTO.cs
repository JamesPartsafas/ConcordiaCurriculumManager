using ConcordiaCurriculumManager.Models.Users;

namespace ConcordiaCurriculumManager.DTO;

public class GroupDTO
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required List<UserDTO> Members { get; set; } = new();
}
