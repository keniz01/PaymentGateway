using MediatR;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions.Commands
{
    public interface ICommandHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
        where TQuery : ICommand<TResult>
    {
    }
}

