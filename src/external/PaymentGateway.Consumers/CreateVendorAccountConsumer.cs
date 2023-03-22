using MassTransit;
using Microsoft.Extensions.Logging;
using PaymentGateway.Application;
using PaymentGateway.Contracts;
using PaymentGateway.Domain.DomainEvents;
using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Consumers
{
    public class CreateVendorAccountConsumer : IConsumer<CreateVendorAccountMessage>
    {
        private readonly IVendorAccountCommandRepository _vendorAccountCommandRepository;
        private readonly ILogger<CreateVendorAccountConsumer> _logger;


        public CreateVendorAccountConsumer(
            IVendorAccountCommandRepository vendorAccountWriteRepository,
            ILogger<CreateVendorAccountConsumer> logger
        )
        {
            _vendorAccountCommandRepository = vendorAccountWriteRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<CreateVendorAccountMessage> context)
        {
            var entity = VendorAccountEntity.Create(context.Message.MemberId, context.Message.MetaMemberId,
                context.Message.ApiSecretKey, context.Message.ApiPublicKey, context.Message.IsActivated);

            var isAccountCreated = await _vendorAccountCommandRepository.CreateVendorAccountAsync(entity, context.CancellationToken);

            await context.Publish<AccountCreatedEvent>(new
            {
                IsAccountCreated = isAccountCreated
            });
        }
    }
}
