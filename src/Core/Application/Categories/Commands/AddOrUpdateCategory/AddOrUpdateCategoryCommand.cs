using Core.Functional;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Core.Application.Categories.Commands.AddOrUpdateCategory
{
    public class AddOrUpdateCategoryCommand : IRequest<Result>
    {
        [Required]
        [NotNull]
        public Guid? Id { get; set; }


        [Required]
        [StringLength(30, MinimumLength = 3)]
        [NotNull]
        public string? Name { get; set; }
    }
}
