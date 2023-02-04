using FluentAssertions;
using System.Text;
using System.Text.Json;
using System.Net.Mime;
using Bogus;
using System.Net;
using Sidetrade.Cloud.Api.PaymentGateway.Presentation.Constants;

namespace Sidetrade.Cloud.Api.PaymentGateway.Tests;

[TestFixture]
public class PaymentGatewayControllerTests
{
    private TestWebApplicationFactory _webApplicationFactory = null!;
    private HttpClient _client = null!;

    [SetUp]
    public void Setup()
    {
        _webApplicationFactory = new TestWebApplicationFactory();
        _client = _webApplicationFactory.CreateClient();
        _client.DefaultRequestHeaders.Add(HttpRequestHeaderNameConstants.META_MEMBER_ID, "448058");
        _client.DefaultRequestHeaders.Add(HttpRequestHeaderNameConstants.MEMBER_ID, "448058");
        _client.DefaultRequestHeaders.Add(HttpRequestHeaderNameConstants.CORRELATION_ID, Guid.NewGuid().ToString());
    }

    [Test(Description = "CreateVendorAccountAsync - Creates a new vendor account.")]
    [Category("PaymentGatewayController")]
    public async Task PaymentGatewayController_CreateVendorAccountAsync_Should_create_new_vendor_account()
    {        
        var faker = new Faker();
        var payload = new 
        {
            ApiPublicKey = $"pk_test_{faker.Random.AlphaNumeric(25)}",
            ApiSecretKey = $"sk_test_{faker.Random.AlphaNumeric(25)}",
            IsActivated = true
        };
        var json = JsonSerializer.Serialize<dynamic>(payload);
        var content = new StringContent(json, Encoding.Default, MediaTypeNames.Application.Json);
        
        var response = await _client.PostAsync(
            PaymentGatewayControllerApiEndpointConstants.CREATE_VENDOR_ACCOUNT,
            content,
            CancellationToken.None);

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Test(Description = "GetVendorAccountAsync - Returns a valid public key")]
    [Category("PaymentGatewayController")]
    public async Task GetVendorAccountAsync__Assert_that_the_call_succeeded()
    {
        var responseMessage = await GetVendorAccountResponseMesssageAsync();
        responseMessage.EnsureSuccessStatusCode();
        responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [TearDown]
    public void TearDown()
    {
        _client.Dispose();
        _client = null!;
    }

    private async Task<HttpResponseMessage> GetVendorAccountResponseMesssageAsync()
    {
        var response = await _client.GetAsync(PaymentGatewayControllerApiEndpointConstants.GET_VENDOR_ACCOUNT, CancellationToken.None);
        return response;
    }
}