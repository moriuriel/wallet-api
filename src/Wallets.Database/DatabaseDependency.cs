using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Wallets.Database.Repositories;
using Wallets.Domain.Interfaces;

namespace Wallets.Database;

public static class DatabaseDependency
{
     public static IServiceCollection AddDatabaseDependency(
           this IServiceCollection services,
           IConfiguration configuration)
     {
          var connectionString = configuration.GetConnectionString("PostgreWallets");
          ArgumentNullException.ThrowIfNull(connectionString);

          services.AddScoped<IDbConnection>(_ => new NpgsqlConnection(connectionString));

          services.AddScoped<IWalletRepository, WalletRepository>();
          services.AddScoped<ITransactionRepository, TransactionRepository>();
          return services;
     }
}
