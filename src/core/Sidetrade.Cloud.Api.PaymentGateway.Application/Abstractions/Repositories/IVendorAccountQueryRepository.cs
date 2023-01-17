namespace Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions;

public interface IVendorAccountQueryRepository
{
    Task<T> GetAsync<T>(string sql, object parameters, CancellationToken cancellationToken) where T : class;
}