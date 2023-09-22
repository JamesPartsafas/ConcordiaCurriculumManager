using ConcordiaCurriculumManager.Models.Curriculum;
using CsvHelper;
using System.Globalization;
using System.Reflection;

namespace ConcordiaCurriculumManager.Repositories.DatabaseContext.Seeding;

public class CourseSeeder
{
    public void SeedCourseData(List<Course> courses, List<(Guid CoursesId, Guid CourseComponentsId)> courseCourseComponents, List<CourseComponent> courseComponents)
    {
        using (var courseReader = new StreamReader(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Repositories/DatabaseContext/Seeding/Courses.csv")))
        using (var courseDescriptionReader = new StreamReader(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Repositories/DatabaseContext/Seeding/CourseDescriptions.csv")))
        using (var courseCsvFile = new CsvReader(courseReader, CultureInfo.InvariantCulture))
        using (var courseDescriptionCsvFile = new CsvReader(courseDescriptionReader, CultureInfo.InvariantCulture))
        {
            IEnumerable<CourseCsv> coursesCsv = courseCsvFile.GetRecords<CourseCsv>();
            IEnumerable<CourseDescriptionCsv> courseDescriptionsCsv = courseDescriptionCsvFile.GetRecords<CourseDescriptionCsv>();

            var coursesCsvGrouping = coursesCsv.GroupBy(courseCsv => courseCsv.CourseID);

            foreach (var courseCsvGroup in coursesCsvGrouping)
            {
                var courseCsvList = courseCsvGroup.ToList();
                var descriptionCsv = courseDescriptionsCsv.FirstOrDefault(courseDescriptionCsv => courseDescriptionCsv.CourseID == courseCsvList[0].CourseID);
                var course = new Course
                {
                    Id = Guid.NewGuid(),
                    CourseID = Int32.Parse(courseCsvList[0].CourseID),
                    Subject = courseCsvList[0].Subject,
                    Catalog = courseCsvList[0].Catalog,
                    Title = courseCsvList[0].LongTitle,
                    Description = descriptionCsv != null ? descriptionCsv.Descr : "",
                    CreditValue = courseCsvList[0].ClassUnits,
                    PreReqs = courseCsvList[0].PreRequisiteDescription,
                    Career = Enum.Parse<CourseCareerEnum>(courseCsvList[0].Career),
                    EquivalentCourses = courseCsvList[0].EquivalentCourses,
                    CourseState = CourseStateEnum.Accepted,
                    Version = 1,
                    Published = true,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                };

                var courseComponentsForCourse = new List<CourseComponent>();

                foreach (var courseCsv in courseCsvList)
                {
                    ComponentCodeEnum code = Enum.Parse<ComponentCodeEnum>(courseCsv.ComponentCode);
                    courseComponentsForCourse.Add(new CourseComponent { ComponentCode = code, ComponentName = ComponentCodeMapping.GetComponentCodeMapping(code) });
                }

                courses.Add(course);

                courseComponents.Join(courseComponentsForCourse,
                    courseComponent => courseComponent.ComponentCode,
                    cCourseComponent => cCourseComponent.ComponentCode,
                    (courseComponent, _) => courseComponent)
                    .ToList()
                    .ForEach(courseComponent =>
                    {
                        // Check that data has not already been inserted, because of OnModelCreating being called twice
                        if (!courseCourseComponents.Any(ccc => ccc.CoursesId == course.Id && ccc.CourseComponentsId == courseComponent.Id))
                        {
                            courseCourseComponents.Add((course.Id, courseComponent.Id));
                        }
                    });
            }
        }
    }

    private class CourseCsv
    {
        public required string CourseID { get; set; }

        public required string Subject { get; set; }

        public required string Catalog { get; set; }

        public required string LongTitle { get; set; }

        public required string ClassUnits { get; set; }

        public required string ComponentCode { get; set; }

        public required string ComponentDescr { get; set; }

        public required string PreRequisiteDescription { get; set; }

        public required string Career { get; set; }

        public required string EquivalentCourses { get; set; }
    }

    private class CourseDescriptionCsv
    {
        public required string CourseID { get; set; }

        public required string Descr { get; set; }
    }
}
