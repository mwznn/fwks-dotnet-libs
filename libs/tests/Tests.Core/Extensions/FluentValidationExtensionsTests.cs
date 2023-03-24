using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentValidation;
using Fwks.Core.Extensions;
using Fwks.Tests.Shared.Configuration;
using Fwks.Tests.Shared.Models;
using Xunit;

namespace Fwks.Tests.Core.Extensions;

[Trait(TestsConfiguration.TRAITS_UNIT_TESTS, TestsConfiguration.PROJECTS_CORE)]
public sealed class FluentValidationExtensionsTests
{
    private class Validator : AbstractValidator<RequestStub>
    {
        public Validator()
        {
            RuleFor(x => x.Phone).PhoneNumber().WithMessage("invalid phone");
        }
    }

    public static IEnumerable<object[]> InvalidPhones()
    {
        return new List<object[]>
        {
            new object[] { "invalid-phone-value" },
            new object[] { "1234567890" },
            new object[] { " " },
            new object[] { string.Empty }
        };
    }

    [Fact(DisplayName = "PhoneNumber when valid, should return a valid response with no errors.")]
    public void PhoneNumber_WhenValid()
    {
        var actual = new Validator().Validate(new RequestStub { Phone = "+1234567890" });

        actual.IsValid.Should().BeTrue();
        actual.Errors.Should().BeEmpty();
    }

    [Theory(DisplayName = "PhoneNumber when invalid, should return the defined message error.")]
    [MemberData(nameof(InvalidPhones))]
    public void PhoneNumber_WhenInvalid(string input)
    {
        var actual = new Validator().Validate(new RequestStub { Phone = input });

        actual.IsValid.Should().BeFalse();
        actual.Errors.Select(x => x.ErrorMessage).Should().Contain("invalid phone");
    }
}