using System;

using Wallets.Application.Commons;
using Wallets.Domain.Interfaces;

namespace Wallets.Application.UseCases.ProcessTransaction;

public sealed class ProcessTransactionUseCase(
     IWalletRepository walletRepository) : IProcessTransactionUseCase
{
     public Task<Response<ProcessTransactionResponse>> HandleAsync(
          ProcessTransactionRequest processTransactionRequest, 
          CancellationToken cancellationToken)
     {
          throw new NotImplementedException();
     }
}
