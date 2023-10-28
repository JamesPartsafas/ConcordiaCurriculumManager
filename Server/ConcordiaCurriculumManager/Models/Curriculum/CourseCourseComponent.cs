﻿namespace ConcordiaCurriculumManager.Models.Curriculum;

public class CourseCourseComponent : BaseModel
{
    public CourseComponent? CourseComponent { get; set; }

    public required ComponentCodeEnum ComponentCode { get; set; }

    public Course? Course { get; set; }

    public required Guid CourseId { get; set; }

    public int? HoursPerWeek { get; set; }

    public static ICollection<CourseCourseComponent> GetComponentCodeMapping(IDictionary<ComponentCodeEnum, int?> codes, Guid courseId)
    {
        var mapping = new List<CourseCourseComponent>();

        foreach (var code in codes)
        {
            mapping.Add(new CourseCourseComponent { ComponentCode = code.Key, CourseId = courseId, HoursPerWeek = code.Value });
        }

        return mapping;
    }
}