namespace Wallets.Domain.ValueObjects.Interfaces;
public interface IAccountHolder
{
    string Name { get; }
    string TaxId { get; }
}