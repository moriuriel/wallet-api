using Wallets.Domain.Shared;
using Wallets.Domain.Entities.Interfaces;

namespace Wallets.Domain.DomainServices.ProccessWithdrawRequest;

public interface IProccessWithdrawRequestService
{
    Result Proccess(IWallet payer, IWallet receiver, decimal amount);
}