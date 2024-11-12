using FluentValidation;

using Wallets.Application.Commons;
using Wallets.Domain.Interfaces;

namespace Wallets.Application.UseCases.DepositWalletBalance;

public sealed class DepositWalletBalanceUseCase(
     IWalletRepository walletRepository,
     IValidator<DepositWalletBalanceRequest> validator) : IDepositWalletBalanceUseCase
{
     public async Task<Response<DepositWalletBalanceResponse>> HandleAsync(
          DepositWalletBalanceRequest request, 
          CancellationToken cancellationToken)
     {
          var validationResult = await validator.ValidateAsync(request, cancellationToken);
          if (!validationResult.IsValid)
               return Response<DepositWalletBalanceResponse>.ValidationError(validationResult);

          var wallet = await walletRepository.FindByIdAsync(
               request.WalletId,
               cancellationToken);

          if(wallet is null)
               return Response<DepositWalletBalanceResponse>.ContentNotExists();

          var result = wallet.Depoist(amount: request.Balance);
          if(result.IsFailure)
               return Response<DepositWalletBalanceResponse>.BusinessRuleField(
                    error: result.Error.Message);
          
          if(!await walletRepository.UpdateBalanceAsync(
               wallet,
               cancellationToken))
               return Response<DepositWalletBalanceResponse>.FailedDependency();

          var content = DepositWalletBalanceResponse.Factory(id: wallet.Id);

          return Response<DepositWalletBalanceResponse>.Success(content);
     }
}
