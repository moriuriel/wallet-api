namespace Wallets.Domain.ValueObjects;

public sealed record AccountHolder(string Name, string TaxId)
{
    public string Name { get; } = Name;
    public string TaxId { get; } = TaxId;
}