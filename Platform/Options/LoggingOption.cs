using Microsoft.Extensions.Configuration;

namespace Platform.Options;

public class LoggingOption : AbstractOptions
{
    public LoggingOption(IConfiguration configuration) : base(configuration)
    {
    }

    public string ApplicationName { get; set; }
}