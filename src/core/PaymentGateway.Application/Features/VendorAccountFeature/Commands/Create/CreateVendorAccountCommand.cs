using PaymentGateway.Application.Abstractions.Commands;

namespace PaymentGateway.Application.Features.VendorAccountFeature.Commands.Create
{
    public record CreateVendorAccountCommand(int MemberId, int MetaMemberId,
    string ApiPublicKey, string ApiSecretKey, bool IsActivated) : ICommand<CreateVendorAccountCommandResult>;
}

