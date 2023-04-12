using Ant.Platform.Options;

namespace Todos.Options;

public class SwaggerOption : AbstractOptions
{
    public SwaggerOption(IConfiguration configuration) : base(configuration)
    {
    }

    public string ApplicationTitle { get; set; }
}