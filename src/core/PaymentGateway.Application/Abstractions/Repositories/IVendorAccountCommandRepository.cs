using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Application;

public interface IVendorAccountCommandRepository
{
    Task<bool> CreateVendorAccountAsync(VendorAccountEntity entity, CancellationToken cancellationToken);
}