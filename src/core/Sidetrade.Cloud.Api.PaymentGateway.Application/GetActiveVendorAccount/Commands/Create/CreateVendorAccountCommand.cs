using MediatR;
using Microsoft.Extensions.Logging;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Shared;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application.GetActiveVendorAccount.Commands.Create
{
    public class CreateVendorAccountCommand: CommandBase
	{
		public CreateVendorAccountCommand(Guid correlationId)
			: base(correlationId)
		{
		}

        public required int VendorId { get; set; }
    }

    public class CreateVendorAccountCommandHandler : CommandHandlerBase<CreateVendorAccountCommand>
    {
        private readonly IVendorAccountCommandRepository _vendorAccountCommandRepository;

        public CreateVendorAccountCommandHandler(
            IVendorAccountCommandRepository vendorAccountWriteRepository,
            ILogger<CreateVendorAccountCommandHandler> logger
         ) : base(logger)
        {
            _vendorAccountCommandRepository = vendorAccountWriteRepository;
        }

        public override async Task<Unit> Handle(CreateVendorAccountCommand command, CancellationToken cancellationToken)
        {
            await _vendorAccountCommandRepository.CreateVendorAccountAsync(command, cancellationToken);
            return Unit.Value;
        }
    }
}

