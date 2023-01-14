using System.Data;
using MediatR;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application.Shared
{
    public abstract class CommandBase: CorrelationIdBase, IRequest
    {
        public CommandBase(Guid correlationId) : base(correlationId)
        {
        }
    }

    public abstract class CommandBase<TResult>: CorrelationIdBase, IRequest<TResult>
    {
        public CommandBase(Guid correlationId) : base(correlationId)
        {
        }
    }
}

