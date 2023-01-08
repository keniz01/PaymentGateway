using MediatR;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application.Shared
{
    public abstract class QueryBase<TResult> : CorrelationIdBase, IRequest<TResult>
    {
        public QueryBase(Guid correlationId) : base(correlationId)
        {
        }
    }
}

