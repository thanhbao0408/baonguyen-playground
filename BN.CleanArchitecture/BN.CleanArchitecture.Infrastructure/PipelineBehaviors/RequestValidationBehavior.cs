using System.Diagnostics;
using System.Text.Json;
using BN.CleanArchitecture.Infrastructure.Validator;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ValidationException = FluentValidation.ValidationException;

namespace BN.CleanArchitecture.Infrastructure.PipelineBehaviors;

// TODO: implement https://codewithmukesh.com/blog/mediatr-pipeline-behaviour/
// [DebuggerStepThrough]
public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger<RequestValidationBehavior<TRequest, TResponse>> _logger;

    public RequestValidationBehavior(IEnumerable<IValidator<TRequest>>? validators,
        ILogger<RequestValidationBehavior<TRequest, TResponse>> logger)
    {
        _validators = validators ?? throw new ArgumentNullException(nameof(validators));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<TResponse> Handle(TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        _logger.LogInformation(
            "[{Prefix}] Handle request={X-RequestData} and response={X-ResponseData}",
            nameof(RequestValidationBehavior<TRequest, TResponse>), typeof(TRequest).Name, typeof(TResponse).Name);

        _logger.LogDebug($"Handling {typeof(TRequest).FullName} with content {JsonSerializer.Serialize(request)}");

        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            //var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var validationResults = await Task.WhenAll(_validators.Select(v => v.HandleValidation(request)));
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();
            if (failures.Count != 0)
                throw new ValidationException(failures);
        }
        TResponse? response = await next();

        _logger.LogInformation($"Handled {typeof(TRequest).FullName}");
        return response;
    
    }
}