using Wallets.Application.Commons;

namespace Wallets.Application.UseCases.FindWalletById;

public interface IFindWalletByIdUseCase
{
     Task<Response<FindWalletByIdResponse>> HandleAsync(
       FindWalletByIdRequest request,
       CancellationToken cancellationToken);
}
