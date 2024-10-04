using Wallets.Domain.Entities;
using Wallets.Domain.Entities.Interfaces;
using Wallets.Domain.ValueObjects;

namespace Wallets.Domain.Factories.WalletFactory;

public class WalletFactory : IWalletFactory
{
    public IWallet Construct(
        AccountHolder accountHolder,
        Account account,
        decimal balance)
        => Wallet.Create(
            accountHolder,
            account,
            balance);
}
