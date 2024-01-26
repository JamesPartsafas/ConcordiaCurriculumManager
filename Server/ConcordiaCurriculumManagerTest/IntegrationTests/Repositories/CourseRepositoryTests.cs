using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using ConcordiaCurriculumManager.Repositories;
using Microsoft.EntityFrameworkCore;
using ConcordiaCurriculumManager.Models.Curriculum;

namespace ConcordiaCurriculumManagerTest.IntegrationTests.Repositories;

[TestClass]
public class CourseRepositoryTests
{
    private static CCMDbContext dbContext = null!;
    private ICourseRepository courseRepository = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        var options = new DbContextOptionsBuilder<CCMDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        dbContext = new CCMDbContext(options);
        courseRepository = new CourseRepository(dbContext);
    }

    [ClassCleanup]
    public static void ClassCleanup() => dbContext.Dispose();

    [TestMethod]
    public async Task GetUniqueCourseSubjects_ReturnsCourses()
    {
        var id = Guid.NewGuid();
        var course = new Course
        {
            Id = id,
            CourseID = 1000,
            Subject = "SOEN",
            Catalog = "490",
            Title = "Capstone",
            Description = "Curriculum manager building simulator",
            CreditValue = "6",
            PreReqs = "SOEN 390",
            CourseNotes = "Lots of fun",
            Career = CourseCareerEnum.UGRD,
            EquivalentCourses = "",
            CourseState = CourseStateEnum.NewCourseProposal,
            Version = 1,
            Published = true,
            CourseCourseComponents = CourseCourseComponent.GetComponentCodeMapping(new Dictionary<ComponentCodeEnum, int?>
                { { ComponentCodeEnum.LEC, 3 }, { ComponentCodeEnum.WKS, 5 } },
                id
            )
        };

        dbContext.Courses.Add(course);
        await dbContext.SaveChangesAsync();

        var result = await courseRepository.GetUniqueCourseSubjects();

        Assert.IsNotNull(result);
        Assert.AreEqual(result.Count(), 1);
        Assert.AreEqual(result.First(), "SOEN");
    }

    [TestMethod]
    public async Task GetMaxCourseId_ReturnsInt()
    {
        var id = Guid.NewGuid();
        var course = new Course
        {
            Id = Guid.NewGuid(),
            CourseID = 1000,
            Subject = "SOEN",
            Catalog = "490",
            Title = "Capstone",
            Description = "Curriculum manager building simulator",
            CreditValue = "6",
            PreReqs = "SOEN 390",
            CourseNotes = "Lots of fun",
            Career = CourseCareerEnum.UGRD,
            EquivalentCourses = "",
            CourseState = CourseStateEnum.NewCourseProposal,
            Version = 1,
            Published = true,
            CourseCourseComponents = CourseCourseComponent.GetComponentCodeMapping(new Dictionary<ComponentCodeEnum, int?>
                { { ComponentCodeEnum.LEC, 3 }, { ComponentCodeEnum.WKS, 5 } },
                id
            )
        };

        dbContext.Courses.Add(course);
        await dbContext.SaveChangesAsync();

        var result = await courseRepository.GetMaxCourseId();

        Assert.IsNotNull(result);
        Assert.AreEqual(result, 1000);
    }

    [TestMethod]
    public async Task GetCourseBySubjectAndCatalog_ValidSubjectAndCatalog_ReturnsCourse()
    {
        var id = Guid.NewGuid();
        var course = new Course
        {
            Id = Guid.NewGuid(),
            CourseID = 1000,
            Subject = "SOEN",
            Catalog = "490",
            Title = "Capstone",
            Description = "Curriculum manager building simulator",
            CreditValue = "6",
            PreReqs = "SOEN 390",
            CourseNotes = "Lots of fun",
            Career = CourseCareerEnum.UGRD,
            EquivalentCourses = "",
            CourseState = CourseStateEnum.Accepted,
            Version = 1,
            Published = true,
            CourseCourseComponents = CourseCourseComponent.GetComponentCodeMapping(new Dictionary<ComponentCodeEnum, int?>
                { { ComponentCodeEnum.LEC, 3 }, { ComponentCodeEnum.WKS, 5 } },
                id
            )
        };

        dbContext.Courses.Add(course);
        await dbContext.SaveChangesAsync();

        var result = await courseRepository.GetCourseBySubjectAndCatalog("SOEN", "490");

        Assert.IsNotNull(result);
        Assert.AreEqual(result.CourseID, course.CourseID);
    }

    [TestMethod]
    public async Task SaveCourses_ValidCourse_ReturnsTrue()
    {
        var id = Guid.NewGuid();
        var course = new Course
        {
            Id = Guid.NewGuid(),
            CourseID = 1000,
            Subject = "SOEN",
            Catalog = "490",
            Title = "Capstone",
            Description = "Curriculum manager building simulator",
            CreditValue = "6",
            PreReqs = "SOEN 390",
            CourseNotes = "Lots of fun",
            Career = CourseCareerEnum.UGRD,
            EquivalentCourses = "",
            CourseState = CourseStateEnum.Accepted,
            Version = 1,
            Published = true,
            CourseCourseComponents = CourseCourseComponent.GetComponentCodeMapping(new Dictionary<ComponentCodeEnum, int?>
                { { ComponentCodeEnum.LEC, 3 }, { ComponentCodeEnum.WKS, 5 } },
                id
            )
        };

        var result = await courseRepository.SaveCourse(course);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task GetCourseByCourseId_ValidId_ReturnsCourse()
    {
        var id = Guid.NewGuid();
        var course = new Course
        {
            Id = Guid.NewGuid(),
            CourseID = 1000,
            Subject = "SOEN",
            Catalog = "490",
            Title = "Capstone",
            Description = "Curriculum manager building simulator",
            CreditValue = "6",
            PreReqs = "SOEN 390",
            CourseNotes = "Lots of fun",
            Career = CourseCareerEnum.UGRD,
            EquivalentCourses = "",
            CourseState = CourseStateEnum.Accepted,
            Version = 1,
            Published = true,
            CourseCourseComponents = CourseCourseComponent.GetComponentCodeMapping(new Dictionary<ComponentCodeEnum, int?>
                { { ComponentCodeEnum.LEC, 3 }, { ComponentCodeEnum.WKS, 5 } },
                id
            )
        };

        dbContext.Courses.Add(course);
        await dbContext.SaveChangesAsync();

        var result = await courseRepository.GetCourseByCourseId(course.CourseID);

        Assert.IsNotNull(result);
        Assert.AreEqual(result.CourseID, course.CourseID);
    }

    [TestMethod]
    public async Task GetCourseByCourseIdAndLatestVersion_ReturnsCourseWithLatestVersion()
    {
        var id1 = Guid.NewGuid();
        var id2 = Guid.NewGuid();
        var course1 = new Course
        {
            Id = Guid.NewGuid(),
            CourseID = 1000,
            Subject = "SOEN",
            Catalog = "490",
            Title = "Capstone",
            Description = "Curriculum manager building simulator",
            CreditValue = "6",
            PreReqs = "SOEN 390",
            Career = CourseCareerEnum.UGRD,
            EquivalentCourses = "",
            CourseState = CourseStateEnum.Accepted,
            Version = 3,
            Published = true,
            CourseCourseComponents = CourseCourseComponent.GetComponentCodeMapping(new Dictionary<ComponentCodeEnum, int?>
                { { ComponentCodeEnum.LEC, 3 }, { ComponentCodeEnum.WKS, 5 } },
                id1
            )
        };

        var course2 = new Course
        {
            Id = Guid.NewGuid(),
            CourseID = 1000,
            Subject = "SOEN",
            Catalog = "490",
            Title = "Capstone",
            Description = "Curriculum manager building simulator",
            CreditValue = "6",
            PreReqs = "SOEN 390",
            Career = CourseCareerEnum.UGRD,
            EquivalentCourses = "",
            CourseState = CourseStateEnum.Deleted,
            Version = 4,
            Published = true,
            CourseCourseComponents = CourseCourseComponent.GetComponentCodeMapping(new Dictionary<ComponentCodeEnum, int?>
                { { ComponentCodeEnum.LEC, 3 }, { ComponentCodeEnum.WKS, 5 } },
                id2
            )
        };

        dbContext.Courses.Add(course1);
        dbContext.Courses.Add(course2);
        await dbContext.SaveChangesAsync();

        var result = await courseRepository.GetCourseByCourseIdAndLatestVersion(course1.CourseID);

        Assert.IsNotNull(result);
        Assert.AreEqual(course1.CourseID, result.CourseID);
        Assert.AreEqual(course1.Version, result.Version);
    }

    [TestMethod]
    public async Task GetCourseWithSupportingFilesBySubjectAndCatalog_ValidSubjectAndCatalog_ReturnsCourseWithSupportingFiles()
    {
        var id = Guid.NewGuid();
        var course = new Course
        {
            Id = Guid.NewGuid(),
            CourseID = 1000,
            Subject = "SOEN",
            Catalog = "490",
            Title = "Capstone",
            Description = "Curriculum manager building simulator",
            CreditValue = "6",
            PreReqs = "SOEN 390",
            CourseNotes = "Lots of fun",
            Career = CourseCareerEnum.UGRD,
            EquivalentCourses = "",
            CourseState = CourseStateEnum.Accepted,
            Version = 1,
            Published = true,
            CourseCourseComponents = CourseCourseComponent.GetComponentCodeMapping(new Dictionary<ComponentCodeEnum, int?>
                { { ComponentCodeEnum.LEC, 3 }, { ComponentCodeEnum.WKS, 5 } },
                id
            ),
            SupportingFiles = SupportingFile.GetSupportingFileMapping(new Dictionary<string, string>
                { { "test.pdf", "test"} },
                id
            ),
        };

        dbContext.Courses.Add(course);
        await dbContext.SaveChangesAsync();

        var result = await courseRepository.GetCourseWithSupportingFilesBySubjectAndCatalog("SOEN", "490");

        Assert.IsNotNull(result);
        Assert.AreEqual(result.CourseID, course.CourseID);
        Assert.IsNotNull(result.CourseCourseComponents);
        Assert.IsNotNull(result.SupportingFiles);
    }

    [TestMethod]
    public async Task GetCourseInProposalBySubjectAndCatalog_ValidSubjectAndCatalog_ReturnsCourse()
    {
        var id = Guid.NewGuid();
        var course = new Course
        {
            Id = Guid.NewGuid(),
            CourseID = 1000,
            Subject = "SOEN",
            Catalog = "490",
            Title = "Capstone",
            Description = "Curriculum manager building simulator",
            CreditValue = "6",
            PreReqs = "SOEN 390",
            CourseNotes = "Lots of fun",
            Career = CourseCareerEnum.UGRD,
            EquivalentCourses = "",
            CourseState = CourseStateEnum.NewCourseProposal,
            Version = 1,
            Published = true,
            CourseCourseComponents = CourseCourseComponent.GetComponentCodeMapping(new Dictionary<ComponentCodeEnum, int?>
                { { ComponentCodeEnum.LEC, 3 }, { ComponentCodeEnum.WKS, 5 } },
                id
            )
        };

        dbContext.Courses.Add(course);
        await dbContext.SaveChangesAsync();

        var result = await courseRepository.GetCourseInProposalBySubjectAndCatalog("SOEN", "490");

        Assert.IsNotNull(result);
        Assert.AreEqual(result.CourseID, course.CourseID);
    }

    [TestMethod]
    public async Task GetCourseByIdAsync_ValidId_ReturnsCourse()
    {
        var id = Guid.NewGuid();
        var expectedCourse = new Course
        {
            Id = id,
            CourseID = 1000,
            Subject = "SOEN",
            Catalog = "490",
            Title = "Capstone",
            Description = "Curriculum manager building simulator",
            CreditValue = "6",
            PreReqs = "SOEN 390",
            CourseNotes = "Lots of fun",
            Career = CourseCareerEnum.UGRD,
            EquivalentCourses = "",
            CourseState = CourseStateEnum.NewCourseProposal,
            Version = 1,
            Published = true,
            CourseCourseComponents = CourseCourseComponent.GetComponentCodeMapping(new Dictionary<ComponentCodeEnum, int?>
                    { { ComponentCodeEnum.LEC, 3 }, { ComponentCodeEnum.WKS, 5 } },
                    id
                )
        };

        dbContext.Courses.Add(expectedCourse);
        await dbContext.SaveChangesAsync();

        var result = await courseRepository.GetCourseByIdAsync(expectedCourse.Id);

        Assert.IsNotNull(result, "No course was returned from the repository");
        Assert.AreEqual(expectedCourse.Id, result.Id, "The ID of the returned course does not match the expected ID");
        Assert.AreEqual(expectedCourse.Subject, result.Subject, "The Subject of the returned course does not match the expected Subject");
    }

    [TestMethod]
    public async Task GetCourseByIdAsync_InvalidId_ReturnsNull()
    {
        var nonExistentId = Guid.NewGuid();

        var result = await courseRepository.GetCourseByIdAsync(nonExistentId);

        Assert.IsNull(result, "A course was returned for a non-existent ID");
    }

    [TestMethod]
    public async Task GetCoursesBySubjectAsync_ReturnsCoursesForSubject()
    {
        var subjectCode = "SOEN";
        var id = Guid.NewGuid();
        var courses = new List<Course>
        {
            new Course {
                Id = id,
                CourseID = 1000,
                Subject = "SOEN",
                Catalog = "490",
                Title = "Capstone",
                Description = "Curriculum manager building simulator",
                CreditValue = "6",
                PreReqs = "SOEN 390",
                CourseNotes = "Lots of fun",
                Career = CourseCareerEnum.UGRD,
                EquivalentCourses = "",
                CourseState = CourseStateEnum.NewCourseProposal,
                Version = 1,
                Published = true,
                CourseCourseComponents = CourseCourseComponent.GetComponentCodeMapping(new Dictionary<ComponentCodeEnum, int?>
                    { { ComponentCodeEnum.LEC, 3 }, { ComponentCodeEnum.WKS, 5 } },
                    id
                )
            }
        };

        dbContext.Courses.AddRange(courses);
        await dbContext.SaveChangesAsync();

        var result = await courseRepository.GetCoursesBySubjectAsync(subjectCode);

        Assert.AreEqual(1, result.Count());
        Assert.IsTrue(result.All(c => c.Subject == subjectCode));
    }
}
