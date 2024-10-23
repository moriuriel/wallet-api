using Wallets.Domain.ValueObjects.Interfaces;

namespace Wallets.Domain.ValueObjects;

public sealed class AccountHolder : IAccountHolder
{
     private AccountHolder(string name, string taxId)
     {
          Name = name;
          TaxId = taxId;
     }

     public string Name { get; private set; }

     public string TaxId { get; private set; }

     public static AccountHolder Factory(
         string name,
         string taxId)
         => new(
             name,
             taxId);
}