namespace ConcordiaCurriculumManager.Models.Metrics;

public class HttpMetric : BaseModel
{
    public required string Controller { get; set; }
    public required string Endpoint { get; set; }
    public required int ResponseStatusCode { get; set; }
    public required long ResponseTimeMilliSecond { get; set; }
}
