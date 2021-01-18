using FluentValidation;

namespace Api.Core.Application.Assets.Commands.AddAsset
{
    public class AddAssetCommandValidator : AbstractValidator<AddAssetCommand>
    {
        public AddAssetCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(x => x.Broker)
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(x => x.Category)
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(x => x.Currency)
                .NotEmpty()
                .Length(3);
        }
    }
}
