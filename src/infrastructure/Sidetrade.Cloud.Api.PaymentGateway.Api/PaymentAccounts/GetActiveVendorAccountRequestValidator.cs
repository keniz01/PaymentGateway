using FluentValidation;

namespace Sidetrade.Cloud.Api.PaymentGateway.Api.PaymentAccounts;

public class VendorIdValidator : AbstractValidator<int>
{
    public VendorIdValidator()
    {
        RuleFor(vendorId => vendorId).GreaterThan(0);
    }
}

public class CorrelationIdValidator : AbstractValidator<Guid>
{
    public CorrelationIdValidator()
    {
        RuleFor(correlationId => correlationId).Empty();
        RuleFor(correlationId => correlationId).Equals(Guid.Empty);
    }
}