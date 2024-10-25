using FluentAssertions;

using Moq;

using Wallets.Application.Commons;
using Wallets.Application.UseCases.FindWalletById;
using Wallets.Domain.Interfaces;
using Wallets.UnitTest.Commons.Builders.Entities;

namespace Wallets.UnitTest.Application.UseCases.FindWalletById;

public sealed class FindWalletByIdUseCaseTest
{
     public FindWalletByIdUseCaseTest() 
          => _useCase = new(_walletRepositoryMock.Object);

     private readonly Mock<IWalletRepository> _walletRepositoryMock = new();
     private readonly FindWalletByIdUseCase _useCase;
     private readonly CancellationToken _cancellationToken = CancellationToken.None;

     [Fact]
     public async Task ExecuteMethodHandleAsync_WithNotFoundWallet_ShouldReturnContentNotExists()
     {
          //Arrange
          var request = new FindWalletByIdRequestBuilder().Build();

          //Act
          var result = await _useCase.HandleAsync(request, _cancellationToken);
          
          //Assert
          result.Type.Should().Be(Response<FindWalletByIdResponse>.ResponseType.ContentNotExits);
          result.Content.Should().BeNull();

          _walletRepositoryMock.Verify(
               _ => _.FindByIdAsync(request.Id, _cancellationToken),
               times: Times.Once);
     }

     [Fact]
     public async Task ExecuteMethodHandleAsync_WithValidValues_ShouldReturnSuccess()
     {
          //Arrange
          var request = new FindWalletByIdRequestBuilder().Build();
          
          var wallet = new WalletBuilder().Build();

          _walletRepositoryMock.Setup(_ => _.FindByIdAsync(request.Id, _cancellationToken))
               .ReturnsAsync(wallet);
          
          var expectedContent = FindWalletByIdResponse.FactoryByEntity(wallet);

          //Act
          var result = await _useCase.HandleAsync(request, _cancellationToken);
          
          //Assert
          result.Type.Should().Be(Response<FindWalletByIdResponse>.ResponseType.Success);
          result.Content.Should().BeEquivalentTo(expectedContent);

          _walletRepositoryMock.Verify(
               _ => _.FindByIdAsync(request.Id, _cancellationToken),
               times: Times.Once);
     }
}
