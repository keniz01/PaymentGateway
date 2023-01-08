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
}

//string dbConnectionString = this.Configuration.GetConnectionString("dbConnection1");

//// Inject IDbConnection, with implementation from SqlConnection class.
//services.AddTransient<IDbConnection>((sp) => new SqlConnection(dbConnectionString));

