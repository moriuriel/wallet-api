using Wallets.Domain.Entities.Interfaces;

namespace Wallets.Domain.Entities;

public sealed class Transaction : Entity, ITransaction
{
     private Transaction(
          Guid payerId,
          Guid receiverId,
          decimal amount,
          DateTime transactionDate,
          DateTime? createdAt,
          Guid? id = null) : base(id ?? Guid.NewGuid())
     {
          PayerId = payerId;
          ReceiverId = receiverId;
          Amount = amount;
          TransactionDate = transactionDate;
          CreatedAt = createdAt ?? DateTime.UtcNow;
     }

     public Guid PayerId { get; private set; }

     public Guid ReceiverId { get; private set; }

     public decimal Amount { get; private set; }

     public DateTime TransactionDate { get; private set; }

     public DateTime CreatedAt { get; private set; }

     public static Transaction Factory(
          Guid payerId,
          Guid receiverId,
          decimal amount,
          DateTime transactionDate,
          DateTime? createdAt = null,
          Guid? id = null)
     => new(
          payerId,
          receiverId,
          amount,
          transactionDate,
          createdAt,
          id);

}
