using Bogus.Extensions.Brazil;

using Wallets.Application.UseCases.Shared;
using Wallets.UnitTest.Commons;

namespace Wallets.UnitTest.Application.UseCases.CreateWallet.Builders;

public class AccountHolderModelBuilder : BuilderBase<AccountHolderModel>
{
     private string _name;
     private string _taxId;

     public AccountHolderModelBuilder()
     {
          _name = FakerSingleton.GetInstance().Faker.Person.FullName;
          _taxId = FakerSingleton.GetInstance().Faker.Person.Cpf(
            includeFormatSymbols: false);
     }
     public AccountHolderModelBuilder WithEmptyContent()
     {
          _name = string.Empty;
          _taxId = string.Empty;
          return this;
     }

     public override AccountHolderModel Build()
     => new()
     {
          Name = _name,
          TaxId = _taxId,
     };
}
