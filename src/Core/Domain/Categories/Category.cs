using Core.Ddd;
using FluentGuard;
using System;

namespace Core.Domain.Categories
{
    public class Category : AggregateRoot<Guid>
    {
        private string name;

        private Category()
        {
        }

        public Category(Guid id, string name)
        {
            Id = id;
            this.name = name;
            Validate();
        }

        public string Name
        {
            get => name;
            set { name = value; Validate(); }
        }

        private void Validate()
        {
            Guard
                .With(name, nameof(Name)).NotNullOrWhiteSpace().Length(3, 30)
                .ThrowIfError();
        }
    }
}
