using Wallets.Application.UseCases.Shared;
using Wallets.Domain.Entities.Interfaces;

namespace Wallets.Application.UseCases.FindWalletById;

public sealed class FindWalletByIdResponse
{
  private FindWalletByIdResponse(
    Guid id,
    AccountHolderModel accountHolder,
    AccountModel account,
    decimal? balance = null)
  {
    Id = id;
    Balance = balance;
    AccountHolder = accountHolder;
    Account = account;
  }

  public Guid Id { get; private set; }

  public decimal? Balance { get; private set; }

  public AccountHolderModel AccountHolder { get; private set; }

  public AccountModel Account { get; private set; }

  public static FindWalletByIdResponse FactoryByEntity(IWallet wallet)
    => new(
        id: wallet.Id,
        accountHolder: AccountHolderModel.FactoryByValueObject(wallet.AccountHolder),
        account: AccountModel.FactoryByValueObject(wallet.Account),
        balance: wallet.Balance);
}
