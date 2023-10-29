using System;
namespace ConcordiaCurriculumManager.DTO.Courses
{
    public class CourseDTO
    {
        public required int CourseID { get; set; }

        public required string Subject { get; set; }

        public required string Catalog { get; set; }
    }
}

