
using Wallets.Application.UseCases.FindWalletById;
using Wallets.UnitTest.Commons;

namespace Wallets.UnitTest.Application.UseCases.FindWalletById;

public class FindWalletByIdRequestBuilder : BuilderBase<FindWalletByIdRequest>
{
     public FindWalletByIdRequestBuilder() 
          => _id = FakerSingleton.GetInstance().Faker.Random.Guid();

     private Guid _id;

     public FindWalletByIdRequestBuilder WithEmptyId()
     {
          _id = Guid.Empty;
          return this;
     }

     public override FindWalletByIdRequest Build()
          => FindWalletByIdRequest.Factory(_id);
}
