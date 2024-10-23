using Wallets.Application.Commons;

namespace Wallets.Application.UseCases.CreateWallet;

public interface ICreateWalletUseCase
{
     Task<Response<CreateWalletResponse>> HandleAsync(
         ICreateWalletRequest request,
         CancellationToken cancellationToken);
}
