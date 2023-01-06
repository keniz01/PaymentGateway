using FluentValidation;

namespace Sidetrade.Cloud.Api.PaymentGateway.Api.PaymentAccounts;

public class CorrelationIdValidator : AbstractValidator<Guid>
{
    public CorrelationIdValidator()
    {
        RuleFor(correlationId => correlationId).Empty();
        RuleFor(correlationId => correlationId).Equals(Guid.Empty);
    }
}