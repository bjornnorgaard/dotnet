using Microsoft.Extensions.Configuration;

namespace Platform.Options;

public class LoggingOptions : AbstractOptions
{
    public string ApplicationName { get; init; }

    public LoggingOptions(IConfiguration configuration) : base(configuration)
    {
    }
}
