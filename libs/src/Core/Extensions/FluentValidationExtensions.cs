using FluentValidation;

namespace Fwks.Core.Extensions;

public static class FluentValidationExtensions
{
    public static IRuleBuilderOptions<T, TProperty> PhoneNumber<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
    {
        return ruleBuilder.Must(property => property switch
        {
            string value when value.IsNotEmpty() && value.StartsWith('+') => true,
            _ => false

        }).WithMessage("'{PropertyName}' is not a valid phone number. It should start with '+' before country code.");
    }
}