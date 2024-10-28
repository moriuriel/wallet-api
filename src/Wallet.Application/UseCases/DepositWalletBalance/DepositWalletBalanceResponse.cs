using System;

namespace Wallets.Application.UseCases.DepositWalletBalance;

public sealed class DepositWalletBalanceResponse
{
     public Guid Id { get; private set; }

     private DepositWalletBalanceResponse(Guid id) 
          => Id = id;

     public static DepositWalletBalanceResponse Factory(Guid id) 
          => new(id);
}
