using Wallets.Domain.Entities.Interfaces;

namespace Wallets.Application.UseCases.ProcessTransaction;

public interface IProcessTransactionRequest
{
     public Guid PayerId { get; }
     public Guid ReceiverId { get; }
     public decimal Amount { get; }
     public DateTime TransactionDate { get; }
     ITransaction ToTransaction();
}
