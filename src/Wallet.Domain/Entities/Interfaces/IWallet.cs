using Wallets.Domain.Shared;
using Wallets.Domain.ValueObjects.Interfaces;

namespace Wallets.Domain.Entities.Interfaces;

public interface IWallet : IEntity
{
     IAccountHolder AccountHolder { get; }
     IAccount Account { get; }
     decimal? Balance { get; }
     Result Depoist(decimal amount);
     Result Withdraw(decimal amount);
}

