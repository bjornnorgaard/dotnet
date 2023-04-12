using Microsoft.Extensions.Configuration;

namespace Platform.Options;

public class SwaggerOption : AbstractOptions
{
    public SwaggerOption(IConfiguration configuration) : base(configuration)
    {
    }

    public string ApplicationTitle { get; init; }
}
