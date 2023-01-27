using Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions.Commands;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application.Features.VendorAccountFeature.Commands.Create
{
    public record CreateVendorAccountCommand(int MemberId, int MetaMemberId,
        string ApiPublicKey, string ApiSecretKey, bool IsActivated) : ICommand<CreateVendorAccountCommandResult>;
}

