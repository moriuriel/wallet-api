namespace Wallets.Application.UseCases.FindWalletById;

public sealed class FindWalletByIdRequest
{
     public Guid Id { get; private set; }

     private FindWalletByIdRequest(Guid id)
       => Id = id;

     public static FindWalletByIdRequest Factory(Guid id)
       => new(id);
}
