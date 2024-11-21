using Wallets.Domain.Entities.Interfaces;
using Wallets.Domain.Shared;

namespace Wallets.Domain.Services.ProcessTransactionRequest;

public sealed class ProcessTransactionRequestService
    : IProcessTransactionRequestService
{
     public Result Proccess(
         IWallet payer,
         IWallet receiver,
         decimal amount)
     {
          var withdrawResult = payer.Withdraw(amount);

          if (withdrawResult.IsFailure)
               return withdrawResult;

          var depoistResult = receiver.Depoist(amount);

          if (depoistResult.IsFailure)
               return depoistResult;

          return Result.Success();
     }
}