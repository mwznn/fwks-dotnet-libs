using Fwks.Core.Domain;
using Fwks.FwksService.Core.Domain.Responses;
using Fwks.FwksService.Core.Domain.Enums;
using Mediator;

namespace Fwks.FwksService.Core.Domain.Queries;

public sealed record GetCustomerByNameQuery(DbType DbType, string Name = "") : BasePageQuery, IQuery<Page<CustomerResponse>>;