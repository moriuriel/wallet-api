using Wallets.Domain.Entities;
using Wallets.Domain.Shared;

namespace Wallets.Domain.DomainServices.ProccessWithdrawRequest;

public sealed class ProccessWithdrawRequestService : IProccessWithdrawRequestService
{
    public Result Proccess(Wallet payer, Wallet receiver, float amount)
    {
        var withdrawResult = payer.Withdraw(amount);

        if (withdrawResult.IsFailure)
            return withdrawResult;

        var depoistResult = receiver.Depoist(amount);
        if (withdrawResult.IsFailure)
            return depoistResult;

        return Result.Success();
    }
}