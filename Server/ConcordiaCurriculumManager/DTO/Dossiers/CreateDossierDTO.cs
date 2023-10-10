using System;
namespace ConcordiaCurriculumManager.DTO.Dossiers
{
    public class CreateDossierDTO
    {
        public required string Title { get; set; }

        public required string Description { get; set; }
    }
}

