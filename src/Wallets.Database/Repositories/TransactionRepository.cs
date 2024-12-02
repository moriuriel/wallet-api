
using System.Data;

using Dapper;

using Wallets.Database.Mappers;
using Wallets.Domain.Entities.Interfaces;
using Wallets.Domain.Interfaces;

namespace Wallets.Database.Repositories;

public class TransactionRepository(IDbConnection dbConnection)  : ITransactionRepository
{
     public async Task<bool> InsertAsync(
          ITransaction transaction, 
          CancellationToken cancellationToken)
     {
          var rawSql = @"INSERT INTO public.transactions
                (id, payer_id, receiver_id, amount, transaction_date, created_at)
                VALUES
                (@id, @payerId, @receiverId, @ammount, @transactionDate, @createdAt)";

           var mapper = TransactionMapper.FactoryByEntity(transaction);

          var command = new CommandDefinition(
            commandText: rawSql,
            parameters: new
            {
                 id = mapper.Id,
                 payerId = mapper.PayerId,
                 receiverId = mapper.ReceiverId,
                 ammount = mapper.Amount,
                 transactionDate = mapper.TransactionDate,
                 createdAt = mapper.CreatedAt,
            },
            cancellationToken: cancellationToken
          );

          return await dbConnection.ExecuteAsync(command) > 0;
     }
}
