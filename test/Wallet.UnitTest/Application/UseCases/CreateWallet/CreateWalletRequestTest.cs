
using FluentAssertions;

using Wallets.UnitTest.Application.UseCases.CreateWallet.Builders;

namespace Wallets.UnitTest.Application.UseCases.CreateWallet;

public class CreateWalletRequestTest
{
     [Fact]
     public void ContructRequest_ConvertToWallet_ShouldReturnSuccess()
     {
          //Arrange
          var request = new CreateWalletRequestBuilder().Build();

          //Act
          var wallet = request.ToWallet();

          //Assert
          wallet.Id.Should().NotBeEmpty();
          wallet.Account.Should().BeEquivalentTo(request.AccountModel);
          wallet.AccountHolder.Should().BeEquivalentTo(request.AccountHolderModel);
          wallet.Balance.Should().BeNull();
     }
}
