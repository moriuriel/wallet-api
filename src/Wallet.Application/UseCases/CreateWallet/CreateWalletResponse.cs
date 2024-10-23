namespace Wallets.Application.UseCases.CreateWallet;

public sealed class CreateWalletResponse
{
     private CreateWalletResponse(Guid id)
         => Id = id;

     public Guid Id { get; private set; }

     public static CreateWalletResponse Factory(Guid id)
         => new(id);
}
