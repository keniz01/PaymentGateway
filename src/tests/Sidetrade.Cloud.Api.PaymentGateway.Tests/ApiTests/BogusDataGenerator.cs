
using Bogus;
using Microsoft.AspNetCore.DataProtection;
using Sidetrade.Cloud.Api.PaymentGateway.Persistence;

namespace Sidetrade.Cloud.Api.PaymentGateway.Tests.ApiTests;

public static class BogusDataGenerator
{
    public static IList<VendorAccountDataModel> Generate()
    {
        var provider = DataProtectionProvider
            .Create(typeof(PaymentGatewayControllerTests).Assembly.GetName().FullName)
            .CreateProtector("PAYMENT_GATEWAY_SECRET_KEY");

        Randomizer.Seed = new Random(1);
        var faker = new Faker<VendorAccountDataModel>()
            .Rules((faker, account) =>
            {
                account.MemberId = faker.Random.Number(300000, 699999);
                account.MetaMemberId = faker.Random.Number(300000, 699999);
                account.ApiSecretKey = provider.Protect($"sk_test_{faker.Random.AlphaNumeric(25)}");
                account.ApiPublicKey = $"pk_test_{faker.Random.AlphaNumeric(25)}";
                account.IsActivated = faker.Random.Bool(1);
            });

        return faker.GenerateBetween(10, 10);
    }
}
