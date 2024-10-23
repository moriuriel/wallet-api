using Wallets.Domain.Entities;
using Wallets.Domain.Entities.Interfaces;
using Wallets.Domain.ValueObjects;

namespace Wallets.Database.Mappers;

public sealed class WalletMapper
{
     public string Id { get; init; } = string.Empty;
     public string Name { get; init; } = string.Empty;
     public string TaxId { get; init; } = string.Empty;
     public decimal? Balance { get; init; }
     public string AccountNumber { get; init; } = string.Empty;
     public string AccountBranch { get; init; } = string.Empty;
     public DateTime CreatedAt { get; init; }

     public IWallet FactoryByMapper()
     {
          var accountHolder = AccountHolder.Factory(
            name: Name,
            taxId: TaxId);

          var account = Account.Factory(
            number: AccountNumber,
            branch: AccountBranch);

          return Wallet.Create(
            accountHolder: accountHolder,
            account: account,
            balance: Balance,
            id: Guid.Parse(Id));
     }

     public static WalletMapper FactoryByEntity(IWallet wallet)
       => new()
       {
            Id = wallet.Id.ToString(),
            Name = wallet.AccountHolder.Name,
            TaxId = wallet.AccountHolder.TaxId,
            Balance = wallet.Balance,
            AccountBranch = wallet.Account.Branch,
            AccountNumber = wallet.Account.Number,
            CreatedAt = DateTime.Now,
       };
}
