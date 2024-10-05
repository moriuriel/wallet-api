using Wallets.Application.Commons;

namespace Wallets.Application.UseCases.CreateWallet;

public sealed class CreateWalletUseCase : ICreateWalletUseCase
{
    public Task<Response<CreateWalletResponse>> HandleAsync(
        ICreateWalletRequest request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}