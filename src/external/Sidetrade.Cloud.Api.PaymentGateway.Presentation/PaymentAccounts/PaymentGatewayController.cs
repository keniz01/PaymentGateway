using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using MapsterMapper;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sidetrade.Cloud.Api.PaymentGateway.Application.VendorAccount.Queries;
using Sidetrade.Cloud.Api.PaymentGateway.Contracts;
using Swashbuckle.AspNetCore.Annotations;

namespace Sidetrade.Cloud.Api.PaymentGateway.Presentation.PaymentAccounts;

[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/payment-gateway")]
public class PaymentGatewayController: ControllerBase
{
    private readonly ILogger<PaymentGatewayController> _logger;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMediator _mediator;

    public PaymentGatewayController(
        IMediator mediator,
        ILogger<PaymentGatewayController> logger,
        IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
        _mediator = mediator;
    }

    [SwaggerOperation
    (
        Summary = "Creates a new vendor payment gateway account.",
        Description = "Creates a new vendor payment gateway account.",
        OperationId = "CreateVendorAccountAsync",
        Tags = new[] { "Payment Gateway" }
     )]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost("vendor-account")]
    public async Task<IResult> CreateVendorAccountAsync(
        [FromHeader(Name = HttpRequestHeaderNameConstants.MEMBER_ID)][Required] int memberId,
        [FromHeader(Name = HttpRequestHeaderNameConstants.META_MEMBER_ID)][Required] int metaMemberId,
        [FromHeader(Name = HttpRequestHeaderNameConstants.CORRELATION_ID)][Required] Guid correlationId,
        [FromBody]CreateVendorAccountRequest request,
        CancellationToken cancellationToken)
    {
        if(!(new VendorIdValidator().Validate(memberId).IsValid 
            || new CorrelationIdValidator().Validate(correlationId).IsValid)
        )
        {
            return Results.BadRequest("Required headers missing.");
        }

        var entity = _mapper.Map<CreateVendorAccountMessage>(request);
        await _publishEndpoint.Publish<CreateVendorAccountMessage>(entity, cancellationToken);

        return Results.Ok(new { IsVendorAcountCreated = true });
    }    

    [SwaggerOperation
    (
        Summary = "Gets an vendor payment gateway account",
        Description = "Gets an vendor payment gateway account",
        OperationId = "GetVendorAccountAsync",
        Tags = new[] { "Payment Gateway" }
     )]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("vendor-account")]
    public async Task<IResult> GetVendorAccountAsync(
        [FromHeader(Name = HttpRequestHeaderNameConstants.MEMBER_ID)][Required]int memberId,
        [FromHeader(Name = HttpRequestHeaderNameConstants.META_MEMBER_ID)][Required]int metaMemberId,        
        [FromHeader(Name = HttpRequestHeaderNameConstants.CORRELATION_ID)][Required]Guid correlationId,
        CancellationToken cancellationToken)
    {
        if(!(new VendorIdValidator().Validate(memberId).IsValid
            || new CorrelationIdValidator().Validate(correlationId).IsValid))
        {
            return Results.BadRequest("Required headers missing.");
        }

        var request = new GetVendorAccountQuery(memberId);
        var response = await _mediator.Send(request, cancellationToken);

        return Results.Ok(new { response.ApiPublicKey });
    }
}