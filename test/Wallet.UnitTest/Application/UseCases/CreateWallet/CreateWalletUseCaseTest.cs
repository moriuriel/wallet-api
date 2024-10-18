using System;
using FluentAssertions;
using Moq;
using Wallets.Application.Commons;
using Wallets.Application.UseCases.CreateWallet;
using Wallets.Domain.Entities.Interfaces;
using Wallets.Domain.Interfaces;
using Wallets.UnitTest.Application.UseCases.CreateWallet.Builders;
using Wallets.UnitTest.Commons.Builders.Entities;

namespace Wallets.UnitTest.Application.UseCases.CreateWallet;

public class CreateWalletUseCaseTest
{
    public CreateWalletUseCaseTest() 
      => _useCase = new(
        walletRepository: _walletRepositoryMock.Object);

  private readonly Mock<ICreateWalletRequest> _request = new();
  private readonly Mock<IWalletRepository> _walletRepositoryMock = new();
  private readonly CreateWalletUseCase _useCase;
  private readonly CancellationToken _cancellationToken = CancellationToken.None;
  private readonly Mock<IWallet> _wallet = new();

  [Fact]
  public async Task ExecuteMethodHandleAsync_WithValidValues_ShouldReturnSuccess()
  {
    //Arrange
    var request = new CreateWalletRequestBuilder().Build();

    _request.SetupGet(_ => _.AccountHolderModel)
      .Returns(request.AccountHolderModel);

    _request.SetupGet(_ => _.AccountModel)
      .Returns(request.AccountModel);

    _request.Setup(_ => _.ToWallet())
      .Returns(_wallet.Object);

    var wallet = new WalletBuilder().Build();
    _wallet.SetupGet(_ => _.Id)
      .Returns(wallet.Id);

    var content = CreateWalletResponse.Factory(wallet.Id);

    //Act
    var response = await _useCase.HandleAsync(
        _request.Object,
        _cancellationToken);
    
    //Assert
    response.Type.Should().Be(Response<CreateWalletResponse>.ResponseType.Created);
    response.Content.Should().BeEquivalentTo(content);
    
    _request.Verify(
      _ => _.ToWallet(), 
      times: Times.Once());
      
    _walletRepositoryMock.Verify(
        _ => _.InsertAsync(_wallet.Object, _cancellationToken),
        times: Times.Once);
  }
}
