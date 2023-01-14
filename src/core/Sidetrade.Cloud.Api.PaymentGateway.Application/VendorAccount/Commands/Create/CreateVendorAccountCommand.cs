using Sidetrade.Cloud.Api.PaymentGateway.Application.Shared;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application.VendorAccount.Commands.Create
{
    public class CreateVendorAccountCommand: CommandBase
	{
		public CreateVendorAccountCommand(Guid correlationId)
			: base(correlationId)
		{
		}

        public required int VendorId { get; set; }
        public int MetaVendorId { get; set; }
        public required string PublicKey { get; set; }
        public required string SecretKey { get; set; }
        public required bool IsActivated { get; set; }
    }

    public record CreateVendorAccountCommandResult(bool IsVendorAcountCreated);
}

