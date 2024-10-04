using Wallets.Domain.Shared;
using Wallets.Domain.Entities.Interfaces;

namespace Wallets.Domain.Services.ProcessPaymentRequest;

public interface IProcessPaymentRequestService
{
    Result Proccess(
        IWallet payer,
        IWallet receiver,
        decimal amount);
}