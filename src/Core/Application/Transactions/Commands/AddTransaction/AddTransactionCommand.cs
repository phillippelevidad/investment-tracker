using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using OperationEnum = Core.Domain.Transactions.Operation;

namespace Core.Application.Transactions.Commands.AddTransaction
{
    public class AddTransactionCommand : IValidatableObject
    {
        [Required]
        [NotNull]
        public Guid? Id { get; set; }

        [Required]
        [NotNull]
        public Guid? AssetId { get; set; }

        [Required]
        [NotNull]
        public string? Operation { get; set; }

        [Required]
        [NotNull]
        public float? Quantity { get; set; }

        [Required]
        [NotNull]
        public decimal? UnitPrice { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z]{3}$", ErrorMessage = "The Currency must be in the format \"AAA\".")]
        [NotNull]
        public string? Currency { get; set; }

        [Required]
        [NotNull]
        public DateTime? DateTime { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!OperationEnum.CanParseFromId(Operation))
            {
                var validIds = OperationEnum.ListAllIds().JoinAsString();

                yield return new ValidationResult(
                    $"Operation '{Operation}' is invalid. Valid values are: {validIds}.",
                    new[] { nameof(Operation) });
            }
        }
    }
}
