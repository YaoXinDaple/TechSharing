using FluentValidation;
using MediatR;

namespace MediatRPresentation.Abstraction;

internal sealed class ValidationBehaviors<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>>? _validator;

    public ValidationBehaviors(IEnumerable<IValidator<TRequest>> validator)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator is null || !_validator.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var validationErrors = _validator
            .Select(validator => validator.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(failure => failure != null)
            .ToList();

        if (validationErrors.Any())
        {
            throw new ValidationException(validationErrors);
        }
        return await next();
    }
}
