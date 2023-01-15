using Sidetrade.Cloud.Api.PaymentGateway.Application.Shared;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application.VendorAccount.Commands.Create
{
    public class CreateVendorAccountCommand: CommandBase
	{
		public CreateVendorAccountCommand(Guid correlationId)
			: base(correlationId)
		{
		}

        public required int MemberId { get; set; }
        public int MetaMemberId { get; set; }
        public required string ApiPublicKey { get; set; }
        public required string ApiSecretKey { get; set; }
        public required bool IsActivated { get; set; }
    }

    public record CreateVendorAccountCommandResult(bool IsVendorAcountCreated);
}

