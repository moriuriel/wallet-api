using FluentValidation;

namespace Wallets.Application.UseCases.DepositWalletBalance;

public class DepositWalletBalanceRequestValidator : AbstractValidator<DepositWalletBalanceRequest>
{
     public DepositWalletBalanceRequestValidator()
     {
          RuleFor(_ => _.Balance)
               .NotNull()
               .GreaterThan(0);
          
          RuleFor(_ => _.WalletId)
               .NotNull()
               .Must(_ => Guid.TryParse(_.ToString(), out var id));
     }
}
