using System;
namespace ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview
{
    public class DossierReport
    {
        public required Dossier Dossier { get; set; }

        public required IList<Course> OldCourses { get; set; }
    }
}

