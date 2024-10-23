using Wallets.Domain.ValueObjects;
using Wallets.Domain.ValueObjects.Interfaces;

namespace Wallets.Application.UseCases.Shared;

public sealed class AccountHolderModel
{
    public string Name { get; init; } = string.Empty;

    public string TaxId { get; init; } = string.Empty;

    public IAccountHolder ToAccountHolder()
        => AccountHolder.Factory(
            Name,
            TaxId);
    
    public static AccountHolderModel FactoryByValueObject(
        IAccountHolder accountHolder)
        => new()
        {
            Name = accountHolder.Name,
            TaxId = accountHolder.TaxId
        };
}
