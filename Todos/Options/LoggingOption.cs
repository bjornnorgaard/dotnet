using Ant.Platform.Options;

namespace Todos.Options;

public class LoggingOption : AbstractOptions
{
    public LoggingOption(IConfiguration configuration) : base(configuration)
    {
    }

    public string ApplicationName { get; set; }
}