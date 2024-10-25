using System;

namespace Wallets.Application.UseCases.DepositWalletBalance;

public class DepositWalletBalanceRequest
{
     private DepositWalletBalanceRequest(
          Guid walletId, 
          decimal balance)
     {
          WalletId = walletId;
          Balance = balance;
     }

     public Guid WalletId { get; private set; }
     
     public decimal Balance { get; private set; }

     public static DepositWalletBalanceRequest Factory(
          Guid walletId, 
          decimal balance)
          => new(walletId, balance);
}
