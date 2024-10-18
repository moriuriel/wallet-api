using System;
using Bogus.Extensions.Brazil;
using Wallets.Application.UseCases.Shared;
using Wallets.UnitTest.Commons;

namespace Wallets.UnitTest.Application.UseCases.CreateWallet.Builders;

public class AccountHolderModelBuilder : BuilderBase<AccountHolderModel>
{
    private readonly string _name;
    private readonly string _taxId;

    public AccountHolderModelBuilder()
    {
      _name = FakerSingleton.GetInstance().Faker.Person.FullName;
      _taxId = FakerSingleton.GetInstance().Faker.Person.Cpf(
        includeFormatSymbols: false);
    }

    public override AccountHolderModel Build()
    => new() 
    {
      Name = _name,
      TaxId = _taxId,
    };
}