using Microsoft.EntityFrameworkCore;

namespace Sidetrade.Cloud.Api.PaymentGateway.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VendorAccountDataModel>(options =>
        {
            options.ToTable("vendor_account").HasKey(prop => prop.MemberId);
            options.Property(prop => prop.MemberId).HasColumnName("member_id").ValueGeneratedNever().IsRequired();
            options.Property(prop => prop.MetaMemberId).HasColumnName("meta_member_id").IsRequired();
            options.Property(prop => prop.ApiPublicKey).HasColumnName("api_public_key").IsRequired();
            options.Property(prop => prop.ApiSecretKey).HasColumnName("api_secret_key").IsRequired();
            options.Property(prop => prop.IsActivated).HasColumnName("is_activated").IsRequired();
            options.Property(prop => prop.DateCreated).HasColumnName("date_created").IsRequired();
            options.Property(prop => prop.DateUpdated).HasColumnName("date_updated").IsRequired();
        });
    }

    public DbSet<VendorAccountDataModel> VendorAccounts => Set<VendorAccountDataModel>();
}