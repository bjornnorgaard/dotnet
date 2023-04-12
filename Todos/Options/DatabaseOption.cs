using Ant.Platform.Options;

namespace Todos.Options;

public class DatabaseOption : AbstractOptions
{
    public  string ConnectionString { get; init; }

    public DatabaseOption(IConfiguration configuration) : base(configuration)
    {
    }
}
