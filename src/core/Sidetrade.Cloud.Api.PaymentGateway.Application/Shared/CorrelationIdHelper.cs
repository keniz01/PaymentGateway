namespace Sidetrade.Cloud.Api.PaymentGateway.Application.Shared;

public class CorrelationIdHelper: ICorrelationIdHelper
{
    private Guid _correlationId;

    public Guid Get() => _correlationId;

    public void Set(Guid correlationId) => _correlationId = correlationId;
}