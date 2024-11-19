namespace Wallets.Application.UseCases.Shared;

public sealed record ProcessTransactionModel
{
     public Guid PayerId { get; init; }
     public Guid ReceiverId { get; init; }
     public decimal Amount { get; init; }
     public DateTime TransactionDate { get; init; }
}
