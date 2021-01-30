using Core.Functional;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Core.Application.Assets.Commands.AddOrUpdateAsset
{
    public class AddOrUpdateAssetCommand : IRequest<Result>
    {
        [Required]
        [NotNull]
        public Guid? Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        [NotNull]
        public string? Name { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        [NotNull]
        public string? Broker { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        [NotNull]
        public string? Category { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z]{3}$", ErrorMessage = "The Currency must be in the format \"AAA\".")]
        [NotNull]
        public string? Currency { get; set; }
    }
}
