using MassTransit;
using Microsoft.Extensions.Logging;
using Sidetrade.Cloud.Api.PaymentGateway.Application;
using Sidetrade.Cloud.Api.PaymentGateway.Contracts;
using Sidetrade.Cloud.Api.PaymentGateway.Domain.DomainEvents;
using Sidetrade.Cloud.Api.PaymentGateway.Domain.Entities;

namespace Sidetrade.Cloud.Api.PaymentGateway.Consumers
{
    public class CreateVendorAcccountConsumer : IConsumer<CreateVendorAccountMessage>
    {
        private readonly IVendorAccountCommandRepository _vendorAccountCommandRepository;
        private readonly ILogger<CreateVendorAcccountConsumer> _logger;


        public CreateVendorAcccountConsumer(
            IVendorAccountCommandRepository vendorAccountWriteRepository,
            ILogger<CreateVendorAcccountConsumer> logger
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
