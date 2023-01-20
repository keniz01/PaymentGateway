using Microsoft.Extensions.Logging;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions.Commands;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application.VendorAccount.Commands.Create
{
    public class CreateVendorAccountCommandHandler : ICommandHandler<CreateVendorAccountCommand, CreateVendorAccountCommandResult>
    {
        private readonly IVendorAccountCommandRepository _vendorAccountCommandRepository;

        public CreateVendorAccountCommandHandler(IVendorAccountCommandRepository vendorAccountWriteRepository)
        {
            _vendorAccountCommandRepository = vendorAccountWriteRepository;
        }

        public async Task<CreateVendorAccountCommandResult> Handle(CreateVendorAccountCommand command, CancellationToken cancellationToken)
        {
            var isAccountCreated = await _vendorAccountCommandRepository.CreateVendorAccountAsync(command, cancellationToken);
            return new CreateVendorAccountCommandResult(isAccountCreated);
        }
    }
}

