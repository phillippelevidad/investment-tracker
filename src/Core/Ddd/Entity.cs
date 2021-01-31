using System;
using System.Diagnostics.CodeAnalysis;

namespace Core.Ddd
{
    // Source: https://github.com/abpframework/abp/blob/dev/framework/src/Volo.Abp.Ddd.Domain/Volo/Abp/Domain/Entities/Entity.cs

    [Serializable]
    public abstract class Entity : IEntity
    {
        public abstract object[] GetKeys();

        public bool EntityEquals(IEntity other)
        {
            return EntityHelper.EntityEquals(this, other);
        }

        public override string ToString()
        {
            return $"[ENTITY: {GetType().Name}] Keys = {string.Join(", ", GetKeys())}";
        }
    }

    [Serializable]
    public abstract class Entity<TKey> : Entity, IEntity<TKey>
    {
        [NotNull]
        public virtual TKey Id { get; protected set; }

        protected Entity()
        {
        }

        protected Entity(TKey id)
        {
            Id = id;
        }

        public override object[] GetKeys()
        {
            return new object[] { Id };
        }

        public override string ToString()
        {
            return $"[ENTITY: {GetType().Name}] Id = {Id}";
        }
    }
}
