using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;
using MediatR;
using Sidetrade.Cloud.Api.PaymentGateway.Application;
using System.Net.Mime;

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
        Summary = "Gets an active vendor payment gateway account",
        Description = "Gets an active vendor payment gateway account",
        OperationId = "GetActiveVendorAccountAsync",
        Tags = new[] { "Payment Gateway" }
     )]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("account")]
    public async Task<IResult> GetActiveVendorAccountAsync(
        [FromHeader(Name = HttpRequestHeaderNameConstants.VENDOR_ID)]
        [Required]
        int vendorId,
        [FromHeader(Name = HttpRequestHeaderNameConstants.CORRELATION_ID)]
        [Required]
        Guid correlationId,
        CancellationToken cancellationToken)
    {

        if(!(
            new VendorIdValidator().Validate(vendorId).IsValid
            || new CorrelationIdValidator().Validate(correlationId).IsValid)
        )
        {
            return Results.BadRequest("Required headers missing.");
        }

        var request = new GetActiveVendorAccountRequest(vendorId, correlationId);
        var response = await _mediator.Send(request, cancellationToken);

        return Results.Ok(new { response.PublicKey });
    }
}