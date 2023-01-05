using MediatR;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application;

public abstract class RequestBase<TResponse> : IRequest<TResponse>
{
    public RequestBase(Guid correlationId) => CorrelationId = correlationId;
    public Guid CorrelationId { get; }
}
