using ConcordiaCurriculumManager.Models.Curriculum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordiaCurriculumManagerTest.UnitTests.Models;

[TestClass]
public class SupportingFileTest
{
    [TestMethod]
    public void GetFileMapping_ValidData_ReturnsCollection()
    {
        var courseId = Guid.NewGuid();
        var dict = new Dictionary<string, string>()
        {
            { "name1", "content1" },
            { "name2", "content2" }
        };

        var mapping = SupportingFile.GetSupportingFileMapping(dict, courseId);

        Assert.AreEqual(mapping.Count, dict.Count());
        Assert.AreEqual(mapping.First().CourseId, courseId);
    }

    [TestMethod]
    public void GetFileMapping_EmptyData_ReturnsEmptyCollection()
    {
        var mapping = SupportingFile.GetSupportingFileMapping(new Dictionary<string, string>(), Guid.NewGuid());

        Assert.AreEqual(mapping.Count, 0);
    }
}
