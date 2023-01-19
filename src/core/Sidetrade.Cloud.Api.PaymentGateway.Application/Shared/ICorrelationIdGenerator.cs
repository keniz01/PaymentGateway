namespace Sidetrade.Cloud.Api.PaymentGateway.Application.Shared;

public interface ICorrelationIdGenerator
{
    Guid Get();
    void Set(Guid correlationId);
}