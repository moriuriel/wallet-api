using Wallets.Domain.ValueObjects;
using Wallets.Domain.ValueObjects.Interfaces;

namespace Wallets.Application.UseCases.Shared;
public sealed class AccountModel
{
    public string Number { get; init; } = string.Empty;

    public string Branch { get; init; } = string.Empty;

    public IAccount ToAccount()
        => Account.Factory(
            Number,
            Branch);
}
