using MediatR;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions.Queries
{
    public interface IQuery<TResult>: IRequest<TResult> 
    {
    }
}

