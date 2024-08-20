namespace Wallets.Domain.ValueObjects;

public sealed record Account(string Number, string Branch)
{
    public string Number { get; } = Number;
    public string Branch { get; } = Branch;
}