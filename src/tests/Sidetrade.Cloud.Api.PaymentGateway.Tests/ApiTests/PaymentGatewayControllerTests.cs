using FluentAssertions;
using System.Text;
using System.Text.Json;
using System.Net.Mime;
using Bogus;
using System.Net;
using Sidetrade.Cloud.Api.PaymentGateway.Presentation.Features.VendorAccountFeature;

namespace Sidetrade.Cloud.Api.PaymentGateway.Tests.ApiTests;

[TestFixture]
public class PaymentGatewayControllerTests
{
    private HttpClient _client = null!;

    [SetUp]
    public void Setup()
    {
        var webApplicationFactory = new TestWebApplicationFactory();
        _client = webApplicationFactory.CreateClient();
        _client.DefaultRequestHeaders.Add(HttpRequestHeaderNameConstants.META_MEMBER_ID, "448058");
        _client.DefaultRequestHeaders.Add(HttpRequestHeaderNameConstants.MEMBER_ID, "448058");
        _client.DefaultRequestHeaders.Add(HttpRequestHeaderNameConstants.CORRELATION_ID, Guid.NewGuid().ToString());
    }

    [Test(Description = "CreateVendorAccountAsync - Creates a new vendor account.")]
    [Category("PaymentGatewayController")]
    public async Task PaymentGatewayController_CreateVendorAccountAsync_Should_create_new_vendor_account()
    {
        var response = await CreateVendorAccountAsync();
        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Test(Description = "GetVendorAccountAsync - Returns a valid public key")]
    [Category("PaymentGatewayController")]
    public async Task GetVendorAccountAsync_should_return_true_status_success_code()
    {
        await CreateVendorAccountAsync();
        var responseMessage = await GetVendorAccountResponseMesssageAsync();
        responseMessage.EnsureSuccessStatusCode();
        responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Test(Description = "GetVendorAccountAsync - Returns a valid public key")]
    [Category("PaymentGatewayController")]
    public async Task GetVendorAccountAsync_should_return_valid_public_key()
    {
        await CreateVendorAccountAsync();
        var responseMessage = await GetVendorAccountResponseMesssageAsync();
        responseMessage.EnsureSuccessStatusCode();

        var jsonContent = await responseMessage.Content.ReadAsStringAsync();
        var vendorAccount = JsonSerializer.Deserialize<GetVendorAccountResponse>(jsonContent,
        new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? new GetVendorAccountResponse(string.Empty);

        vendorAccount.ApiPublicKey.Should().NotBeEmpty();
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

    private async Task<HttpResponseMessage> CreateVendorAccountAsync()
    {
        var faker = new Faker();
        var payLoad = new
        {
            PublicKey = $"pk_test_{faker.Random.AlphaNumeric(25)}",
            SecretKey = $"sk_test_{faker.Random.AlphaNumeric(25)}",
            IsActivated = true
        };
        var json = JsonSerializer.Serialize<dynamic>(payLoad);
        var content = new StringContent(json, Encoding.Default, MediaTypeNames.Application.Json);

        var response = await _client.PostAsync(
            PaymentGatewayControllerApiEndpointConstants.CREATE_VENDOR_ACCOUNT,
            content,
            CancellationToken.None);
        return response;
    }
}