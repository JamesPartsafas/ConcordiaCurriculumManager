using ConcordiaCurriculumManager.Models.Curriculum.CourseGroupings;
using System;
namespace ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview
{
    public class DossierReport
    {
        public required Dossier Dossier { get; set; }

        public required IList<Course> OldCourses { get; set; }

        public required IList<CourseGrouping> OldGroupings { get; set; }
    }
}

