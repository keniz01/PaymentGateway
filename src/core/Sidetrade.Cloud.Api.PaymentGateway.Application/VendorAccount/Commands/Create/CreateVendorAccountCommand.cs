using Sidetrade.Cloud.Api.PaymentGateway.Application.Shared;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application.VendorAccount.Commands.Create
{
    public record CreateVendorAccountCommand(int MemberId, int MetaMemberId, 
    string ApiPublicKey, string ApiSecretKey, bool IsActivated) : ICommand<CreateVendorAccountCommandResult>;
}

