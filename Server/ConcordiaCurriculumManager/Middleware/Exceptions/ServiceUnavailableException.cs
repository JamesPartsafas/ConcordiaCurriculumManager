namespace ConcordiaCurriculumManager.Middleware.Exceptions;

public class ServiceUnavailableException : Exception
{
    public ServiceUnavailableException()
    {
    }

    public ServiceUnavailableException(string message)
        : base(message)
    {
    }
}
