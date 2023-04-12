using Humanizer;
using Microsoft.AspNetCore.Mvc;

namespace Todos.Exceptions;

public class PlatformException : Exception
{
    public PlatformException(PlatformError error) : base(error.Humanize(LetterCasing.Sentence))
    {
        Code = error;
        Error = base.Message;
    }

    private string Error { get; }
    private PlatformError Code { get; }

    public BadRequestObjectResult ToBadRequestObjectResponse()
    {
        var response = new PlatformBadRequestResponse { Code = (int)Code, Message = Error };
        var result = new BadRequestObjectResult(response);
        return result;
    }
}