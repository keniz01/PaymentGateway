using Sidetrade.Cloud.Api.PaymentGateway.Application;
using Bogus;
using Microsoft.AspNetCore.DataProtection;
using AutoBogus;

namespace Sidetrade.Cloud.Api.PaymentGateway.Tests;

public class BogusDataGenerator
    {
        public static IList<GetActiveVendorAccountQueryResult> Generate()
        {
            var provider = DataProtectionProvider
                .Create(typeof(PaymentGatewayControllerTests).Assembly.GetName().FullName)
                .CreateProtector("PAYMENT_GATEWAY_SECRET_KEY");

            Randomizer.Seed = new Random(1);
        var faker = new Faker<GetActiveVendorAccountQueryResult>()
            .CustomInstantiator(faker => 
            {
                return GetActiveVendorAccountQueryResult.Create
                (
                    correlationId: faker.Random.Guid(),
                    vendorId: faker.Random.Number(300000, 699999),
                    metaVendorId: faker.Random.Number(300000, 699999),
                    secretKey: provider.Protect($"sk_test_{faker.Random.AlphaNumeric(25)}"),
                    publicKey: $"pk_test_{faker.Random.AlphaNumeric(25)}",
                    isActivated: faker.Random.Bool(1)
                );
            });

            return faker.GenerateBetween(10, 10);
        }
    }
