using MediatR;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions.Commands
{
    public interface ICommand<TResult>: IRequest<TResult> 
    {
    }    
}

