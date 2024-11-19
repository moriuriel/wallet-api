using Wallets.Application.Commons;

namespace Wallets.Application.UseCases.ProcessTransaction;

public interface IProcessTransactionUseCase
{
     Task<Response<ProcessTransactionResponse>> HandleAsync(
          ProcessTransactionRequest processTransactionRequest,
          CancellationToken cancellationToken);
}
