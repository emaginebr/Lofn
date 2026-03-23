using FluentValidation;
using Lofn.DTO.ShopCart;

namespace Lofn.Domain.Validators
{
    public class ShopCartItemInfoValidator : AbstractValidator<ShopCartItemInfo>
    {
        public ShopCartItemInfoValidator()
        {
            RuleFor(x => x.Product)
                .NotNull().WithMessage("Product is required");

            RuleFor(x => x.Product.ProductId)
                .GreaterThan(0).WithMessage("ProductId must be greater than 0")
                .When(x => x.Product != null);

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0");
        }
    }
}
