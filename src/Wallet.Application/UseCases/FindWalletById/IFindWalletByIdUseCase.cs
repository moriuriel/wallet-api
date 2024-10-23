namespace Wallets.Application.UseCases.FindWalletById;

public interface IFindWalletByIdUseCase
{
  Task<FindWalletByIdResponse> HandleAsync(
    FindWalletByIdRequest request,
    CancellationToken cancellationToken);
}
