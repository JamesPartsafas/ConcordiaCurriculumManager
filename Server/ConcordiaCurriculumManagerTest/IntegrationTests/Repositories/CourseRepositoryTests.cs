using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using ConcordiaCurriculumManager.Repositories;
using Microsoft.EntityFrameworkCore;
using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManagerTest.UnitTests.UtilityFunctions;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;

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
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
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

    [TestMethod]
    public async Task UpdateCourse_ReturnsTrue()
    {
        var course = TestData.GetSampleAcceptedCourse();
        course.MarkAsPublished();
        await courseRepository.SaveCourse(course);

        var result = await courseRepository.UpdateCourse(course);

        Assert.AreEqual(true, course.Published);
        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task UpdateCourseReferences_ValidInput_ReturnsTrueAndUpdatesAllReferences()
    {
        var oldCourse = TestData.GetSampleAcceptedCourse();
        var newCourse = TestData.GetSampleAcceptedCourse();
        var randomCourse1 = TestData.GetSampleAcceptedCourse();
        var randomCourse2 = TestData.GetSampleAcceptedCourse();

        newCourse.Subject = oldCourse.Subject;
        newCourse.Catalog = oldCourse.Catalog;
        newCourse.Version = oldCourse.Version + 1;

        randomCourse2.Subject = "COMP";
        randomCourse2.Catalog = "560T";
        randomCourse2.Published = true;

        var newCourseReferences = new List<(string Subject, string Catalog)>
        {
            (randomCourse2.Subject, randomCourse2.Catalog)
        };

        var references = new List<CourseReference>
        {
            new() {
                CourseReferenced = oldCourse,
                CourseReferencedId = oldCourse.Id,
                CourseReferencing = randomCourse1,
                CourseReferencingId = randomCourse1.Id,
                State = CourseReferenceEnum.UpToDate
            }
        };

        await dbContext.Courses.AddRangeAsync(new List<Course> { oldCourse, newCourse, randomCourse1, randomCourse2 });
        await dbContext.CourseReferences.AddRangeAsync(references);
        await dbContext.SaveChangesAsync();

        var result = await courseRepository.UpdateCourseReferences(oldCourse, newCourse, newCourseReferences);

        Assert.IsTrue(result);

        var oldUpdatedReferences = await dbContext.CourseReferences
            .Where(c => c.CourseReferencedId.Equals(oldCourse.Id) || c.CourseReferencingId.Equals(oldCourse.Id))
            .ToListAsync();

        var newUpdatedReferences = await dbContext.CourseReferences
            .Where(c => c.CourseReferencedId.Equals(newCourse.Id) || c.CourseReferencingId.Equals(newCourse.Id))
            .ToListAsync();

        oldUpdatedReferences.ForEach(r => Assert.AreEqual(CourseReferenceEnum.OutOfDate, r.State));

        foreach (var reference in newUpdatedReferences)
        {
            if (reference.CourseReferencedId.Equals(newCourse.Id))
            {
                Assert.AreEqual(randomCourse1.Id, reference.CourseReferencingId);
            }
            else
            {
                Assert.AreEqual(randomCourse2.Id, reference.CourseReferencedId);
            }

            Assert.AreEqual(CourseReferenceEnum.UpToDate, reference.State);
        }
    }

    [TestMethod]
    public async Task InvalidateAllCourseReferences_ValidInput_ReturnsTrueAndSetsStatusToOutOfDate()
    {
        var firstCourse = TestData.GetSampleAcceptedCourse();
        var secondCourse = TestData.GetSampleAcceptedCourse();
        var thirdCourse = TestData.GetSampleAcceptedCourse();

        var references = new List<CourseReference>() {
            new()
            {
                CourseReferenced = firstCourse,
                CourseReferencedId = firstCourse.Id,
                CourseReferencing = secondCourse,
                CourseReferencingId = secondCourse.Id,
                State = CourseReferenceEnum.UpToDate
            },
            new()
            {
                CourseReferenced = thirdCourse,
                CourseReferencedId = thirdCourse.Id,
                CourseReferencing = firstCourse,
                CourseReferencingId = firstCourse.Id,
                State = CourseReferenceEnum.UpToDate
            },
        };

        await dbContext.Courses.AddRangeAsync(new List<Course>() { firstCourse, secondCourse, thirdCourse });
        await dbContext.CourseReferences.AddRangeAsync(references);
        await dbContext.SaveChangesAsync();

        var result = await courseRepository.InvalidateAllCourseReferences(firstCourse.Id);

        var newReferences = await dbContext.CourseReferences
               .Where(c => (c.CourseReferencedId.Equals(firstCourse.Id) || c.CourseReferencingId.Equals(firstCourse.Id)))
               .ToListAsync();

        Assert.IsTrue(result);
        foreach (var reference in newReferences)
        {
            var originalReference = references.FirstOrDefault(r => r.CourseReferencedId.Equals(reference.CourseReferencedId)
                                                                   && r.CourseReferencingId.Equals(reference.CourseReferencingId));

            Assert.IsNotNull(originalReference);
            Assert.AreEqual(CourseReferenceEnum.OutOfDate, reference.State);
        }
    }
}
