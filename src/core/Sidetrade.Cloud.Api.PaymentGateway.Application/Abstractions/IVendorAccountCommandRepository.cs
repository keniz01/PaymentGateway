using Sidetrade.Cloud.Api.PaymentGateway.Application.GetActiveVendorAccount.Commands.Create;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application;

public interface IVendorAccountCommandRepository
{
    Task<GetActiveVendorAccountQueryResult> CreateVendorAccountAsync(CreateVendorAccountCommand command, CancellationToken cancellationToken);
}