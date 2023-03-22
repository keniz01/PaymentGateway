using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PaymentGateway.Application.Abstractions.Correlation;

namespace PaymentGateway.Persistence;

public class ApplicationDbContext: DbContext
{
    private readonly ILogger<DbContextOptions<ApplicationDbContext>> _logger;
    private readonly ICorrelationIdHelper _correlationIdHelper;

    public ApplicationDbContext(
        ICorrelationIdHelper correlationIdHelper,
        DbContextOptions<ApplicationDbContext> options,
        ILogger<DbContextOptions<ApplicationDbContext>> logger) : base(options)
    {
        _logger = logger;
        _correlationIdHelper = correlationIdHelper;
        SavedChanges += ApplicationDbContext_SavedChanges!;
        SaveChangesFailed += ApplicationDbContext_SaveChangesFailed!;
    } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbVendorAccount>(options => 
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

    private void ApplicationDbContext_SavedChanges(object sender, SavedChangesEventArgs e)
        => _logger.LogInformation("********* Request Id: {CorrelationId} saved at {TimeElapsed} ms. *********", _correlationIdHelper.Get(), DateTimeOffset.UtcNow);

    private void ApplicationDbContext_SaveChangesFailed(object sender, SaveChangesFailedEventArgs e)
        => _logger.LogInformation("********* Request Id: {CorrelationId} save failed at {TimeElapsed} ms. *********", _correlationIdHelper.Get(), DateTimeOffset.UtcNow);
}