using Wallets.Domain.ValueObjects;

namespace Wallets.UnitTest.Commons.Builders.ValueObjects;

public class AccountBuilder : BuilderBase<Account>
{
     public AccountBuilder()
     {
          _number = FakerSingleton.GetInstance().Faker.Finance.Account(length: 6);
          _branch = FakerSingleton.GetInstance().Faker.Finance.Account(length: 2);
     }

     private readonly string _number;
     private readonly string _branch;

     public override Account Build()
         => Account.Factory(_number, _branch);
}
