namespace PaymentGateway.Application.Abstractions.Correlation;

public class CorrelationIdHelper: ICorrelationIdHelper
{
    private Guid _correlationId;

    public Guid Get() => _correlationId;

    public void Set(Guid correlationId) => _correlationId = correlationId;
}