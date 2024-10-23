using FluentAssertions;

using Wallets.Application.UseCases.CreateWallet;
using Wallets.Application.UseCases.Shared;
using Wallets.UnitTest.Application.UseCases.CreateWallet.Builders;
using Wallets.UnitTest.Commons;

namespace Wallets.UnitTest.Application.UseCases.CreateWallet;

public class CreateWalletRequestValidatorTest
{
     private readonly CreateWalletRequestValidator _validator = new();

     [Fact]
     public async Task CreateWallet_WithValidValues_ShouldReturnSuccess()
     {
          //Arrange
          var request = new CreateWalletRequestBuilder().Build();

          //Act
          var result = await _validator.ValidateAsync(request);

          //Assert
          result.IsValid.Should().BeTrue();
          result.Errors.Should().HaveCount(0);
     }

     [Fact]
     public async Task CreateWallet_WithInvalidAccountHolder_ShouldReturnError()
     {

          //Arrange
          var accountHolderModel = new AccountHolderModelBuilder()
            .WithEmptyContent()
            .Build();

          var request = new CreateWalletRequestBuilder()
            .WithAccountHolder(accountHolderModel)
            .Build();

          const int errorNumebr = 2;

          //Act
          var result = await _validator.ValidateAsync(request);

          //Assert
          result.IsValid.Should().BeFalse();
          result.Errors.Should().HaveCount(errorNumebr);
          result.Errors.Should().Contain(
            _ => _.ErrorMessage == string.Format(Messsage.ValidationError.EMPTY_FIELD, nameof(AccountHolderModel.Name)));
     }
}
