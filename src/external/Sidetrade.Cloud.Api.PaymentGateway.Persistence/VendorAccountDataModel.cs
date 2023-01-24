using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sidetrade.Cloud.Api.PaymentGateway.Persistence;

[Table("vendor_account")]
public sealed class VendorAccountDataModel
{
    [Key]
    [Column("member_id")]
    public int MemberId { get; set; }

    [Column("meta_member_id")]
    public int MetaMemberId { get; set; }

    [Column("api_secret_key")]
    public string ApiSecretKey { get; set; } = string.Empty;

    [Column("api_public_key")]
    public string ApiPublicKey { get; set; } = string.Empty;

    [Column("is_activated")]
    public bool IsActivated { get; set; }

    [Column("date_created")]
    public bool DateCreated { get; set; }    

    [Column("date_updated")]
    public bool DateUpdated { get; set; }
} 