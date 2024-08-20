using Wallets.Domain.Shared;
using Wallets.Domain.Entities;

namespace Wallets.Domain.DomainServices.ProccessWithdrawRequest;

public interface IProccessWithdrawRequestService
{
    Result Proccess(Wallet payer, Wallet receiver, float amount);
}