using MediatR;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application;

public abstract class CorrelationIdBase
{
    public CorrelationIdBase(Guid correlationId) => CorrelationId = correlationId;
    public Guid CorrelationId { get; private set; }

    public void SetCorrelationId(Guid correlationId) => CorrelationId = correlationId;
}
