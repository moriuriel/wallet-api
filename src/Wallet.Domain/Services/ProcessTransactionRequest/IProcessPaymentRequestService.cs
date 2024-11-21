using Wallets.Domain.Shared;
using Wallets.Domain.Entities.Interfaces;

namespace Wallets.Domain.Services.ProcessTransactionRequest;

public interface IProcessTransactionRequestService
{
     Result Proccess(
         IWallet payer,
         IWallet receiver,
         decimal amount);
}