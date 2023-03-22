using MediatR;

namespace PaymentGateway.Application.Abstractions.Commands
{
    public interface ICommandHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
        where TQuery : ICommand<TResult>
    {
    }
}

