using Microsoft.Extensions.Configuration;

namespace Platform.Options;

public class CorsOptions : AbstractOptions
{
    public string[] AllowedOrigins { get; set; } = null!;

    public CorsOptions(IConfiguration configuration) : base(configuration)
    {
    }
}
