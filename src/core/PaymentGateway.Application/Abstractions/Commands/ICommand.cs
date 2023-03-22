using MediatR;

namespace PaymentGateway.Application.Abstractions.Commands
{
    public interface ICommand<TResult>: IRequest<TResult> 
    {
    }    
}

