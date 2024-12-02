using Wallets.Domain.Entities.Interfaces;

namespace Wallets.Database.Mappers;

internal sealed class TransactionMapper
{
     public string Id { get; init; } = string.Empty;
     public string PayerId { get; init; } = string.Empty;

     public string ReceiverId { get; init; } = string.Empty;

     public decimal Amount { get; init; }

     public DateTime TransactionDate { get; init; }

     public DateTime CreatedAt { get; init; }

     public static TransactionMapper FactoryByEntity(ITransaction transaction)
          => new()
          {
               Id = transaction.Id.ToString(),
               Amount = transaction.Amount,
               PayerId = transaction.PayerId.ToString(),
               ReceiverId = transaction.ReceiverId.ToString(),
               TransactionDate = transaction.TransactionDate,
               CreatedAt = transaction.CreatedAt,
          };
}
