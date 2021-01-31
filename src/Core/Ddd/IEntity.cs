namespace Core.Ddd
{
    // Source: https://github.com/abpframework/abp/blob/dev/framework/src/Volo.Abp.Ddd.Domain/Volo/Abp/Domain/Entities/IEntity.cs

    public interface IEntity
    {
        object[] GetKeys();
    }

    public interface IEntity<TKey> : IEntity
    {
        TKey Id { get; }
    }
}
