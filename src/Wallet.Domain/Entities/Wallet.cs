using Wallets.Domain.Entities.Interfaces;
using Wallets.Domain.Shared;
using Wallets.Domain.ValueObjects;

namespace Wallets.Domain.Entities;

public class Wallet : AggregateRoot, IWallet
{
    private Wallet(
        AccountHolder accountHolder,
        Account account,
        decimal balance,
        Guid? id = null) : base(id: id ?? Guid.NewGuid())
    {
        Account = account;
        Balance = balance;
        AccountHolder = accountHolder;
    }

    public AccountHolder AccountHolder { get; }
    public Account Account { get; }
    public decimal Balance { get; private set; }

    public static Wallet Create(
        AccountHolder accountHolder,
        Account account,
        decimal balance,
        Guid? id = null)
        => new(
            accountHolder,
            account,
            balance,
            id);

    public Result Depoist(decimal amount)
    {
        if (amount <= ushort.MinValue)
            return Result.Failure(
                DomainErrors.Wallet.AmountRequestedMustBeGreaterThanZero);

        Balance += amount;

        return Result.Success();
    }

    public Result Withdraw(decimal amount)
    {
        if (amount == ushort.MinValue)
            return Result.Failure(
                DomainErrors.Wallet.AmountRequestedMustBeGreaterThanZero);

        if (Balance < amount)
            return Result.Failure(
                DomainErrors.Wallet.InsufficientBalance);

        Balance -= amount;

        return Result.Success();
    }
}
