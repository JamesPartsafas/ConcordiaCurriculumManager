using System.ComponentModel.DataAnnotations;

namespace ConcordiaCurriculumManager.DTO;

public class GroupCreateDTO
{
    [Required(ErrorMessage = "Name is required.")]
    [MinLength(1, ErrorMessage = "Name cannot be empty.")]
    public required string Name { get; set; }
}
