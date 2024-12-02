using System.Data;
using Dapper;
using Wallets.Database.Mappers;
using Wallets.Domain.Entities.Interfaces;
using Wallets.Domain.Interfaces;

namespace Wallets.Database.Repositories;

public sealed class WalletRepository(IDbConnection dbConnection) : IWalletRepository
{
     public async Task<IWallet?> FindByIdAsync(Guid id, CancellationToken cancellationToken)
     {
          var rawSql = $@"SELECT 
                              id as {nameof(WalletMapper.Id)},
                              name as {nameof(WalletMapper.Name)},
                              tax_id as {nameof(WalletMapper.TaxId)},
                              account_branch as {nameof(WalletMapper.AccountBranch)},
                              account_number as {nameof(WalletMapper.AccountNumber)},
                              balance as {nameof(WalletMapper.Balance)}
                         FROM public.wallets WHERE id = @{nameof(id)}";

          var command = new CommandDefinition(
               commandText: rawSql,
               parameters: new {
                    id = id.ToString(),
               },
               cancellationToken: cancellationToken
          );

          var wallet = await dbConnection.QuerySingleAsync<WalletMapper>(command);

          if(wallet is null)
               return default;
          
          return wallet.FactoryByMapper();
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

          return dbConnection.ExecuteAsync(command);
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

          int total = await dbConnection.ExecuteScalarAsync<int>(command);

          return total > uint.MinValue;
     }

     public async Task<bool> UpdateBalanceAsync(
          IWallet wallet, 
          CancellationToken cancellationToken)
     {
          var rawSql = @$"UPDATE public.wallets
                              SET balance = @balance
                         WHERE wallets.id = @id";
          
          var mapper = WalletMapper.FactoryByEntity(wallet);

          var command = new CommandDefinition(
               commandText: rawSql,
               parameters: new 
               {
                    balance = mapper.Balance,
                    id = mapper.Id,
               }
          );

          int rowsAffected = await dbConnection.ExecuteAsync(command);

          return rowsAffected > uint.MinValue;
     }
}
