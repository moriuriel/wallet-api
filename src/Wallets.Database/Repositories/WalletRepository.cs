using System.Data;
using Dapper;
using Wallets.Database.Mappers;
using Wallets.Domain.Entities.Interfaces;
using Wallets.Domain.Interfaces;

namespace Wallets.Database.Repositories;

public sealed class WalletRepository : IWalletRepository
{
     private readonly IDbConnection _dbConnection;

     public WalletRepository(IDbConnection dbConnection)
       => _dbConnection = dbConnection;

     public Task<IWallet> FindByIdAsync(Guid id, CancellationToken cancellationToken)
     {
          throw new NotImplementedException();
     }

     public Task InsertAsync(
       IWallet wallet,
       CancellationToken cancellationToken)
     {
          var rawSql = @"INSERT INTO public.wallets
                (id, name, tax_id, account_branch, account_number, created_at)
                VALUES
                (@id, @name, @taxId, @accountBranch, @accountNumber, @createdAt)";

          var mapper = WalletMapper.FactoryByEntity(wallet);

          var command = new CommandDefinition(
            commandText: rawSql,
            parameters: new
            {
                 id = mapper.Id,
                 name = mapper.Name,
                 taxId = mapper.TaxId,
                 accountBranch = mapper.AccountBranch,
                 accountNumber = mapper.AccountNumber,
                 createdAt = mapper.CreatedAt,
            },
            cancellationToken: cancellationToken
          );

          return _dbConnection.ExecuteAsync(command);
     }

     public async Task<bool> IsExistsAccountHolderAsync(
       string taxId,
       CancellationToken cancellationToken)
     {
          var rawSql = $@"SELECT COUNT(tax_id) FROM public.wallets WHERE tax_id = @{nameof(taxId)}";

          var command = new CommandDefinition(
            commandText: rawSql,
            parameters: new { taxId },
            cancellationToken: cancellationToken
          );

          int total = await _dbConnection.ExecuteScalarAsync<int>(command);

          return total > uint.MinValue;
     }
}
