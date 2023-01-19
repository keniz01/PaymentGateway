using MediatR;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application.Shared
{
    public interface IQuery<TResult>: IRequest<TResult> 
    {
    }
}

