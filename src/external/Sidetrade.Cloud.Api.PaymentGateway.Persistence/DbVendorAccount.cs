using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sidetrade.Cloud.Api.PaymentGateway.Persistence;

public sealed class DbVendorAccount
{
    public int MemberId { get; set; }
    public int MetaMemberId { get; set; }
    public string ApiSecretKey { get; set; } = string.Empty;
    public string ApiPublicKey { get; set; } = string.Empty;
    public bool IsActivated { get; set; }
    public DateTimeOffset DateCreated { get; set; }    
    public DateTimeOffset DateUpdated { get; set; }
} 