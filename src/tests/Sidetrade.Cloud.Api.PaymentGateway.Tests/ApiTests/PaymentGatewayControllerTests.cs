using FluentAssertions;
using Sidetrade.Cloud.Api.PaymentGateway.Api.PaymentAccounts;
using System.Text.Json;

namespace Sidetrade.Cloud.Api.PaymentGateway.Tests;

[TestFixture]
public class PaymentGatewayControllerTests
{
    private TestWebApplicationFactory _webApplicationFactory = null!;

    [SetUp]
    public void Setup()
    {
        _webApplicationFactory = new TestWebApplicationFactory();
    }

    [Test(Description = "GetActiveVendorAccountAsync - Returns a valid public key")]
    [Category("PaymentGatewayController")]
    public async Task GetActiveVendorAccountAsync_should_return_vendor_account_from_vendor_id()
    {        
        var client = _webApplicationFactory.CreateClient();
        client.DefaultRequestHeaders.Add(HttpRequestHeaderNameConstants.VENDOR_ID, "433217");
        client.DefaultRequestHeaders.Add(HttpRequestHeaderNameConstants.CORRELATION_ID, Guid.NewGuid().ToString());
        var response = await client.GetAsync(ApiEndpointConstants.GET_VENDOR_ACCOUNT, CancellationToken.None);
        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Test(Description = "GetActiveVendorAccountAsync - Returns a valid public key")]
    [Category("PaymentGatewayController")]
    public async Task GetActiveVendorAccountAsync_should_return_valid_public_key()
    {        
        var client = _webApplicationFactory.CreateClient();
        client.DefaultRequestHeaders.Add(HttpRequestHeaderNameConstants.VENDOR_ID, "459111");
        client.DefaultRequestHeaders.Add(HttpRequestHeaderNameConstants.CORRELATION_ID, "3b32bbea-642f-4959-b5cb-23d8eab0376b");
        var responseMessage = await client.GetAsync(ApiEndpointConstants.GET_VENDOR_ACCOUNT, CancellationToken.None);
        var httpContent = await responseMessage.Content.ReadAsStringAsync();
        var response = JsonSerializer.Deserialize<ActiveVendorAccount>(httpContent, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        response.PublicKey.Should().NotBeEmpty();
    }

    [TearDown]
    public void TearDown()
    {
        _webApplicationFactory.Dispose();
        _webApplicationFactory = null!;
    }
}