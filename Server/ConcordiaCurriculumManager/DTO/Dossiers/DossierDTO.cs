using ConcordiaCurriculumManager.Models.Users;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConcordiaCurriculumManager.DTO.Dossiers;

public class DossierDTO
{
    public required Guid Id { get; set; }

    public required Guid InitiatorId { get; set; }

    public required String Title { get; set; }
}


