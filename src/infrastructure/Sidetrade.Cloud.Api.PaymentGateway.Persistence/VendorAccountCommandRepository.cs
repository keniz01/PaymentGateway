using Microsoft.Extensions.Logging;
using Sidetrade.Cloud.Api.PaymentGateway.Application;
using Sidetrade.Cloud.Api.PaymentGateway.Application.VendorAccount.Commands.Create;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Npgsql;
using MapsterMapper;

namespace Sidetrade.Cloud.Api.PaymentGateway.Persistence;

public class VendorAccountCommandRepository : IVendorAccountCommandRepository
{
    private readonly ILogger<VendorAccountCommandRepository> _logger;
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;
    public VendorAccountCommandRepository(
        ApplicationDbContext context, 
        ILogger<VendorAccountCommandRepository> logger,
        IMapper mapper)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> CreateVendorAccountAsync(CreateVendorAccountCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("********* {LogDate}: Request for {CorrelationId}", DateTime.UtcNow, command.CorrelationId);
        
        var sqlQuery = new StringBuilder("INSERT vendor_id ")
            .AppendLine("(vendor_id,meta_member_id,secret_key,public_key,is_activated,created_date,updated_date) ")
            .AppendLine("VALUES (@vendor_id,@meta_member_id,@secret_key,@public_key,@is_activated,@created_date,@updated_date)")
            .ToString();

        var parameters = new List<NpgsqlParameter>
        {
            new NpgsqlParameter("@vendor_id", command.VendorId),
            new NpgsqlParameter("@meta_member_id", command.MetaVendorId),
            new NpgsqlParameter("@secret_key", command.SecretKey),
            new NpgsqlParameter("@public_key", command.PublicKey),
            new NpgsqlParameter("@is_activated", command.IsActivated),
            new NpgsqlParameter("@created_date", DateTime.UtcNow),
            new NpgsqlParameter("@updated_date", DateTime.UtcNow)
        };

        await _context.Database.ExecuteSqlRawAsync(sqlQuery, parameters, cancellationToken);
        var affectedRows = await _context.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("********* {LogDate}: Response for {CorrelationId}", DateTime.UtcNow, command.CorrelationId);
        return affectedRows == 1;
    }
}
