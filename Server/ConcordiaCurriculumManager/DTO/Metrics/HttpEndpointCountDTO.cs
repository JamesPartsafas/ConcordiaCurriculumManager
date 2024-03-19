namespace ConcordiaCurriculumManager.DTO.Metrics;

public class HttpEndpointCountDTO
{
    public required string Controller { get; set; }
    public required string Endpoint { get; set; }
    public required string FullPath { get; set; }
    public required int Count { get; set; }
}

public class HttpEndpointCountWrapperDTO
{
    public required IList<HttpEndpointCountDTO> Result { get; set; }
    public required int NextIndex { get; set; }
}