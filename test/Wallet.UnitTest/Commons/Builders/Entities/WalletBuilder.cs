using Wallets.Domain.Entities;
using Wallets.Domain.ValueObjects;
using Wallets.UnitTest.Commons.Builders.ValueObjects;

namespace Wallets.UnitTest.Commons.Builders.Entities;

public sealed class WalletBuilder : BuilderBase<Wallet>
{
    public WalletBuilder()
    {
        _account = new AccountBuilder().Build();
        _accountHolder = new AccountHolderBuilder().Build();
        _balance = FakerSingleton.GetInstance().Faker.Finance.Amount();
    }

    private readonly Account _account;
    private readonly AccountHolder _accountHolder;
    private decimal _balance;

    public WalletBuilder WithBalance(decimal balance)
    {
        _balance = balance;
        return this;
    }

    public override Wallet Build()
        => Wallet.Create(
            _accountHolder,
            _account,
            _balance);
}