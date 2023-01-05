using Microsoft.EntityFrameworkCore;
using Sidetrade.Cloud.Api.PaymentGateway.Application;

namespace Sidetrade.Cloud.Api.PaymentGateway.Persistence;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GetActiveVendorAccountResponse>(options => {
            options.ToTable("vendor_account");
            options.HasKey(prop => prop.VendorId);
            options.Property(prop => prop.VendorId).HasColumnName("vendor_id");
            options.Property(prop => prop.MetaVendorId).HasColumnName("meta_member_id");
            options.Property(prop => prop.SecretKey).HasColumnName("secret_key");
            options.Property(prop => prop.PublicKey).HasColumnName("public_key");
            options.Property(prop => prop.IsActivated).HasColumnName("is_activated");
        });

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<GetActiveVendorAccountResponse> ActiveVendorAccounts => Set<GetActiveVendorAccountResponse>();
}