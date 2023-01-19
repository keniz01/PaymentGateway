using MediatR;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application.Shared
{
    public interface ICommand<TResult>: IRequest<TResult> 
    {
    }    
}

