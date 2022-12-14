using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Extensions.Primitives;
using Xunit;
using Fwks.Tests.Shared.Configuration;
using Fwks.Core.Contexts;

namespace Fwks.Tests.Core.Contexts;

[Trait(TestsConfiguration.TRAITS_UNIT_TESTS, TestsConfiguration.PROJECTS_CORE)]
public sealed class CorrelationContextTests
{


    [Fact(DisplayName = "Setup, when request header exists, should set the value on context and response headers.")]
    public void Setup_WhenRequestHeadersExist()
    {
        var requestHeaders = new Dictionary<string, StringValues>
        {
            { CorrelationContext.HeaderPropertyName, "correlation-value" }
        };

        var responseHeaders = new Dictionary<string, StringValues>();

        CorrelationContext.Setup(requestHeaders, responseHeaders);

        CorrelationContext.Id.Should().Be("correlation-value");
        responseHeaders.Should().BeEquivalentTo(requestHeaders);
    }

    [Fact(DisplayName = "Setup, when request header does not exist, should create a guid before setting on context and response headers.")]
    public void Setup_WhenRequestHeadersDontExist()
    {
        var requestHeaders = new Dictionary<string, StringValues>();

        var responseHeaders = new Dictionary<string, StringValues>();

        CorrelationContext.Setup(requestHeaders, responseHeaders);

        CorrelationContext.Id.Should().HaveLength(36);
        responseHeaders.Keys.Should().BeEquivalentTo([CorrelationContext.HeaderPropertyName]);
        responseHeaders.Values.Should().BeEquivalentTo(new StringValues[] { CorrelationContext.Id });
    }

    [Fact(DisplayName = "Set Id, when already existent, should ignore the new value.")]
    public void SetId_WhenExisting()
    {
        CorrelationContext.Id = "Value 1";

        CorrelationContext.Id = "Value 2";

        CorrelationContext.Id.Should().Be("Value 1");
    }

    [Fact(DisplayName = "Set Id, when empty or white space, should throw an ArgumentException.")]
    public void SetId_WhenEmptyOrWhiteSpace()
    {
        FluentActions
            .Invoking(() => CorrelationContext.Id = "")
            .Should()
            .Throw<ArgumentException>();
    }
}
