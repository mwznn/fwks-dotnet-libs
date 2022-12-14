using System;
using System.Globalization;
using System.Linq;
using System.Text;
using Fwks.Core.Exceptions;
using Fwks.Core.Constants;

namespace Fwks.Core.Extensions;

public static partial class StringExtensions
{
    public static bool IsEmpty(this string value)
    {
        return string.IsNullOrWhiteSpace(value);
    }

    public static bool IsNotEmpty(this string value)
    {
        return !value.IsEmpty();
    }

    public static bool IsEqualTo(this string left, string right, StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase)
    {
        return left.Equals(right, comparisonType);
    }

    public static string RemoveDiacritics(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return text;

        text = text.Normalize(NormalizationForm.FormD);

        var chars = text.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();

        return new string(chars).Normalize(NormalizationForm.FormC);
    }

    public static long FormatInBytes(this string format)
    {
        var values = RegexPatterns.ByteTransform()
            .Matches(format.ToLowerInvariant())
            .FirstOrDefault()?.Groups.Values
            .Skip(1)
            .Select(x => x.Value.ToLowerInvariant())
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();

        if (values == default || values.Length != 2)
            throw new FormatConversionException(format);

        if (!double.TryParse(values[0], out var value))
            throw new FormatConversionException(format);

        return (long)(value * Math.Pow(1024, GetUnit()));

        int GetUnit()
        {
            return values[1] switch
            {
                "b" => 0,
                "kb" => 1,
                "mb" => 2,
                "gb" => 3,
                "tb" => 4,
                _ => throw new FormatConversionException(values[1])
            };
        }
    }

    public static string RemoveSpaces(this string value)
    {
        return value.Replace(" ", string.Empty);
    }

    public static string SpaceTitleCase(this string value)
    {
        return RegexPatterns.TitleCase().Replace(value, " $1").Trim();
    }

    public static string ToPascalCase(this string value)
    {
        return RegexPatterns.PascalCase().Replace(value.Trim(), match => match.Groups[1].Value.ToUpper());
    }

    public static string ToCamelCase(this string value)
    {
        value = value.ToPascalCase().Trim();

        return value.Length > 0 ? value[..1].ToLower() + value[1..] : value;
    }

    public static string ToSlugCase(this string value, bool lowerCase = true)
    {
        return ToSlugSnakeCase(value.Trim(), "-", lowerCase);
    }

    public static string ToSnakeCase(this string value, bool lowerCase = true)
    {
        return ToSlugSnakeCase(value.Trim(), "_", lowerCase);
    }

    private static string ToSlugSnakeCase(string value, string character, bool toLower)
    {
        value = RegexPatterns.SlugSnakeStepOne().Replace(value, $"$1{character}$2");

        value = RegexPatterns.SlugSnakeStepTwo().Replace(value, $"$1{character}$2");

        value = RegexPatterns.SlugSnakeStepThree().Replace(value, character);

        return toLower ? value.ToLower() : value;
    }
}