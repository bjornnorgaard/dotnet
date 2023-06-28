using Platform.Options;

namespace Todos.Options;

public class JwtOptions : AbstractOptions
{
    public string Secret { get; set; } = null!;
    public int ExpirationInMinutes { get; set; }
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;

    public JwtOptions(IConfiguration configuration) : base(configuration)
    {
    }
}
