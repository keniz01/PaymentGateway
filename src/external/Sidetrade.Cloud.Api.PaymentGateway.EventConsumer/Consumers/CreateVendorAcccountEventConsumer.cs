using MapsterMapper;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Features.VendorAccountFeature.Commands.Create;
using Sidetrade.Cloud.Api.PaymentGateway.Domain.DomainEvents;
using Sidetrade.Cloud.Api.PaymentGateway.EventMessages;

namespace Sidetrade.Cloud.Api.PaymentGateway.EventConsumer.Consumers
{
    public class CreateVendorAcccountEventConsumer : IConsumer<CreateVendorAccountMessage>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CreateVendorAcccountEventConsumer> _logger;
        private readonly IMapper _mapper;

        public CreateVendorAcccountEventConsumer(
            ILogger<CreateVendorAcccountEventConsumer> logger,
            IMapper mapper,
            IMediator mediator
        )
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<CreateVendorAccountMessage> context)
        {
            var command = _mapper.Map<CreateVendorAccountCommand>(context.Message);
            var isAccountCreated = await _mediator.Send(command, context.CancellationToken);

            await context.Publish<AccountCreatedEvent>(new
            {
                IsAccountCreated = isAccountCreated
            });
            _logger.LogInformation("********* Request Id: {CorrelationId} completed. *********", context.CorrelationId);
        }
    }
}
