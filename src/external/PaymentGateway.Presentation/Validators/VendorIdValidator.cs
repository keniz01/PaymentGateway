using FluentValidation;

namespace PaymentGateway.Presentation.Validators;

public class VendorIdValidator : AbstractValidator<int>
{
    public VendorIdValidator()
    {
        RuleFor(vendorId => vendorId).GreaterThan(0);
    }
}