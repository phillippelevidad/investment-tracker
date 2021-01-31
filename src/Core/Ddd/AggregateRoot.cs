using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Core.Ddd
{
    // Source: https://github.com/abpframework/abp/blob/dev/framework/src/Volo.Abp.Ddd.Domain/Volo/Abp/Domain/Entities/BasicAggregateRoot.cs

    [Serializable]
    public abstract class AggregateRoot : Entity, IAggregateRoot
    {
        private readonly ICollection<IDomainEvent> events = new Collection<IDomainEvent>();

        protected virtual void AddEvent(IDomainEvent eventData)
        {
            events.Add(eventData);
        }

        public virtual IReadOnlyList<IDomainEvent> ListEvents()
        {
            return events.ToList().AsReadOnly();
        }

        public virtual void ClearEvents()
        {
            events.Clear();
        }
    }

    [Serializable]
    public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot<TKey>
    {
        private readonly ICollection<IDomainEvent> events = new Collection<IDomainEvent>();

        protected AggregateRoot()
        {
        }

        protected AggregateRoot(TKey id) : base(id)
        {
        }

        protected virtual void AddEvent(IDomainEvent eventData)
        {
            events.Add(eventData);
        }

        public virtual IReadOnlyList<IDomainEvent> ListEvents()
        {
            return events.ToList().AsReadOnly();
        }

        public virtual void ClearEvents()
        {
            events.Clear();
        }
    }
}
