using Wallets.Domain.Entities.Interfaces;

namespace Wallets.Domain.Interfaces;
public interface IWalletRepository
{
    Task InsertAsync(
        IWallet wallet,
        CancellationToken cancellationToken);

    Task<bool> IsExistsAccountHolderAsync(
        string taxId,
        CancellationToken cancellationToken);
    
    Task<IWallet> FindByIdAsync(
        Guid id,
        CancellationToken cancellationToken);
}
