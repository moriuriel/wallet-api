using Wallets.Domain.Entities.Interfaces;

namespace Wallets.Domain.Interfaces;
public interface IWalletRepository
{
    Task InsertAsync(
        IWallet wallet,
        CancellationToken cancellationToken);
}
