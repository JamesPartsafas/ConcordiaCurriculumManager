using ConcordiaCurriculumManager.Models.Users;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConcordiaCurriculumManager.DTO.Dossiers;

public class DossierDTO
{
    public required Guid Id { get; set; }

    public required Guid InitiatorId { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public required bool Published { get; set; }
}


