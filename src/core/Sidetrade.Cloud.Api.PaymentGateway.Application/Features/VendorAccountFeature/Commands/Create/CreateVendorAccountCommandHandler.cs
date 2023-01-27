using MapsterMapper;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions.Commands;
using Sidetrade.Cloud.Api.PaymentGateway.Domain.Entities;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application.Features.VendorAccountFeature.Commands.Create
{
    public class CreateVendorAccountCommandHandler : ICommandHandler<CreateVendorAccountCommand, CreateVendorAccountCommandResult>
    {
        private readonly IVendorAccountCommandRepository _vendorAccountCommandRepository;
        private readonly IMapper _mapper;

        public CreateVendorAccountCommandHandler(IVendorAccountCommandRepository vendorAccountCommandRepository, IMapper mapper)
        {
            _vendorAccountCommandRepository = vendorAccountCommandRepository;
            _mapper = mapper;
        }

        public async Task<CreateVendorAccountCommandResult> Handle(CreateVendorAccountCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<VendorAccountEntity>(command);
            var response = await _vendorAccountCommandRepository.CreateVendorAccountAsync(entity, cancellationToken);
            return new CreateVendorAccountCommandResult(response);
        }
    }
}

