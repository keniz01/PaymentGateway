using MediatR;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions.Queries
{
    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
    }
}

