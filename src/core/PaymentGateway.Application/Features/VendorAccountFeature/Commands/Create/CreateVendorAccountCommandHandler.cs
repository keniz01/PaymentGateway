using MapsterMapper;
using PaymentGateway.Application.Abstractions.Commands;
using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Application.Features.VendorAccountFeature.Commands.Create
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

