﻿namespace Wallets.Domain.Shared;

public class DomainErrors
{
    public static class Wallet
    {
        public static readonly Error InsufficientBalance = new(
            "Wallet.Balance",
            "Insufficient balance");

        public static readonly Error AmountRequestedMustBeGreaterThanZero = new(
            "Wallet.Balance",
            "Amount requested must be greater than zero");
    }
}