using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Users;

namespace ConcordiaCurriculumManager.Models.Curriculum.Dossiers
{

    public class Dossier : BaseModel
    {
        public required User Initiator { get; set; }

        public required Guid InitiatorId { get; set; }

        public required String Title { get; set; }

        public List<CourseCreationRequest> CourseCreationRequests { get; set; } = new List<CourseCreationRequest>();
    }
}

