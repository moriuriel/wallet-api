using Wallets.Domain.Entities.Interfaces;
using Wallets.Domain.Interfaces;

namespace Wallets.Database.Repositories;

public sealed class WalletRepository : IWalletRepository
{
  public Task InsertAsync(
    IWallet wallet,
    CancellationToken cancellationToken)
  {
    throw new NotImplementedException();
  }

  public Task<bool> IsExistsAccountHolderAsync(
    string taxId,
    CancellationToken cancellationToken)
  {
    throw new NotImplementedException();
  }
}
