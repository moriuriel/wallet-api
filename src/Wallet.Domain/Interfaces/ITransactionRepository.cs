using Wallets.Domain.Entities.Interfaces;

namespace Wallets.Domain.Interfaces;

public interface ITransactionRepository
{
     Task<bool> InsertAsync(
          ITransaction transaction,
          CancellationToken cancellationToken);
}
