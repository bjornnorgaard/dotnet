using Platform.Options;

namespace Todos.Options;

public class FacebookOptions : AbstractOptions
{
    public string AppId { get; set; } = null!;
    public string AppSecret { get; set; } = null!;
    public string RedirectUri { get; set; } = null!;

    public FacebookOptions(IConfiguration configuration) : base(configuration)
    {
    }
}
