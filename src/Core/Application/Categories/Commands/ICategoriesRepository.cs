using Core.Domain.Categories;
using Core.Functional;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Categories.Commands
{
    public interface ICategoriesRepository
    {
        Task<Category> FindAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Result> AddAsync(Category category, CancellationToken cancellationToken = default);

        Task<Result> UpdateAsync(Category category, CancellationToken cancellationToken = default);

        Task<Result> RemoveAsync(Category category, CancellationToken cancellationToken = default);
    }
}
