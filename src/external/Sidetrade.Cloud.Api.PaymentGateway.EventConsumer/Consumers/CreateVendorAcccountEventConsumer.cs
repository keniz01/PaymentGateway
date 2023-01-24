using MapsterMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sidetrade.Cloud.Api.PaymentGateway.Application;
using Sidetrade.Cloud.Api.PaymentGateway.Domain.DomainEvents;
using Sidetrade.Cloud.Api.PaymentGateway.Domain.Entities;

namespace Sidetrade.Cloud.Api.PaymentGateway.EventConsumer.Consumers
{
    public class CreateVendorAcccountEventConsumer : IConsumer<VendorAccountEntity>
    {
        private readonly IVendorAccountCommandRepository _vendorAccountCommandRepository;
        private readonly ILogger<CreateVendorAcccountEventConsumer> _logger;

        public CreateVendorAcccountEventConsumer(
            IVendorAccountCommandRepository vendorAccountWriteRepository,
            ILogger<CreateVendorAcccountEventConsumer> logger
        )
        {
            _vendorAccountCommandRepository = vendorAccountWriteRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<VendorAccountEntity> context)
        {
            var isAccountCreated = await _vendorAccountCommandRepository.CreateVendorAccountAsync(context.Message, context.CancellationToken);

            await context.Publish<AccountCreatedEvent>(new
            {
                IsAccountCreated = isAccountCreated
            });
        }
    }
}
