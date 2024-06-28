

using FluentValidation;
using Snappfood.Application.Models.Request;
using Snappfood.Application.Resources;

namespace Snappfood.Application.Models.Validation;

public class ProductCreateValidator : AbstractValidator<ProductCreate>
{

    public ProductCreateValidator()
    {
        RuleFor(x => x.Title)
            .NotNull()
            .WithMessage(e => string.Format(Messages.IsRequired, nameof(e.Title)))
            .NotEmpty()
            .WithMessage(e => string.Format(Messages.IsRequired, nameof(e.Title)))
            .MaximumLength(40)
            .WithMessage(e => string.Format(Messages.MaximumLength, nameof(e.Title)));

        RuleFor(x => x.Price)
            .NotNull()
            .WithMessage(e => string.Format(Messages.IsRequired, nameof(e.Price)))
            .NotEmpty()
            .WithMessage(e => string.Format(Messages.IsRequired, nameof(e.Price)));

        RuleFor(x => x.Discount)
            .InclusiveBetween(0, 100)
            .WithMessage(e => string.Format(Messages.MustBeBetween, nameof(e.Discount)));
    }

}
