using FluentAssertions;

using Moq;

using Wallets.Domain.Entities.Interfaces;
using Wallets.Domain.Services.ProcessPaymentRequest;
using Wallets.Domain.Shared;
using Wallets.UnitTest.Commons;

namespace Wallets.UnitTest.Domain.Services.ProcessPaymentRequest;
public class ProcessPaymentRequestServiceTest
{
     private readonly Mock<IWallet> _payer = new();
     private readonly Mock<IWallet> _receiver = new();
     private readonly ProcessPaymentRequestService _service;

     public ProcessPaymentRequestServiceTest()
     {
          _service = new();
     }

     [Fact]
     public void ExecuteMethodProcess_WithValidValues_ShouldReturnSuccess()
     {
          //Arrange
          var amount = FakerSingleton.GetInstance().Faker.Finance.Amount();

          _payer.Setup(_ => _.Withdraw(amount))
              .Returns(Result.Success());

          _receiver.Setup(_ => _.Depoist(amount))
              .Returns(Result.Success());

          //Act
          var result = _service.Proccess(
              _payer.Object,
              _receiver.Object,
              amount);

          //Assert
          result.IsSuccess.Should().BeTrue();

          _payer.Verify(
              _ => _.Withdraw(amount),
              times: Times.Once);

          _receiver.Verify(
              _ => _.Depoist(amount),
              times: Times.Once);
     }

     [Fact]
     public void ExecuteMethodProcess_WithErrorOnWithdraw_ShouldReturnError()
     {
          //Arrange
          var amount = FakerSingleton.GetInstance().Faker.Finance.Amount();

          _payer.Setup(_ => _.Withdraw(amount))
              .Returns(Result.Failure(
                  DomainErrors.Wallet.AmountRequestedMustBeGreaterThanZero));

          _receiver.Setup(_ => _.Depoist(amount))
              .Returns(Result.Success());

          //Act
          var result = _service.Proccess(
              _payer.Object,
              _receiver.Object,
              amount);

          //Assert
          result.IsSuccess.Should().BeFalse();
          result.IsFailure.Should().BeTrue();
          result.Error.Should().Be(DomainErrors.Wallet.AmountRequestedMustBeGreaterThanZero);

          _payer.Verify(
              _ => _.Withdraw(amount),
              times: Times.Once);

          _receiver.Verify(
              _ => _.Depoist(amount),
              times: Times.Never);
     }

     [Fact]
     public void ExecuteMethodProcess_WithErrorOnDeposit_ShouldReturnError()
     {
          //Arrange
          var amount = FakerSingleton.GetInstance().Faker.Finance.Amount();

          _payer.Setup(_ => _.Withdraw(amount))
              .Returns(Result.Success());

          _receiver.Setup(_ => _.Depoist(amount))
              .Returns(Result.Failure(DomainErrors.Wallet.InsufficientBalance));

          //Act
          var result = _service.Proccess(
              _payer.Object,
              _receiver.Object,
              amount);

          //Assert
          result.IsSuccess.Should().BeFalse();
          result.IsFailure.Should().BeTrue();
          result.Error.Should().Be(DomainErrors.Wallet.InsufficientBalance);

          _payer.Verify(
              _ => _.Withdraw(amount),
              times: Times.Once);

          _receiver.Verify(
              _ => _.Depoist(amount),
              times: Times.Once);
     }
}