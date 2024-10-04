using Bogus.Extensions.Brazil;
using Wallets.UnitTest.Commons;
using Wallets.Domain.ValueObjects;
using Wallets.Domain.Entities;
using FluentAssertions;
using Wallets.UnitTest.Commons.Builders.Entities;
using Wallets.Domain.Shared;
using Wallets.UnitTest.Commons.Builders.ValueObjects;

namespace Wallets.UnitTest.Domain.Entities;

public class WalletTest
{
    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void ConstructWallet_WithValidValues_ShouldReturnSuccess(
        bool setNullableFields)
    {
        //Arrange
        var accountHolder = new AccountHolderBuilder().Build();
        var account = new AccountBuilder().Build();

        var balance = FakerSingleton.GetInstance().Faker.Finance.Amount();
        Guid id = setNullableFields ? FakerSingleton.GetInstance().Faker.Random.Uuid() : Guid.Empty;

        //Act
        var wallet = Wallet.Create(
            accountHolder,
            account,
            balance,
            id);

        //Assert
        wallet.Id.Should().Be(id);
        wallet.Account.Should().BeEquivalentTo(account);
        wallet.AccountHolder.Should().BeEquivalentTo(accountHolder);
        wallet.Balance.Should().Be(balance);
    }

    [Fact]
    public void ExecuteMethodDepoist_WithValidAmount_ShouldReturnSuccess()
    {
        //Arrange
        var wallet = new WalletBuilder().Build();
        var amount = FakerSingleton.GetInstance().Faker.Finance.Amount();

        //Act
        var result = wallet.Depoist(amount);

        //
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void ExecuteMethodDepoist_WithInvalidAmount_ShouldReturnError()
    {
        //Arrange
        var wallet = new WalletBuilder().Build();
        var amount = FakerSingleton.GetInstance().Faker.Finance.Amount(max: ushort.MinValue);

        //Act
        var result = wallet.Depoist(amount);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should()
            .Be(DomainErrors.Wallet.AmountRequestedMustBeGreaterThanZero);
    }

    [Fact]
    public void ExecuteMethodWithdraw_WithInvalidAmount_ShouldReturnError()
    {
        //Arrange
        var wallet = new WalletBuilder().Build();
        var amount = FakerSingleton.GetInstance().Faker.Finance.Amount(max: ushort.MinValue);

        //Act
        var result = wallet.Withdraw(amount);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should()
            .Be(DomainErrors.Wallet.AmountRequestedMustBeGreaterThanZero);
    }

    [Fact]
    public void ExecuteMethodWithdraw_WithInsufficientBalance_ShouldReturnError()
    {
        //Arrange
        var wallet = new WalletBuilder()
            .WithBalance(balance: ushort.MinValue)
            .Build();

        var amount = FakerSingleton.GetInstance().Faker.Finance
                                   .Amount();

        //Act
        var result = wallet.Withdraw(amount);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should()
            .Be(DomainErrors.Wallet.InsufficientBalance);
    }

    [Fact]
    public void ExecuteMethodWithdraw_WithValidValues_ShouldReturnSuccess()
    {
        //Arrange
        var amount = FakerSingleton.GetInstance().Faker.Finance
                                   .Amount();

        var wallet = new WalletBuilder()
            .WithBalance(balance: amount)
            .Build();

        //Act
        var result = wallet.Withdraw(amount);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }
}
