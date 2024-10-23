using Wallets.Application.UseCases.Shared;
using Wallets.UnitTest.Commons;

namespace Wallets.UnitTest.Application.UseCases.CreateWallet.Builders;

public class AccountModelBuilder : BuilderBase<AccountModel>
{
     public AccountModelBuilder()
     {
          _branch = FakerSingleton.GetInstance().Faker.Finance.Account(length: 2);
          _number = FakerSingleton.GetInstance().Faker.Finance.Account(length: 6);
     }
     private readonly string _branch;
     private readonly string _number;
     public override AccountModel Build()
       => new()
       {
            Branch = _branch,
            Number = _number,
       };
}
