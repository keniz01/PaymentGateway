using FluentValidation;

namespace PaymentGateway.Presentation.Validators;

public class CorrelationIdValidator : AbstractValidator<Guid>
{
    public CorrelationIdValidator()
    {
        RuleFor(correlationId => correlationId).Empty();
        RuleFor(correlationId => correlationId).Equals(Guid.Empty);
    }
}