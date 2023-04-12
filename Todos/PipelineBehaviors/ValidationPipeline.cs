using FluentValidation;
using MediatR;

namespace Todos.PipelineBehaviors;

public class ValidationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<ValidationPipeline<TRequest, TResponse>> _logger;
    private readonly IValidator<TRequest>? _validator;

    public ValidationPipeline(
        ILogger<ValidationPipeline<TRequest, TResponse>> logger,
        IValidator<TRequest>? validator = null)
    {
        _logger = logger;
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validator == null) return await next();

        var result = await _validator.ValidateAsync(request, cancellationToken);

        if (!result.IsValid)
        {
            var featureName = request.GetType().FullName?.Split(".").Last().Split("+").First();

            _logger.LogWarning("Validation failed for {FeatureName} {@FeatureCommand}", featureName, request);
            throw new ValidationException(result.Errors);
        }

        return await next();
    }
}