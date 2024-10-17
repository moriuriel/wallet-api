using Wallets.Domain.Entities.Interfaces;
using Wallets.Domain.ValueObjects;
using Wallets.Domain.ValueObjects.Interfaces;

namespace Wallets.Domain.Factories.WalletFactory;
public interface IWalletFactory
{
    IWallet Construct(
        IAccountHolder accountHolder,
        IAccount account,
        decimal balance);
}
