using System.ComponentModel.DataAnnotations;

namespace ConcordiaCurriculumManager.Models.Curriculum;

public class CourseReference
{
    [Key]
    public int CourseID { get; set; }

    public Course CourseReferencing { get; set; }

    public ICollection<Course> CourseReferenced { get; set; } = new List<Course>();

}