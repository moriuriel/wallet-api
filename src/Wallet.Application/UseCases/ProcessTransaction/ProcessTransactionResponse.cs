namespace Wallets.Application.UseCases.ProcessTransaction;

public sealed class ProcessTransactionResponse
{
     public Guid TransactionId { get; private set; }

     public ProcessTransactionResponse(Guid transactionId)
          => TransactionId = transactionId;

     public static ProcessTransactionResponse Factory(Guid transactionId)
          => new(transactionId);
}
