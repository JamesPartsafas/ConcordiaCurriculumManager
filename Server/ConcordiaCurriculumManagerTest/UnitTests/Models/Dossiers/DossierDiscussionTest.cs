using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;
using ConcordiaCurriculumManagerTest.UnitTests.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordiaCurriculumManagerTest.UnitTests.Models.Dossiers;

[TestClass]
public class DossierDiscussionTest
{
    [TestMethod]
    public void DeleteMessage_GivenValidMessageId_Deletes()
    {
        var message = TestData.GetSampleDiscussionMessage();
        var discussion = TestData.GetSampleDossierDiscussion();

        discussion.Messages = new List<DiscussionMessage> { message };

        discussion.DeleteMessage(message.Id, message.AuthorId);

        Assert.AreEqual(true, message.IsDeleted);
    }

    [TestMethod]
    [ExpectedException(typeof(BadRequestException))]
    public void MarkAsDeleted_GivenWrongAuthorId_MarksAsDeleted()
    {
        var message = TestData.GetSampleDiscussionMessage();
        var discussion = TestData.GetSampleDossierDiscussion();

        discussion.Messages = new List<DiscussionMessage> { message };

        discussion.DeleteMessage(Guid.NewGuid(), Guid.NewGuid());
    }
}
