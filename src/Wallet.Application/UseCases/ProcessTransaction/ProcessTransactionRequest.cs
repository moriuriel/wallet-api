using Wallets.Domain.Entities;
using Wallets.Domain.Entities.Interfaces;

namespace Wallets.Application.UseCases.ProcessTransaction;

public sealed class ProcessTransactionRequest : IProcessTransactionRequest
{
     public ProcessTransactionRequest(
          Guid payerId,
          Guid receiverId,
          decimal amount,
          DateTime transactionDate)
     {
          PayerId = payerId;
          ReceiverId = receiverId;
          Amount = amount;
          TransactionDate = transactionDate;
     }

     public Guid PayerId { get; private set; }
     public Guid ReceiverId { get; private set; }
     public decimal Amount { get; private set; }
     public DateTime TransactionDate { get; private set; }

     public ITransaction ToTransaction()
          => Transaction.Factory(
               PayerId,
               ReceiverId,
               Amount,
               TransactionDate);
     
     public static ProcessTransactionRequest Factory(
          Guid payerId,
          Guid receiverId,
          decimal amount,
          DateTime transactionDate)
          => new(
               payerId,
               receiverId,
               amount,
               transactionDate);

     public static ProcessTransactionRequest Factory(Guid payerId)
     {
          throw new NotImplementedException();
     }
}
