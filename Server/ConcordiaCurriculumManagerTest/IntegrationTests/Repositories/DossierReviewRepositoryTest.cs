﻿using Microsoft.EntityFrameworkCore;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories;
using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using ConcordiaCurriculumManagerTest.UnitTests.UtilityFunctions;

namespace ConcordiaCurriculumManagerTest.IntegrationTests.Repositories;

[TestClass]
public class DossierReviewRepositoryTest
{
    private static CCMDbContext dbContext = null!;
    private IDossierReviewRepository dossierReviewRepository = null!;

    [ClassInitialize]
    public static void ClassInitialize(TestContext _)
    {
        var options = new DbContextOptionsBuilder<CCMDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        dbContext = new CCMDbContext(options);
    }

    [ClassCleanup]
    public static void ClassCleanup() => dbContext.Dispose();

    [TestInitialize]
    public void TestInitialize()
    {
        dossierReviewRepository = new DossierReviewRepository(dbContext);
    }


    [TestMethod]
    public async Task GetDossierWithApprovalStagesAndRequestsAndDiscussion_ValidId_ReturnsDossierWithRelatedData()
    {
        var user = TestData.GetSampleUser();

        var dossier = new Dossier
        {
            Id = Guid.NewGuid(),
            Initiator = user,
            InitiatorId = user.Id,
            Title = "test title",
            Description = "test description",
            State = DossierStateEnum.Created,
            ApprovalStages = new List<ApprovalStage>(),
            CourseCreationRequests = new List<CourseCreationRequest>(),
            CourseModificationRequests = new List<CourseModificationRequest>(),
            CourseDeletionRequests = new List<CourseDeletionRequest>(),
            Discussion = new DossierDiscussion
            {
                DossierId = Guid.NewGuid(),
                Messages = new List<DiscussionMessage>()
            }
        };

        var otherDossier = TestData.GetSampleDossier();

        dbContext.Users.Add(user);
        dbContext.Dossiers.Add(dossier);
        dbContext.Dossiers.Add(otherDossier);
        await dbContext.SaveChangesAsync();

        var result = await dossierReviewRepository.GetDossierWithApprovalStagesAndRequestsAndDiscussion(dossier.Id);

        Assert.IsNotNull(result);
        Assert.IsNotNull(result.ApprovalStages);
        Assert.IsNotNull(result.CourseDeletionRequests);
        Assert.IsNotNull(result.CourseCreationRequests);
        Assert.IsNotNull(result.CourseModificationRequests);
        Assert.IsNotNull(result.Discussion);
        Assert.AreEqual(result.Id, dossier.Id);
    }
}
