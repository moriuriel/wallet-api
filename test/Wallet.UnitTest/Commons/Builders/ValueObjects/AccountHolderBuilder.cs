using Bogus.Extensions.Brazil;
using Wallets.Domain.ValueObjects;

namespace Wallets.UnitTest.Commons.Builders.ValueObjects;
public class AccountHolderBuilder : BuilderBase<AccountHolder>
{
    public AccountHolderBuilder()
    {
        _name = FakerSingleton.GetInstance().Faker.Person.FullName;
        _taxId = FakerSingleton.GetInstance().Faker.Person.Cpf(includeFormatSymbols: false);
    }

    private readonly string _name; 
    private readonly string _taxId;

    public override AccountHolder Build()
        => new(_name, _taxId);
}
