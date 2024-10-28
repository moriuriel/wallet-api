using Bogus;

using Wallets.Application.UseCases.DepositWalletBalance;
using Wallets.UnitTest.Commons;

namespace Wallets.UnitTest.Application.UseCases.DepositWalletBalance;

public sealed class DepositWalletBalanceRequestBuilder
     : BuilderBase<DepositWalletBalanceRequest>
{
     public DepositWalletBalanceRequestBuilder()
     {
          walletId = FakerSingleton.GetInstance().Faker.Random.Guid();
          balance = FakerSingleton.GetInstance().Faker.Finance.Amount(decimals: 2);
     }

     private readonly Guid walletId;
     private readonly decimal balance;

     public override DepositWalletBalanceRequest Build() 
          => DepositWalletBalanceRequest.Factory(walletId, balance);
}
