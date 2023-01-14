using FluentAssertions;
using Sidetrade.Cloud.Api.PaymentGateway.Api.PaymentAccounts;
using System.Text;
using System.Text.Json;
using System.Net.Mime;
using Bogus;

namespace Sidetrade.Cloud.Api.PaymentGateway.Tests;

[TestFixture]
public class PaymentGatewayControllerTests
{
    private TestWebApplicationFactory _webApplicationFactory = null!;
    private Faker _faker = null!;

    [SetUp]
    public void Setup()
    {
        _webApplicationFactory = new TestWebApplicationFactory();
        _faker = new Faker();
    }

    [Test(Description = "CreateVendorAccountAsync - Creates a new vendor account.")]
    [Category("PaymentGatewayController")]
    public async Task CreateVendorAccountAsync_Should_create_new_vendor_account()
    {        
        var client = _webApplicationFactory.CreateClient();
        client.DefaultRequestHeaders.Add(HttpRequestHeaderNameConstants.META_VENDOR_ID, "448058");
        client.DefaultRequestHeaders.Add(HttpRequestHeaderNameConstants.VENDOR_ID, "448058");
        client.DefaultRequestHeaders.Add(HttpRequestHeaderNameConstants.CORRELATION_ID, Guid.NewGuid().ToString());
        
        var payLoad = new 
        {
            PublicKey = $"pk_test_{_faker.Random.AlphaNumeric(25)}",
            SecretKey = $"sk_test_{_faker.Random.AlphaNumeric(25)}",
            IsActivated = true
        };
        var json = JsonSerializer.Serialize<dynamic>(payLoad);
        var content = new StringContent(json, Encoding.Default, MediaTypeNames.Application.Json);
        
        var response = await client.PostAsync(
            PaymentGatewayControllerApiEndpointConstants.CREATE_VENDOR_ACCOUNT,
            content,
            CancellationToken.None);
        response.IsSuccessStatusCode.Should().BeTrue();
    }

    // [Test(Description = "GetActiveVendorAccountAsync - Returns a valid public key")]
    // [Category("PaymentGatewayController")]
    // public async Task GetActiveVendorAccountAsync_should_return_true_status_success_code()
    // {        
    //     var client = _webApplicationFactory.CreateClient();
    //     client.DefaultRequestHeaders.Add(HttpRequestHeaderNameConstants.VENDOR_ID, "448058");
    //     client.DefaultRequestHeaders.Add(HttpRequestHeaderNameConstants.CORRELATION_ID, Guid.NewGuid().ToString());
    //     var response = await client.GetAsync(PaymentGatewayControllerApiEndpointConstants.GET_VENDOR_ACCOUNT, CancellationToken.None);
    //     response.IsSuccessStatusCode.Should().BeTrue();
    // }

    // [Test(Description = "GetActiveVendorAccountAsync - Returns a valid public key")]
    // [Category("PaymentGatewayController")]
    // public async Task GetActiveVendorAccountAsync_should_return_valid_public_key()
    // {        
    //     var client = _webApplicationFactory.CreateClient();
    //     client.DefaultRequestHeaders.Add(HttpRequestHeaderNameConstants.VENDOR_ID, "378406");
    //     client.DefaultRequestHeaders.Add(HttpRequestHeaderNameConstants.CORRELATION_ID, "3b32bbea-642f-4959-b5cb-23d8eab0376b");
    //     var responseMessage = await client.GetAsync(PaymentGatewayControllerApiEndpointConstants.GET_VENDOR_ACCOUNT, CancellationToken.None);
    //     var httpContent = await responseMessage.Content.ReadAsStringAsync();
    //     var response = JsonSerializer.Deserialize<ActiveVendorAccount>(httpContent, 
    //         new JsonSerializerOptions
    //         {
    //             PropertyNameCaseInsensitive = true
    //         })!;
    //     response.PublicKey.Should().NotBeEmpty();
    // }

    [TearDown]
    public void TearDown()
    {
        _webApplicationFactory.Dispose();
        _webApplicationFactory = null!;
    }
}