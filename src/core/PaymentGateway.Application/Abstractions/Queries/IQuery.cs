using MediatR;

namespace PaymentGateway.Application.Abstractions.Queries
{
    public interface IQuery<TResult>: IRequest<TResult> 
    {
    }
}

