using Wallets.Domain.Entities.Interfaces;

namespace Wallets.Domain.Entities;

public class Entity : IEntity
{
    public Guid Id { get; protected init; }

    protected Entity(Guid id)
        => Id = id;
}