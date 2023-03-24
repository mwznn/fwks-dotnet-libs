using Fwks.Core.Domain;
using Fwks.FwksService.Core.Enums;

namespace Fwks.FwksService.Core.Models.Requests;

public sealed record GetCustomerByNameRequest(DbType DbType, string Name = "") : BasePageQuery;

public sealed record AddCustomerRequest(string Name, string DateOfBirth, string Email, string PhoneNumber);