using System;
using FluentAssertions;
using Fwks.Core.Extensions;
using Fwks.Tests.Shared.Configuration;
using Fwks.Tests.Shared.Models;
using Xunit;

namespace Fwks.Tests.Core.Extensions;

[Trait(TestsConfiguration.TRAITS_UNIT_TESTS, TestsConfiguration.PROJECTS_CORE)]
public sealed class EnumExtensionsTests
{
    [Fact(DisplayName = "ToEnum() should return a converted value when valid.")]
    public void ToEnum_ReturnsConvertedValue()
    {
        var expected = EnumStub.ValueB;

        "ValueB".ToEnum<EnumStub>()
            .Should()
            .Be(expected);
    }

    [Fact(DisplayName = "ToEnum() should return an `InvalidCastException` when the value is invalid.")]
    public void ToEnum_ReturnsExceptionWhenInvalid()
    {
        FluentActions
            .Invoking("invalid enum".ToEnum<EnumStub>)
            .Should()
            .Throw<InvalidCastException>();
    }
}
