using System.Collections.Generic;

namespace Fwks.Tests.Core.Extensions.MemberData;

public sealed class StringExtensionsMemberData
{
    public static IEnumerable<object[]> FormatInBytesValid()
    {
        yield return new object[] { "1b", 1 };
        yield return new object[] { "1kb", 1024 };
        yield return new object[] { "1mb", 1048576 };
        yield return new object[] { "1gb", 1073741824 };
        yield return new object[] { "1tb", 1099511627776 };
    }

    public static IEnumerable<object[]> FormatInBytesInvalid()
    {
        yield return new object[] { "1" };
        yield return new object[] { "1x" };
        yield return new object[] { string.Empty };
    }

    public static IEnumerable<object[]> PascalCase()
    {
        yield return new object[] { "This Value", "ThisValue" };
        yield return new object[] { " This Value ", "ThisValue" };
        yield return new object[] { " ThisValue ", "ThisValue" };
    }

    public static IEnumerable<object[]> CamelCase()
    {
        yield return new object[] { "This Value", "thisValue" };
        yield return new object[] { " This Value ", "thisValue" };
        yield return new object[] { " ThisValue ", "thisValue" };
    }

    public static IEnumerable<object[]> SlugCase()
    {
        yield return new object[] { "This Value", "this-value" };
        yield return new object[] { " This Value ", "this-value" };
        yield return new object[] { " ThisValue ", "this-value" };
    }

    public static IEnumerable<object[]> SnakeCase()
    {
        yield return new object[] { "This Value", "this_value" };
        yield return new object[] { " This Value ", "this_value" };
        yield return new object[] { " ThisValue ", "this_value" };
    }
}
