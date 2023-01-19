namespace Sidetrade.Cloud.Api.PaymentGateway.Application.Shared;

public class CorrelationIdGenerator: ICorrelationIdGenerator
{
    private Guid _correlationId;

    public Guid Get() => _correlationId;

    public void Set(Guid correlationId) => _correlationId = correlationId;
}