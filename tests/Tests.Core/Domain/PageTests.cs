using System;
using System.Collections.Generic;
using FluentAssertions;
using Fwks.Core.Domain;
using Fwks.Tests.Shared.Configuration;
using Fwks.Tests.Shared.Models;
using Xunit;

namespace Fwks.Tests.Core.Domain;

[Trait(TestsConfiguration.TRAITS_UNIT_TESTS, TestsConfiguration.PROJECTS_CORE)]
public sealed class PageTests
{
    public static IEnumerable<object[]> PageProperties()
    {
        return new List<object[]>
        {
            new object[] { 10, 100, 10 },
            new object[] { 10, 1000, 100 },
            new object[] { 10, 1, 1 },
            new object[] { 10, 9, 1 },
            new object[] { 10, 11, 2 }
        };
    }

    [Theory(DisplayName = "Default constructor should create a paged model with total pages value.")]
    [MemberData(nameof(PageProperties))]
    public void Default_Page(int pageSize, int totalItems, int expectedPages)
    {
        var actual = new Page<RequestStub>
        {
            CurrentPage = 1,
            PageSize = pageSize,
            TotalItems = totalItems,
            Items = Array.Empty<RequestStub>()
        };

        actual.TotalPages.Should().Be(expectedPages);
    }
}
