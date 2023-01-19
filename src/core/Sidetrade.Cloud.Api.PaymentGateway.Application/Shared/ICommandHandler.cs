using MediatR;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application.Shared
{
    public interface ICommandHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
        where TQuery : ICommand<TResult>
    {
    }
}

