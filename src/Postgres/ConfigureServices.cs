using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Fwks.Postgres.Services;
using Fwks.Core.Abstractions.Services.Infra;
using Fwks.Core.Abstractions.Builders;

namespace Fwks.Postgres;

public static class ConfigureServices
{
    public static IServiceCollection AddPostgres<TDbContext>(this IServiceCollection services, IConnectionStringBuilder builder)
        where TDbContext : DbContext
    {
        return services
            .AddDbContext<TDbContext>(x => x.UseNpgsql(builder.BuildConnectionString()))
            .AddScoped<ITransactionService, TransactionService<TDbContext>>();
    }

    public static IServiceCollection AddPostgres<TDbContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsBuilderAction)
        where TDbContext : DbContext
    {
        return services
            .AddDbContext<TDbContext>(optionsBuilderAction)
            .AddScoped<ITransactionService, TransactionService<TDbContext>>();
    }
}