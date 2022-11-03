using System.ComponentModel.DataAnnotations.Schema;

namespace FamilyPlaner.Domain.Common;

public abstract class Entity
{
    private readonly List<DomainEvent> _domainEvents = new();

    protected Entity()
    { }

    public Guid Id { get; set; }
    public DateTimeOffset Created { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTimeOffset LastModified { get; set; }
    public Guid LastModifiedBy { get; set; }
    public bool Deleted { get; set; }

    [NotMapped]
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
