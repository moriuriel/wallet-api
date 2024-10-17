using Wallets.Application.UseCases.Shared;
using Wallets.Domain.Entities.Interfaces;

namespace Wallets.Application.UseCases.CreateWallet;
public interface ICreateWalletRequest
{
    AccountHolderModel AccountHolderModel { get; }
    AccountModel AccountModel { get; }
    IWallet ToWallet();
}