using Wallets.Domain.Shared;
using Wallets.Domain.ValueObjects;

namespace Wallets.Domain.Entities.Interfaces;

public interface IWallet : IEntity
{
    public AccountHolder AccountHolder { get; }
    public Account Account { get; }
    public decimal Balance { get; }
    public Result Depoist(decimal amount);
    public Result Withdraw(decimal amount);
}

