using System;
using System.Collections.Generic;
using System.Reflection;

namespace Core.Ddd
{
    // Source: https://github.com/abpframework/abp/blob/dev/framework/src/Volo.Abp.Ddd.Domain/Volo/Abp/Domain/Entities/EntityHelper.cs

    public static class EntityHelper
    {
        public static bool EntityEquals(IEntity entity1, IEntity entity2)
        {
            if (entity1 == null || entity2 == null)
            {
                return false;
            }

            //Same instances must be considered as equal
            if (ReferenceEquals(entity1, entity2))
            {
                return true;
            }

            //Must have a IS-A relation of types or must be same type
            var typeOfEntity1 = entity1.GetType();
            var typeOfEntity2 = entity2.GetType();
            if (!typeOfEntity1.IsAssignableFrom(typeOfEntity2) && !typeOfEntity2.IsAssignableFrom(typeOfEntity1))
            {
                return false;
            }

            //Transient objects are not considered as equal
            if (HasDefaultKeys(entity1) && HasDefaultKeys(entity2))
                return false;

            var entity1Keys = entity1.GetKeys();
            var entity2Keys = entity2.GetKeys();

            if (entity1Keys.Length != entity2Keys.Length)
            {
                return false;
            }

            for (var i = 0; i < entity1Keys.Length; i++)
            {
                var entity1Key = entity1Keys[i];
                var entity2Key = entity2Keys[i];

                if (entity1Key == null)
                {
                    if (entity2Key == null)
                        continue;

                    return false;
                }

                if (entity2Key == null)
                    return false;

                if (entity1Key == default && entity2Key == default)
                    return false;

                if (!entity1Key!.Equals(entity2Key))
                    return false;
            }

            return true;
        }

        public static bool IsEntity(Type type)
        {
            return typeof(IEntity).IsAssignableFrom(type);
        }

        public static bool IsEntityWithId(Type type)
        {
            foreach (var interfaceType in type.GetInterfaces())
            {
                if (interfaceType.GetTypeInfo().IsGenericType &&
                    interfaceType.GetGenericTypeDefinition() == typeof(IEntity<>))
                    return true;
            }

            return false;
        }

        public static bool HasDefaultId<TKey>(IEntity<TKey> entity)
        {
            if (EqualityComparer<TKey>.Default.Equals(entity.Id, default))
            {
                return true;
            }

            //Workaround for EF Core since it sets int/long to min value when attaching to dbcontext
            if (typeof(TKey) == typeof(int))
            {
                return Convert.ToInt32(entity.Id) <= 0;
            }

            if (typeof(TKey) == typeof(long))
            {
                return Convert.ToInt64(entity.Id) <= 0;
            }

            return false;
        }

        private static bool IsDefaultKeyValue(object value)
        {
            if (value == null)
                return true;

            var type = value.GetType();

            //Workaround for EF Core since it sets int/long to min value when attaching to DbContext
            if (type == typeof(int))
                return Convert.ToInt32(value) <= 0;

            if (type == typeof(long))
            {
                return Convert.ToInt64(value) <= 0;
            }

            return value == default;
        }

        public static bool HasDefaultKeys(IEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            foreach (var key in entity.GetKeys())
                if (!IsDefaultKeyValue(key))
                    return false;

            return true;
        }

        public static Type? FindPrimaryKeyType<TEntity>()
            where TEntity : IEntity
        {
            return FindPrimaryKeyType(typeof(TEntity));
        }

        public static Type? FindPrimaryKeyType(Type entityType)
        {
            if (!typeof(IEntity).IsAssignableFrom(entityType))
            {
                throw new Exception(
                    $"Given {nameof(entityType)} is not an entity. It should implement {typeof(IEntity).AssemblyQualifiedName}!");
            }

            foreach (var interfaceType in entityType.GetTypeInfo().GetInterfaces())
            {
                if (interfaceType.GetTypeInfo().IsGenericType &&
                    interfaceType.GetGenericTypeDefinition() == typeof(IEntity<>))
                {
                    return interfaceType.GenericTypeArguments[0];
                }
            }

            return null;
        }
    }
}
