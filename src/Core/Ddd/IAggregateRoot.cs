using System.Collections.Generic;

namespace Ddd
{
    // Source: https://github.com/abpframework/abp/blob/dev/framework/src/Volo.Abp.Ddd.Domain/Volo/Abp/Domain/Entities/IAggregateRoot.cs

    public interface IAggregateRoot : IEntity
    {
        IReadOnlyList<IDomainEvent> ListEvents();

        void ClearEvents();
    }

    public interface IAggregateRoot<TKey> : IEntity<TKey>, IAggregateRoot
    {
    }
}
