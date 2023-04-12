using Ant.Platform.Options;

namespace Todos.Options;

public class LoggingOption : AbstractOptions
{
    public string ApplicationName { get; set; }

    public LoggingOption(IConfiguration configuration) : base(configuration)
    {
    }
}
