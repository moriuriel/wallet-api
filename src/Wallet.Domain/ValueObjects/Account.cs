using Wallets.Domain.ValueObjects.Interfaces;

namespace Wallets.Domain.ValueObjects;

public sealed record Account : IAccount
{
    private Account(string number, string branch)
    {
        Number = number;
        Branch = branch;
    }

    public string Number { get; private set; }

    public string Branch { get; private set; }

    public static Account Factory(
        string number,
        string branch)
        => new(
            number,
            branch);
}