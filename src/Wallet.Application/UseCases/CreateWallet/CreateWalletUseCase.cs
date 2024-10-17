using Wallets.Application.Commons;
using Wallets.Domain.Interfaces;

namespace Wallets.Application.UseCases.CreateWallet;

public sealed class CreateWalletUseCase(
    IWalletRepository walletRepository) 
: ICreateWalletUseCase
{
    public async Task<Response<CreateWalletResponse>> HandleAsync(
        ICreateWalletRequest request,
        CancellationToken cancellationToken)
    {
        var wallet = request.ToWallet();

       await walletRepository.InsertAsync(
            wallet,
            cancellationToken);

        return Response<CreateWalletResponse>.Created(
            content: CreateWalletResponse.Factory(wallet.Id));
    }
}