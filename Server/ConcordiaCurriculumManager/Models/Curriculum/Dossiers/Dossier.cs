using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Users;

namespace ConcordiaCurriculumManager.Models.Curriculum.Dossiers
{

    public class Dossier : BaseModel
    {
        public User? Initiator { get; set; }

        public required Guid InitiatorId { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public required bool Published { get; set; }

        public List<CourseCreationRequest> CourseCreationRequests { get; set; } = new List<CourseCreationRequest>();

    }
}

