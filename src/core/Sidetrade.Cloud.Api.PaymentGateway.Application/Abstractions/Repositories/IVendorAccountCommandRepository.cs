using Sidetrade.Cloud.Api.PaymentGateway.Application.VendorAccount.Commands.Create;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application;

public interface IVendorAccountCommandRepository
{
    Task<bool> CreateVendorAccountAsync(CreateVendorAccountCommand command, CancellationToken cancellationToken);
}