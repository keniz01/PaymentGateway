using FluentAssertions;
using System.Text;
using System.Text.Json;
using System.Net.Mime;
using Bogus;
using System.Net;
using PaymentGateway.Presentation.Constants;

namespace PaymentGateway.Tests;

[TestFixture]
public class VendorAccountControllerTests
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
    [Category("VendorAccountControllerTests")]
    public async Task CreateVendorAccountAsync_When_create_new_vendor_account_expect__success_status_code()
    {
        var response = await CreateVendorAccountAsync();
        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Test(Description = "When request for vendor account, respond with successful status code.")]
    [Category("VendorAccountControllerTests")]
    public async Task GetVendorAccountAsync_When_request_for_vendor_account_expect_success_status_code()
    {
        await CreateVendorAccountAsync();
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
        var response = await _client.GetAsync(EndpointUriConstants.GET_VENDOR_ACCOUNT, CancellationToken.None);
        return response;
    }

    private async Task<HttpResponseMessage> CreateVendorAccountAsync()
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
            EndpointUriConstants.CREATE_VENDOR_ACCOUNT,
            content,
            CancellationToken.None);

        return response;
    }
}