using FluentValidation;

using Wallets.Application.Commons;
using Wallets.Application.UseCases.Shared;

namespace Wallets.Application.UseCases.CreateWallet;

public class CreateWalletRequestValidator : AbstractValidator<CreateWalletRequest>
{
     public CreateWalletRequestValidator()
     {
          RuleFor(_ => _.AccountHolderModel)
            .SetValidator(new AccountHolderModelValidator());

          RuleFor(_ => _.AccountModel)
            .SetValidator(new AccountModelValidator());
     }
}

public class AccountHolderModelValidator : AbstractValidator<AccountHolderModel>
{
     public AccountHolderModelValidator()
     {
          RuleFor(_ => _.Name)
            .NotEmpty()
            .WithMessage(Messsage.ValidationError.EMPTY_FIELD)
            .MaximumLength(100);

          RuleFor(_ => _.TaxId)
            .NotEmpty()
            .WithMessage(Messsage.ValidationError.EMPTY_FIELD);
     }
}

public class AccountModelValidator : AbstractValidator<AccountModel>
{
     public AccountModelValidator()
     {
          RuleFor(_ => _.Branch)
            .NotEmpty()
            .WithMessage(Messsage.ValidationError.EMPTY_FIELD)
            .MaximumLength(6)
            .MinimumLength(6);

          RuleFor(_ => _.Number)
            .NotEmpty()
            .WithMessage(Messsage.ValidationError.EMPTY_FIELD)
            .MaximumLength(2)
            .MinimumLength(2);
     }
}