using FluentAssertions;
using Fwks.Core.Exceptions;
using Fwks.Core.Extensions;
using Fwks.Tests.Core.Extensions.MemberData;
using Fwks.Tests.Shared.Configuration;
using Xunit;

namespace Fwks.Tests.Core.Extensions;

[Trait(TestsConfiguration.TRAITS_UNIT_TESTS, TestsConfiguration.PROJECTS_CORE)]
public sealed class StringExtensionsTests
{
    [Fact(DisplayName = "RemoveDiacritics() should remove replace characters with accents with plain values.")]
    public void RemoveDiacritics()
    {
        var expected = "aaaaaeeeeiiiiooooouuuu";

        var actual = "áàãâäéèêëíìîïóòõôöúùûü".RemoveDiacritics();

        actual.Should().Be(expected);
    }

    [Theory(DisplayName = "FormatInBytes() when valid, should return a valid response with no errors.")]
    [MemberData(nameof(StringExtensionsMemberData.FormatInBytesValid), MemberType = typeof(StringExtensionsMemberData))]
    public void TransformBytes_Valid(string format, long expectedBytes)
    {
        var actual = format.FormatInBytes();

        actual.Should().Be(expectedBytes);
    }

    [Theory(DisplayName = "FormatInBytes() when invalid, should throw an FormatConversionException.")]
    [MemberData(nameof(StringExtensionsMemberData.FormatInBytesInvalid), MemberType = typeof(StringExtensionsMemberData))]
    public void TransformBytes_Invalid(string format)
    {
        FluentActions
            .Invoking(format.FormatInBytes)
            .Should()
            .Throw<FormatConversionException>();
    }

    [Fact(DisplayName = "SpaceTitleCase() should separate words using title case.")]
    public void SpaceTitleCase()
    {
        var expected = "Spaced Value Using Title Case";

        var actual = "SpacedValueUsingTitleCase".SpaceTitleCase();

        actual.Should().Be(expected);
    }

    [Theory(DisplayName = "ToPascalCase() should transform the input into pascal case.")]
    [MemberData(nameof(StringExtensionsMemberData.PascalCase), MemberType = typeof(StringExtensionsMemberData))]
    public void ToPascalCase(string input, string expected)
    {
        var actual = input.ToPascalCase();

        actual.Should().Be(expected);
    }

    [Theory(DisplayName = "ToCamelCase() should transform the input into camel case.")]
    [MemberData(nameof(StringExtensionsMemberData.CamelCase), MemberType = typeof(StringExtensionsMemberData))]
    public void ToCamelCase(string input, string expected)
    {
        var actual = input.ToCamelCase();

        actual.Should().Be(expected);
    }

    [Theory(DisplayName = "ToSlugCase() should transform the input into slug case.")]
    [MemberData(nameof(StringExtensionsMemberData.SlugCase), MemberType = typeof(StringExtensionsMemberData))]
    public void ToSlugCase(string input, string expected)
    {
        var actual = input.ToSlugCase();

        actual.Should().Be(expected);
    }

    [Theory(DisplayName = "ToSnakeCase() should transform the input into snake case.")]
    [MemberData(nameof(StringExtensionsMemberData.SnakeCase), MemberType = typeof(StringExtensionsMemberData))]
    public void ToSnakeCase(string input, string expected)
    {
        var actual = input.ToSnakeCase();

        actual.Should().Be(expected);
    }
}