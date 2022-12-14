using System;
using System.Threading;
using System.Threading.Tasks;

namespace Fwks.Core.Abstractions.Services.Infra;

public interface ITransactionService : IDisposable
{
    Task<bool> CommitAsync(CancellationToken cancellationToken = default);
    bool Commit();
}