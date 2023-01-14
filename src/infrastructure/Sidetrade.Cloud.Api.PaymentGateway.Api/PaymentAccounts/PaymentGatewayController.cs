using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;
using MediatR;
using Sidetrade.Cloud.Api.PaymentGateway.Application.VendorAccount;
using System.Net.Mime;
using Sidetrade.Cloud.Api.PaymentGateway.Application.VendorAccount.Commands.Create;

namespace Sidetrade.Cloud.Api.PaymentGateway.Api.PaymentAccounts;

[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/payment-gateway")]
public class PaymentGatewayController: ControllerBase
{
    private readonly ILogger<PaymentGatewayController> _logger;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public PaymentGatewayController(ILogger<PaymentGatewayController> logger, IMapper mapper, IMediator mediator)
    {
        _logger = logger;
        _mapper = mapper;
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
        [FromHeader(Name = HttpRequestHeaderNameConstants.VENDOR_ID)][Required] int vendorId,
        [FromHeader(Name = HttpRequestHeaderNameConstants.META_VENDOR_ID)][Required] int metaVendorId,
        [FromHeader(Name = HttpRequestHeaderNameConstants.CORRELATION_ID)][Required] Guid correlationId,
        [FromBody]CreateVendorAccountRequest request,
        CancellationToken cancellationToken)
    {
        if(!(new VendorIdValidator().Validate(vendorId).IsValid 
            || new CorrelationIdValidator().Validate(correlationId).IsValid)
        )
        {
            return Results.BadRequest("Required headers missing.");
        }

        var command = new CreateVendorAccountCommand(correlationId)
        {
            VendorId = vendorId,
            MetaVendorId = metaVendorId,
            PublicKey = request.PublicKey,
            SecretKey = request.SecretKey,
            IsActivated = request.IsActivated
        };

        await _mediator.Send(command, cancellationToken);

        return Results.Ok(new { IsVendorAcountCreated = true });
    }    

    [SwaggerOperation
    (
        Summary = "Gets an active vendor payment gateway account",
        Description = "Gets an active vendor payment gateway account",
        OperationId = "GetActiveVendorAccountAsync",
        Tags = new[] { "Payment Gateway" }
     )]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("vendor-account")]
    public async Task<IResult> GetActiveVendorAccountAsync(
        [FromHeader(Name = HttpRequestHeaderNameConstants.VENDOR_ID)]
        [Required]
        int vendorId,
        [FromHeader(Name = HttpRequestHeaderNameConstants.CORRELATION_ID)]
        [Required]
        Guid correlationId,
        CancellationToken cancellationToken)
    {
        if(!(new VendorIdValidator().Validate(vendorId).IsValid
            || new CorrelationIdValidator().Validate(correlationId).IsValid))
        {
            return Results.BadRequest("Required headers missing.");
        }

        var request = new GetActiveVendorAccountQuery(vendorId, correlationId);
        var response = await _mediator.Send(request, cancellationToken);

        return Results.Ok(new { response.PublicKey });
    }
}