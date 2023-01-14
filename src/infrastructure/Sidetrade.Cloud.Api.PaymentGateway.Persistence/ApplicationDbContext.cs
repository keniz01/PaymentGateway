using Microsoft.EntityFrameworkCore;

namespace Sidetrade.Cloud.Api.PaymentGateway.Persistence;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}