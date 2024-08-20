using Wallets.Domain.Shared;
using Wallets.Domain.ValueObjects;

namespace Wallets.Domain.Entities.Interfaces;

public interface IWallet : IEntity
{
    public AccountHolder AccountHolder { get; }
    public Account Account { get; }
    public float Balance { get; }
    public Result Depoist(float amount);
    public Result Withdraw(float amount);
}

