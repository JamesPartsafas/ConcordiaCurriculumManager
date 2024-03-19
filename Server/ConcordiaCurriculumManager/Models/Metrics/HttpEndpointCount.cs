namespace ConcordiaCurriculumManager.Models.Metrics;

public class HttpEndpointCount
{
    public required string Controller { get; set; }
    public required string Endpoint { get; set; }
    public required string FullPath { get; set; }
    public required int Count { get; set; }
}
