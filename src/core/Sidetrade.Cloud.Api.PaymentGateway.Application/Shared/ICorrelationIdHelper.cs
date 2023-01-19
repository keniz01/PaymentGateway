namespace Sidetrade.Cloud.Api.PaymentGateway.Application.Shared;

public interface ICorrelationIdHelper
{
    Guid Get();
    void Set(Guid correlationId);
}