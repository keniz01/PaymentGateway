namespace Sidetrade.Cloud.Api.PaymentGateway.Application;

public interface IVendorAccountReadOnlyRepository
{
    Task<GetActiveVendorAccountResponse> GetActiveVendorAccountAsync(GetActiveVendorAccountRequest request, CancellationToken cancellationToken);
}