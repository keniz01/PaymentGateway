using FluentValidation;

namespace Sidetrade.Cloud.Api.PaymentGateway.Presentation.PaymentAccounts;

public class VendorIdValidator : AbstractValidator<int>
{
    public VendorIdValidator()
    {
        RuleFor(vendorId => vendorId).GreaterThan(0);
    }
}