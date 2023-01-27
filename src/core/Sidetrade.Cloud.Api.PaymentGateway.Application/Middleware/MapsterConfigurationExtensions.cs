using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Features.VendorAccountFeature.Commands.Create;
using Sidetrade.Cloud.Api.PaymentGateway.Domain.Entities;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application.Middleware;

public static class MapsterConfigurationExtensions
{
    public static IServiceCollection AddApplicationMappings(this IServiceCollection services)
    {
        TypeAdapterConfig<CreateVendorAccountCommand, VendorAccountEntity>
        .NewConfig()
        .ConstructUsing(src => VendorAccountEntity.Create(src.MemberId,
            src.MetaMemberId, src.ApiSecretKey, src.ApiSecretKey, src.IsActivated));

        return services;
    }
}

