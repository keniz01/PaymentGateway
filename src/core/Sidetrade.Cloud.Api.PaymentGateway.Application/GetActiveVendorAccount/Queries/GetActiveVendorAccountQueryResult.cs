using Sidetrade.Cloud.Api.PaymentGateway.Application.Shared;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application;

/// <summary>
/// Request to get a vendor payment account by vendor id.
/// </summary>
public class GetActiveVendorAccountQueryResult: ResultBase
{
    public GetActiveVendorAccountQueryResult() : base(Guid.Empty) { }

    private GetActiveVendorAccountQueryResult(Guid correlationId) : base(correlationId) { }

    private GetActiveVendorAccountQueryResult(Guid correlationId, int vendorId,
        int? metaVendorId, string secretKey, string publicKey, bool isActivated)
        : base(correlationId)
    {
        VendorId = vendorId;
        MetaVendorId = metaVendorId;
        SecretKey = secretKey;
        PublicKey = publicKey;
        IsActivated = isActivated;
    }

    public int VendorId { get; protected set; }
    public int? MetaVendorId { get; protected set; }
    public string SecretKey { get; protected set; } = string.Empty;
    public string PublicKey { get; protected set; } = string.Empty;
    public bool IsActivated { get; protected set; }

    public static GetActiveVendorAccountQueryResult Create(Guid correlationId,
        int vendorId, int? metaVendorId, string secretKey, string publicKey, bool isActivated)
    {
        if(vendorId < 1)
        {
            return new GetActiveVendorAccountQueryResultUnknown(correlationId);
        }

        return new GetActiveVendorAccountQueryResult(correlationId, vendorId, metaVendorId, secretKey, publicKey, isActivated);
    }

    public static GetActiveVendorAccountQueryResult Unknown(Guid correlationId)
    {
        throw new NotImplementedException();
    }

    public override string ToString() => $"{VendorId} | {MetaVendorId} | {SecretKey} | {PublicKey} | {IsActivated}";

    public class GetActiveVendorAccountQueryResultUnknown : GetActiveVendorAccountQueryResult
    {
        public GetActiveVendorAccountQueryResultUnknown(Guid correlationId) : base(correlationId)
        {
        }
    }
}