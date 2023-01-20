using MassTransit;
using Microsoft.Extensions.Logging;
using Sidetrade.Cloud.Api.PaymentGateway.Application;
using Sidetrade.Cloud.Api.PaymentGateway.Application.VendorAccount.Commands.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sidetrade.Cloud.Api.PaymentGateway.EventConsumer.Consumers
{
    public class CreateVendorAcccountEventConsumer : IConsumer<CreateVendorAccountEvent>
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

        public async Task Consume(ConsumeContext<CreateVendorAccountEvent> context)
        {
            var command = new ()
            {

            }
            var isAccountCreated = await _vendorAccountCommandRepository.CreateVendorAccountAsync(command, cancellationToken);
            return new CreateVendorAccountCommandResult(isAccountCreated);
        }
    }
}
