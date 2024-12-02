using Wallets.Application.Commons;
using Wallets.Domain.Interfaces;
using Wallets.Domain.Services.ProcessTransactionRequest;

namespace Wallets.Application.UseCases.ProcessTransaction;

public sealed class ProcessTransactionUseCase(
     IWalletRepository walletRepository,
     IProcessTransactionRequestService processTransactionRequestService,
     ITransactionRepository transactionRepository)
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

          if (payerWallet is null)
          {
               return Response<ProcessTransactionResponse>.BusinessRuleField(
                    error: "Payer Wallet Not Found.");
          }

          var receiverWallet = await walletRepository.FindByIdAsync(
               id: transaction.PayerId,
               cancellationToken
          );

          if (receiverWallet is null)
          {
               return Response<ProcessTransactionResponse>.BusinessRuleField(
                    error: "Receiver Wallet Not Found.");
          }

          var processTransactionResult = processTransactionRequestService.Proccess(
               payerWallet,
               receiverWallet,
               amount: transaction.Amount);

          if (processTransactionResult.IsFailure)
          {
               return Response<ProcessTransactionResponse>.BusinessRuleField(
                    error: processTransactionResult.Error.Message);
          }

          var updateBalancePayerTask = walletRepository.UpdateBalanceAsync(
               payerWallet,
               cancellationToken);

          var updateBalanceReceiverTask = walletRepository.UpdateBalanceAsync(
               receiverWallet,
               cancellationToken);

          var transactionInsertTask = transactionRepository.InsertAsync(
               transaction,
               cancellationToken);

          await Task.WhenAll(
               updateBalancePayerTask,
               updateBalanceReceiverTask,
               transactionInsertTask);

          if (!updateBalancePayerTask.Result || 
               !updateBalanceReceiverTask.Result ||
               !transactionInsertTask.Result)
          {
               return Response<ProcessTransactionResponse>.FailedDependency();
          }

          return Response<ProcessTransactionResponse>.Created(
               content: ProcessTransactionResponse.Factory(transactionId: transaction.Id));
     }
}
