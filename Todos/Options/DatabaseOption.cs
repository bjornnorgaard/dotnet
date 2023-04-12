using Ant.Platform.Options;

namespace Todos.Options;

public class DatabaseOption : AbstractOptions
{
    public DatabaseOption(IConfiguration configuration) : base(configuration)
    {
    }

    public string ConnectionString { get; init; }
}