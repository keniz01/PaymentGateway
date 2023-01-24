using Sidetrade.Cloud.Api.PaymentGateway.Domain.Entities;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions.Repositories;

public interface IVendorAccountCommandRepository
{
    Task<bool> CreateVendorAccountAsync(VendorAccountEntity entity, CancellationToken cancellationToken);
}