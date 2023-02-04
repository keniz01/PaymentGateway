using FluentValidation;

namespace Sidetrade.Cloud.Api.PaymentGateway.Presentation.Validators;

public class VendorIdValidator : AbstractValidator<int>
{
    public VendorIdValidator()
    {
        RuleFor(vendorId => vendorId).GreaterThan(0);
    }
}