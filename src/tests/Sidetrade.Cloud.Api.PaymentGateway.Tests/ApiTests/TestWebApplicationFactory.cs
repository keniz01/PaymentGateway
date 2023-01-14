using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Sidetrade.Cloud.Api.PaymentGateway.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Diagnostics;
using Microsoft.Data.Sqlite;
using System.Text;

namespace Sidetrade.Cloud.Api.PaymentGateway.Tests;

public class TestWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly SqliteConnection _connection;
        public TestWebApplicationFactory()
        {
            _connection = new SqliteConnection("datasource=:memory:");
            _connection.Open();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.ConfigureServices(services =>
            {
                services.AddDataProtection();
                var contextDescriptor = services.SingleOrDefault(descriptor => 
                    descriptor.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                services.Remove(contextDescriptor!);

                var connectionDescriptor = services.SingleOrDefault(descriptor => 
                    descriptor.ServiceType == typeof(DbConnection));
                    
                services.Remove(connectionDescriptor!);
                
                // services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase(_dbName));

                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlite(_connection)
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging()
                    .LogTo(message => Debug.WriteLine(message))
                    .Options;

                var context = new ApplicationDbContext(options);
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var sqlQuery = new StringBuilder("INSERT INTO vendor_account ")
                    .AppendLine("(vendor_id,meta_member_id,secret_key,public_key,is_activated,date_created,date_updated) ")
                    .AppendLine("VALUES (@vendor_id,@meta_member_id,@secret_key,@public_key,@is_activated,@date_created,@date_updated);")
                    .ToString();
                
                var fakeData = BogusDataGenerator.Generate();
                foreach(var command in fakeData)
                {
                    var parameters = new List<SqliteParameter>
                    {
                        new SqliteParameter("@vendor_id", command.VendorId),
                        new SqliteParameter("@meta_member_id", command.MetaVendorId),
                        new SqliteParameter("@secret_key", command.SecretKey),
                        new SqliteParameter("@public_key", command.PublicKey),
                        new SqliteParameter("@is_activated", command.IsActivated),
                        new SqliteParameter("@date_created", DateTime.UtcNow),
                        new SqliteParameter("@date_updated", DateTime.UtcNow)
                    };

                    context.Database.ExecuteSqlRaw(sqlQuery, parameters);
                    context.SaveChanges();
                }
                _connection.Close();
            });

            builder.UseEnvironment("Development");
        }
    }