using Wallets.Application.Commons;
using Wallets.Domain.Interfaces;

namespace Wallets.Application.UseCases.CreateWallet;

public sealed class CreateWalletUseCase : ICreateWalletUseCase
{
    private readonly IWalletRepository _walletRepository;

    public CreateWalletUseCase(IWalletRepository walletRepository)
        => _walletRepository = walletRepository;

    public async Task<Response<CreateWalletResponse>> HandleAsync(
        ICreateWalletRequest request,
        CancellationToken cancellationToken)
    {
        var wallet = request.ToWallet();

       await _walletRepository.InsertAsync(
            wallet,
            cancellationToken);

        return Response<CreateWalletResponse>.Created(
            content: CreateWalletResponse.Factory(wallet.Id));
    }
}