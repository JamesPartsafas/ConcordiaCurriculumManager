using System;
namespace ConcordiaCurriculumManager.Models.Curriculum.Dossiers
{
    public class CourseModificationRequest : CourseRequest
    {
        public required Guid CourseId { get; set; }

        public Course? Course { get; set; }
    }
}

