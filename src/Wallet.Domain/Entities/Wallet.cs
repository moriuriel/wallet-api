using Wallets.Domain.Entities.Interfaces;
using Wallets.Domain.Shared;
using Wallets.Domain.ValueObjects.Interfaces;

namespace Wallets.Domain.Entities;

public class Wallet : AggregateRoot, IWallet
{
     private Wallet(
         IAccountHolder accountHolder,
         IAccount account,
         decimal? balance = null,
         Guid? id = null) : base(id: id ?? Guid.NewGuid())
     {
          Account = account;
          Balance = balance;
          AccountHolder = accountHolder;
     }

     public IAccountHolder AccountHolder { get; }
     public IAccount Account { get; }
     public decimal? Balance { get; private set; }

     public static Wallet Create(
         IAccountHolder accountHolder,
         IAccount account,
         decimal? balance = null,
         Guid? id = null)
         => new(
             accountHolder,
             account,
             balance,
             id);

     public Result Depoist(decimal amount)
     {
          if (amount <= ushort.MinValue)
               return Result.Failure(
                   DomainErrors.Wallet.AmountRequestedMustBeGreaterThanZero);

          if(Balance is null)
               Balance = amount;
          else
               Balance += amount;

          return Result.Success();
     }

     public Result Withdraw(decimal amount)
     {
          if (amount == ushort.MinValue)
               return Result.Failure(
                   DomainErrors.Wallet.AmountRequestedMustBeGreaterThanZero);

          if (Balance < amount)
               return Result.Failure(
                   DomainErrors.Wallet.InsufficientBalance);

          Balance -= amount;

          return Result.Success();
     }
}
