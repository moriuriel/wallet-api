using FluentValidation;
using Wallets.Application.Commons;
using Wallets.Domain.Interfaces;

namespace Wallets.Application.UseCases.CreateWallet;

public sealed class CreateWalletUseCase(
    IWalletRepository walletRepository,
    IValidator<CreateWalletRequest> validator) 
: ICreateWalletUseCase
{
    public async Task<Response<CreateWalletResponse>> HandleAsync(
        ICreateWalletRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);
        if(!validationResult.IsValid)
            return Response<CreateWalletResponse>.ValidationError(validationResult);

        var wallet = request.ToWallet();

        if(await walletRepository.IsExistsAccountHolderAsync(
            wallet.AccountHolder.TaxId,
            cancellationToken))
            return Response<CreateWalletResponse>.Conflict();

       await walletRepository.InsertAsync(
            wallet,
            cancellationToken);

        return Response<CreateWalletResponse>.Created(
            content: CreateWalletResponse.Factory(wallet.Id));
    }
}