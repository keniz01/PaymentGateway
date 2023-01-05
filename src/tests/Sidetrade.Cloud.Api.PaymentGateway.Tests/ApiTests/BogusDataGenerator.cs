using Sidetrade.Cloud.Api.PaymentGateway.Application;
using Bogus;
using Microsoft.AspNetCore.DataProtection;

namespace Sidetrade.Cloud.Api.PaymentGateway.Tests;

public class BogusDataGenerator
    {
        public static IList<GetActiveVendorAccountResponse> Generate()
        {
            var provider = DataProtectionProvider
                .Create(typeof(PaymentGatewayControllerTests).Assembly.GetName().FullName)
                .CreateProtector("PAYMENT_GATEWAY_SECRET_KEY");

            Randomizer.Seed = new Random(123123);    
            var faker = new Faker<GetActiveVendorAccountResponse>();

            faker.Rules((faker, user) =>
            {
                user.VendorId = faker.Random.Number(300000, 699999);
                user.MetaVendorId = faker.Random.Number(300000, 699999);
                user.SecretKey = provider.Protect($"sk_test_{faker.Random.AlphaNumeric(25)}");
                user.PublicKey = $"pk_test_{faker.Random.AlphaNumeric(25)}";
                user.IsActivated = faker.Random.Bool(1);
            });

            return faker.GenerateBetween(10, 10);
        }
    }
