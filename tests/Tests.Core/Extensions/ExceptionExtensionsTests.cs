using System;
using System.Collections.Generic;
using FluentAssertions;
using Fwks.Core.Extensions;
using Fwks.Tests.Shared.Configuration;
using Xunit;

namespace Fwks.Tests.Core.Extensions;

[Trait(TestsConfiguration.TRAITS_UNIT_TESTS, TestsConfiguration.PROJECTS_CORE)]
public sealed class ExceptionExtensionsTests
{
    [Fact(DisplayName = "ExtractMessages() should return a list of messages from exception and inner exceptions.")]

    public void ExtractMessages()
    {
        var expected = new List<string> { "first", "second", "third" };

        var exception = new Exception("first", new("second", new("third")));

        var actual = exception.ExtractMessages();

        actual
            .Should()
            .BeEquivalentTo(expected);
    }
}
