using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManagerTest.UnitTests.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordiaCurriculumManagerTest.UnitTests.Models.Dossiers;

[TestClass]
public class DiscussionMessageTest
{
    [TestMethod]
    public void MarkAsDeleted_GivenCorrectAuthorId_MarksAsDeleted()
    {
        var message = TestData.GetSampleDiscussionMessage();
        var userId = message.AuthorId;

        message.MarkAsDeleted(userId);

        Assert.AreEqual(true, message.IsDeleted);
    }

    [TestMethod]
    [ExpectedException(typeof(BadRequestException))]
    public void MarkAsDeleted_GivenWrongAuthorId_MarksAsDeleted()
    {
        var message = TestData.GetSampleDiscussionMessage();
        var userId = Guid.NewGuid();

        message.MarkAsDeleted(userId);
    }
}
