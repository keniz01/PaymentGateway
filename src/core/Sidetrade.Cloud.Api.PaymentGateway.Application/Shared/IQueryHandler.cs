using MediatR;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application.Shared
{
    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
    }
}

