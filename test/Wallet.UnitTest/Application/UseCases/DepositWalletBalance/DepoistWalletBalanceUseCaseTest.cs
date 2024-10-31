using System;

using FluentAssertions;

using Moq;

using Wallets.Application.Commons;
using Wallets.Application.UseCases.DepositWalletBalance;
using Wallets.Domain.Entities.Interfaces;
using Wallets.Domain.Interfaces;
using Wallets.Domain.Shared;

namespace Wallets.UnitTest.Application.UseCases.DepositWalletBalance;

public class DepoistWalletBalanceUseCaseTest
{
     public DepoistWalletBalanceUseCaseTest() 
          => _depositWalletBalanceUseCase = new(
               walletRepository: _mockWalletRepository.Object);
     
     private readonly Mock<IWalletRepository> _mockWalletRepository = new();
     private readonly DepositWalletBalanceUseCase _depositWalletBalanceUseCase;
     private readonly CancellationToken _cancellationToken = CancellationToken.None;
     private readonly Mock<IWallet> _mockWallet = new();

     [Fact]
     public async Task ExecuteMethodHandleAsync_WithNotFoundWallet_ShouldReturnContentNotExists()
     {
          //Arrange
          var request = new DepositWalletBalanceRequestBuilder().Build();

          //Act
          var result = await _depositWalletBalanceUseCase.HandleAsync(
               request,
               _cancellationToken);

          //Assert
          result.Type.Should().Be(
               Response<DepositWalletBalanceResponse>.ResponseType.ContentNotExits);
          result.Content.Should().BeNull();

          _mockWalletRepository.Verify(
               _ => _.FindByIdAsync(request.WalletId, _cancellationToken),
               times: Times.Once);
     }

     [Fact]
     public async Task ExecuteMethodHandleAsync_WithValidValues_ShouldReturnSuccess()
     {
          //Arrange
          var request = new DepositWalletBalanceRequestBuilder().Build();
          
          _mockWallet.SetupGet(_ => _.Id)
               .Returns(request.WalletId);

          _mockWalletRepository.Setup(
               _ => _.FindByIdAsync(
                    request.WalletId, 
                    _cancellationToken))
               .ReturnsAsync(_mockWallet.Object);
          
          _mockWallet.Setup(_ => _.Depoist(request.Balance))
               .Returns(Result.Success());

          _mockWalletRepository.Setup(
               _ => _.UpdateBalanceAsync(
                    _mockWallet.Object, 
                    _cancellationToken))
          .ReturnsAsync(true);

          var content = DepositWalletBalanceResponse.Factory(_mockWallet.Object.Id);
          //Act
          var result = await _depositWalletBalanceUseCase.HandleAsync(
               request,
               _cancellationToken);

          //Assert
          result.Type.Should().Be(
               Response<DepositWalletBalanceResponse>.ResponseType.Success);
          result.Content.Should().BeEquivalentTo(content);

          _mockWalletRepository.Verify(
               _ => _.FindByIdAsync(
                    request.WalletId, 
                    _cancellationToken),
               times: Times.Once);

          _mockWallet.Verify(
               _ => _.Depoist(request.Balance),
               times: Times.Once);
          
          _mockWalletRepository.Verify(
               _ => _.UpdateBalanceAsync(
                    _mockWallet.Object, 
                    _cancellationToken),
               times: Times.Once);
     }
}
