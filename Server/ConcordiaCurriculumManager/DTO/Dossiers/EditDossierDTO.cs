using System;
namespace ConcordiaCurriculumManager.DTO.Dossiers
{
    public class EditDossierDTO
    {
        public required Guid InitiatorId { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }
    }
}

