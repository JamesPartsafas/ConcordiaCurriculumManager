using ConcordiaCurriculumManager.Models.Curriculum.Dossier;
using ConcordiaCurriculumManager.Models.Users;

namespace ConcordiaCurriculumManager.Models.Curriculum.Dossiers
{

    public class Dossier : BaseModel
    {
        public required User Initiator { get; set; }

        public required Guid InitiatorId { get; set; }

        public required String Title { get; set; }
    }
}

