using Wallets.Domain.Shared;
using Wallets.Domain.ValueObjects.Interfaces;

namespace Wallets.Domain.Entities.Interfaces;

public interface IWallet : IEntity
{
     public IAccountHolder AccountHolder { get; }
     public IAccount Account { get; }
     public decimal? Balance { get; }
     public Result Depoist(decimal amount);
     public Result Withdraw(decimal amount);
}

