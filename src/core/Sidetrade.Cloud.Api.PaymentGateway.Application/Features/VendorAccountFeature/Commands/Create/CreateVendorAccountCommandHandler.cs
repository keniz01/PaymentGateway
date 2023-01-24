using MapsterMapper;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions.Commands;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions.Repositories;
using Sidetrade.Cloud.Api.PaymentGateway.Domain.Entities;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application.Features.VendorAccountFeature.Commands.Create
{
    public class CreateVendorAccountCommandHandler : ICommandHandler<CreateVendorAccountCommand, CreateVendorAccountCommandResult>
    {
        private readonly IVendorAccountCommandRepository _vendorAccountCommandRepository;

        public CreateVendorAccountCommandHandler(IVendorAccountCommandRepository vendorAccountCommandRepository)
        {
            _vendorAccountCommandRepository = vendorAccountCommandRepository;
        }

        public async Task<CreateVendorAccountCommandResult> Handle(CreateVendorAccountCommand command, CancellationToken cancellationToken)
        {
            var entity = VendorAccountEntity.Create(command.MemberId,
                command.MetaMemberId, command.ApiSecretKey, command.ApiPublicKey, command.IsActivated);

            var response = await _vendorAccountCommandRepository.CreateVendorAccountAsync(entity, cancellationToken);
            return new CreateVendorAccountCommandResult(response);
            ;
        }
    }
}

