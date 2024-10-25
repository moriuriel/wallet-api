using Wallets.Application.Commons;
using Wallets.Domain.Interfaces;

namespace Wallets.Application.UseCases.FindWalletById;

public sealed class FindWalletByIdUseCase(
  IWalletRepository walletRepository) : IFindWalletByIdUseCase
{
     public async Task<Response<FindWalletByIdResponse>> HandleAsync(
          FindWalletByIdRequest request,
          CancellationToken cancellationToken)
     {
          var wallet = await walletRepository.FindByIdAsync(
            id: request.Id,
            cancellationToken
          );

          if (wallet is null)
               return Response<FindWalletByIdResponse>.ContentNotExists();

          var content = FindWalletByIdResponse.FactoryByEntity(wallet);

          return Response<FindWalletByIdResponse>.Success(content);
     }
}
