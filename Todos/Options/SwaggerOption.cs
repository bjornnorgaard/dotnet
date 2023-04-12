using Ant.Platform.Options;

namespace Todos.Options;

public class SwaggerOption : AbstractOptions
{
    public string ApplicationTitle { get; set; }

    public SwaggerOption(IConfiguration configuration) : base(configuration)
    {
    }
}
