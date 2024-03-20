namespace ConcordiaCurriculumManager.DTO.Metrics;

public class HttpStatusCountDTO
{
    public required int Count { get; set; }
    public required int HttpStatus { get; set; }
}

public class HttpStatusCountWrapperDTO
{
    public required IList<HttpStatusCountDTO> Result { get; set; }
    public required int NextIndex { get; set; }
}
