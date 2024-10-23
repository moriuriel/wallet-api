using Wallets.Application.UseCases.Shared;
using Wallets.Domain.Entities;
using Wallets.Domain.Entities.Interfaces;

namespace Wallets.Application.UseCases.CreateWallet;
public sealed class CreateWalletRequest : ICreateWalletRequest
{
     public required AccountHolderModel AccountHolderModel { get; init; }
     public required AccountModel AccountModel { get; init; }

     public IWallet ToWallet()
         => Wallet.Create(
             accountHolder: AccountHolderModel.ToAccountHolder(),
             account: AccountModel.ToAccount());
}
