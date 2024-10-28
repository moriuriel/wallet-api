using System;

using Wallets.Application.Commons;

namespace Wallets.Application.UseCases.DepositWalletBalance;

public interface IDepositWalletBalanceUseCase
{
     Task<Response<DepositWalletBalanceResponse>> HandleAsync(
          DepositWalletBalanceRequest request, 
          CancellationToken cancellationToken);
}
