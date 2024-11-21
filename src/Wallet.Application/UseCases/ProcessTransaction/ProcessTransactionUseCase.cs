using Wallets.Application.Commons;
using Wallets.Domain.Interfaces;
using Wallets.Domain.Services.ProcessTransactionRequest;

namespace Wallets.Application.UseCases.ProcessTransaction;

public sealed class ProcessTransactionUseCase(
     IWalletRepository walletRepository,
     IProcessTransactionRequestService processTransactionRequestService) 
     : IProcessTransactionUseCase
{
     public async Task<Response<ProcessTransactionResponse>> HandleAsync(
          ProcessTransactionRequest processTransactionRequest, 
          CancellationToken cancellationToken)
     {
          var transaction = processTransactionRequest.ToTransaction();

          var payerWallet = await walletRepository.FindByIdAsync(
               id: transaction.PayerId,
               cancellationToken
          );

          if(payerWallet is null)
          {
               return Response<ProcessTransactionResponse>.BusinessRuleField(
                    error: "Payer Wallet Not Found.");
          }

          var receiverWallet = await walletRepository.FindByIdAsync(
               id: transaction.PayerId,
               cancellationToken
          );

          if(receiverWallet is null)
          {
               return Response<ProcessTransactionResponse>.BusinessRuleField(
                    error: "Receiver Wallet Not Found.");
          }

          var processTransactionResult = processTransactionRequestService.Proccess(
               payerWallet,
               receiverWallet,
               amount: transaction.Amount);
          
          if(processTransactionResult.IsFailure)
          {
               return Response<ProcessTransactionResponse>.BusinessRuleField(
                    error: processTransactionResult.Error.Message);
          }
          
          if(!await walletRepository.UpdateBalanceAsync(
               payerWallet,
               cancellationToken) || 
               !await walletRepository.UpdateBalanceAsync(
               receiverWallet,
               cancellationToken))
          {
               return Response<ProcessTransactionResponse>.FailedDependency();
          }

          return Response<ProcessTransactionResponse>.Created(
               content: ProcessTransactionResponse.Factory(transactionId: transaction.Id));
     }
}
