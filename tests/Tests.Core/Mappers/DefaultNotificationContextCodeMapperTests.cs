using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Fwks.Core.Domain;
using Fwks.Core.Mappers;
using Fwks.Tests.Shared.Configuration;
using Xunit;

namespace Fwks.Tests.Core.Mappers;

[Trait(TestsConfiguration.TRAITS_UNIT_TESTS, TestsConfiguration.PROJECTS_CORE)]
public sealed class DefaultNotificationContextCodeMapperTests
{
    private readonly DefaultNotificationContextCodeMapper _target;

    public DefaultNotificationContextCodeMapperTests()
    {
        _target = new DefaultNotificationContextCodeMapper();
    }

    public static IEnumerable<object[]> ValidCodes()
    {
        return new List<object[]>
        {
            new object[] { new[] { "200" }, 200 },
            new object[] { new[] { "201" }, 201 },
            new object[] { new[] { "202" }, 202 },
            new object[] { new[] { "400" }, 400 },
            new object[] { new[] { "401" }, 401 },
            new object[] { new[] { "403" }, 403 },
            new object[] { new[] { "404" }, 404 },
            new object[] { new[] { "500" }, 500 }
        };
    }

    [Theory(DisplayName = "Get, when valid input, should return the equivalent http status code.")]
    [MemberData(nameof(ValidCodes))]
    public void Get_Valid(string[] inputs, int expected)
    {
        var actual = _target.Get(Build(inputs));

        actual.Should().Be(expected);
    }

    [Fact(DisplayName = "Get, when multiple inputs, should return higher http status code.")]
    public void Get_MultipleValid()
    {
        var actual = _target.Get(Build("200", "201", "404", "500"));

        actual.Should().Be(500);
    }

    [Fact(DisplayName = "Get, when invalid input, should return internal server error status code.")]
    public void Get_Invalid()
    {
        var actual = _target.Get(Build("invalid-code"));

        actual.Should().Be(500);
    }

    private static IReadOnlyCollection<Notification> Build(params string[] inputs)
    {
        return inputs.Select(x =>
            new Notification
            {
                Code = x,
                Message = "default message"
            }).ToList();
    }
}
