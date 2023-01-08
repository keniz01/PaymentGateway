namespace Sidetrade.Cloud.Api.PaymentGateway.Application.Shared
{
    public abstract class ResultBase : CorrelationIdBase
    {
        public ResultBase(Guid correlationId) : base(correlationId)
        {
        }
    }
}

