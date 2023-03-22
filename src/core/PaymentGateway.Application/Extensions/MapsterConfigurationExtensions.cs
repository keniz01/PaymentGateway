using Mapster;
using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.Application.Features.VendorAccountFeature.Commands.Create;
using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Application.Middleware;

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

