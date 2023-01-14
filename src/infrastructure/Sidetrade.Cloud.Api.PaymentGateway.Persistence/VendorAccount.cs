using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sidetrade.Cloud.Api.PaymentGateway.Persistence;

[Table("vendor_account")]
public sealed class VendorAccount
{
    [Key]
    [Column("vendor_id")]
    public int VendorId { get; set; }

    [Column("meta_member_id")]
    public int? MetaVendorId { get; set; }

    [Column("secret_key")]
    public string SecretKey { get; set; } = string.Empty;

    [Column("public_key")]
    public string PublicKey { get; set; } = string.Empty;

    [Column("is_activated")]
    public bool IsActivated { get; set; }

    [Column("date_created")]
    public bool DateCreated { get; set; }    

    [Column("date_updated")]
    public bool DateUpdated { get; set; }
} 