using FluentAssertions;
using Wallets.Domain.Factories.WalletFactory;
using Wallets.UnitTest.Commons;
using Wallets.UnitTest.Commons.Builders.ValueObjects;

namespace Wallets.UnitTest.Domain.Factories;
public class WalletFactoryTest
{
    [Fact]
    public void ExecuteNewMethod_WithValidValues_ShouldReturnSuccess()
    {
        //Arrange
        var accountHolder = new AccountHolderBuilder().Build();
        var account = new AccountBuilder().Build();

        var balance = FakerSingleton.GetInstance().Faker.Finance.Amount();

        var factory = new WalletFactory();

        //Act
        var wallet = factory.Construct(
            accountHolder,
            account,
            balance);

        //Assert
        wallet.Id.Should().NotBeEmpty();
        wallet.Account.Should().Be(account);
        wallet.AccountHolder.Should().Be(accountHolder);
        wallet.Balance.Should().Be(balance);
    }
}
