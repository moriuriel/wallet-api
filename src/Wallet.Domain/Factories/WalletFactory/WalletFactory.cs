using Wallets.Domain.Entities;
using Wallets.Domain.Entities.Interfaces;
using Wallets.Domain.ValueObjects;
using Wallets.Domain.ValueObjects.Interfaces;

namespace Wallets.Domain.Factories.WalletFactory;

public class WalletFactory : IWalletFactory
{
    public IWallet Construct(
        IAccountHolder accountHolder,
        IAccount account,
        decimal balance)
        => Wallet.Create(
            accountHolder,
            account,
            balance);
}
