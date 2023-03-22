namespace PaymentGateway.Domain.DomainEvents;
public record AccountCreatedEvent
{
    public required bool IsAccountCreated { get; init; }
}