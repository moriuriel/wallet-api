using System;
using Wallets.Application.UseCases.CreateWallet;
using Wallets.Application.UseCases.Shared;
using Wallets.UnitTest.Commons;

namespace Wallets.UnitTest.Application.UseCases.CreateWallet.Builders;

public class CreateWalletRequestBuilder : BuilderBase<CreateWalletRequest>
{
    private readonly AccountHolderModel _accountHolderModel;
    private readonly AccountModel _accountModel;
    public CreateWalletRequestBuilder()
    {
        _accountHolderModel = new AccountHolderModelBuilder().Build();
        _accountModel = new AccountModelBuilder().Build();
    }
    public override CreateWalletRequest Build()
    => new()
    {
        AccountHolderModel = _accountHolderModel,
        AccountModel = _accountModel
    };
}
