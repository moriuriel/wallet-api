namespace Wallets.Domain.Entities.Interfaces;

public interface ITransaction : IEntity
{
     Guid PayerId { get; }
     Guid ReceiverId { get; }
     decimal Amount { get; }
     DateTime TransactionDate { get; }
     DateTime CreatedAt { get; }
}
