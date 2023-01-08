namespace Sidetrade.Cloud.Api.PaymentGateway.Application;

public interface IVendorAccountQueryRepository
{
    Task<GetActiveVendorAccountQueryResult> GetActiveVendorAccountAsync(GetActiveVendorAccountQuery request, CancellationToken cancellationToken);
}
