using Wallets.Domain.Entities.Interfaces;
using Wallets.Domain.ValueObjects;

namespace Wallets.Domain.Factories.WalletFactory;
public interface IWalletFactory
{
    IWallet Construct(
        AccountHolder accountHolder,
        Account account,
        float balance);
}
