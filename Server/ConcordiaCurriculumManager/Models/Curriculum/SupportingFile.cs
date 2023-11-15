namespace ConcordiaCurriculumManager.Models.Curriculum;

public class SupportingFile : BaseModel
{
    public Course? Course { get; set; }

    public required Guid CourseId { get; set; }

    public required string FileName { get; set; }

    public required string ContentBase64 { get; set; }

    public static ICollection<SupportingFile> GetSupportingFileMapping(IDictionary<string, string> files, Guid courseId)
    {
        var mapping = new List<SupportingFile>();

        foreach (var file in files)
        {
            mapping.Add(new SupportingFile { CourseId = courseId, FileName = file.Key, ContentBase64 = file.Value });
        }

        return mapping;
    }

    public static Dictionary<string, string> GetSupportingFilesDictionary(ICollection<SupportingFile> supportingFiles)
    {
        var dictionary = new Dictionary<string, string>();

        if (supportingFiles != null)
        {
            foreach (var file in supportingFiles)
            {
                dictionary[file.FileName] = file.ContentBase64;
            }
        }

        return dictionary;
    }
}
