using Platform.Options;

namespace Todos.Options;

public class DatabaseOptions : AbstractOptions
{
    public string ConnectionString { get; init; } = null!;

    public DatabaseOptions(IConfiguration configuration) : base(configuration)
    {
    }
}
