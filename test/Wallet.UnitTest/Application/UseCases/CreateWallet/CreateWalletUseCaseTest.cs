using System;
using Moq;
using Wallets.Application.UseCases.CreateWallet;
using Wallets.Domain.Interfaces;

namespace Wallets.UnitTest.Application.UseCases.CreateWallet;

public class CreateWalletUseCaseTest
{
    public CreateWalletUseCaseTest() 
      => _useCase = new(
        walletRepository: _walletRepositoryMock.Object);

  private readonly Mock<IWalletRepository> _walletRepositoryMock = new();
  private readonly CreateWalletUseCase _useCase;
  private readonly CancellationToken _cancellationToken = CancellationToken.None;

  [Fact]
  public async Task ExecuteMethodHandleAsync_WithValidValues_ShouldReturnSuccess()
  {
    
  }
}
