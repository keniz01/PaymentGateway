namespace Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions.Correlation;

public interface ICorrelationIdHelper
{
    Guid Get();
    void Set(Guid correlationId);
}