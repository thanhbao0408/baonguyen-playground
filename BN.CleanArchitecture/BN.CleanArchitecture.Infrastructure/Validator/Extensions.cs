using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;

namespace BN.CleanArchitecture.Infrastructure.Validator;

public static class Extensions
{
    private static ValidationResultModel ToValidationResultModel(this ValidationResult validationResult)
    {
        return new ValidationResultModel(validationResult);
    }

    /// <summary>
    /// Ref https://www.jerriepelser.com/blog/validation-response-aspnet-core-webapi
    /// </summary>
    public static async Task<ValidationResult> HandleValidation<TRequest>(this IValidator<TRequest> validator, TRequest request)
    {
        ValidationResult? validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.ToValidationResultModel());
        }
        return validationResult;
    }

    public static IServiceCollection AddCustomValidators(this IServiceCollection services, Type[] types)
    {
        return services.Scan(scan => scan
            .FromAssembliesOf(types)
            .AddClasses(c => c.AssignableTo(typeof(IValidator<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());
    }
}