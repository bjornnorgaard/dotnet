namespace Platform.Exceptions;

public class PlatformBadRequestResponse
{
    public required int Code { get; init; }
    public required string Message { get; init; }
}