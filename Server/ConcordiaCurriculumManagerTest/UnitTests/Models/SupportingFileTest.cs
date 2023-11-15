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


    [TestMethod]
    public void GetSupportingFilesDictionary_ValidData_ReturnsDictionary()
    {
        var supportingFile = new List<SupportingFile>
        {
            new SupportingFile
            {
                CourseId = Guid.NewGuid(),
                FileName = "file1.pdf",
                ContentBase64 = "JVBERi0xLjQKJeLjz9MKMyAwIG9iago8PAovVHlwZSAvUGFn...",
            }
        };

        var mapping = SupportingFile.GetSupportingFilesDictionary(supportingFile);
        Assert.IsTrue(mapping.ContainsKey("file1.pdf"));
        Assert.IsTrue(mapping.ContainsValue("JVBERi0xLjQKJeLjz9MKMyAwIG9iago8PAovVHlwZSAvUGFn..."));
    }

    [TestMethod]
    public void GetSupportingFilesDictionary_EmptyData_ReturnsEmptyDictionary()
    {
        var mapping = SupportingFile.GetSupportingFilesDictionary(new List<SupportingFile>());

        Assert.AreEqual(0, mapping.Count);
    }
}
