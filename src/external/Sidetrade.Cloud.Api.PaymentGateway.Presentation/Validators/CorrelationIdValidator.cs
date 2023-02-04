using FluentValidation;

namespace Sidetrade.Cloud.Api.PaymentGateway.Presentation.Validators;

public class CorrelationIdValidator : AbstractValidator<Guid>
{
    public CorrelationIdValidator()
    {
        RuleFor(correlationId => correlationId).Empty();
        RuleFor(correlationId => correlationId).Equals(Guid.Empty);
    }
}