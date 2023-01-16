using Sidetrade.Cloud.Api.PaymentGateway.Application.Shared;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application.VendorAccount;

/// <summary>
/// Request to get a vendor payment account by vendor id.
/// </summary>
public class GetActiveVendorAccountQueryResult: ResultBase
{
    public GetActiveVendorAccountQueryResult() : base(Guid.Empty) { }

    private GetActiveVendorAccountQueryResult(Guid correlationId) : base(correlationId) { }

    private GetActiveVendorAccountQueryResult(Guid correlationId, int memberId,
        int? metaMemberId, string apiSecretKey, string apiPublicKey, bool isActivated)
        : base(correlationId)
    {
        MemberId = memberId;
        MetaMemberId = metaMemberId;
        ApiSecretKey = apiSecretKey;
        ApiPublicKey = apiPublicKey;
        IsActivated = isActivated;
    }

    public int MemberId { get; protected set; }
    public int? MetaMemberId { get; protected set; }
    public string ApiSecretKey { get; protected set; } = string.Empty;
    public string ApiPublicKey { get; protected set; } = string.Empty;
    public bool IsActivated { get; protected set; }

    public static GetActiveVendorAccountQueryResult Create(Guid correlationId,
        int memberId, int? metaMemberId, string apiSecretKey, string apiPublicKey, bool isActivated)
    {
        if(memberId < 1)
        {
            return new UnknowVendorAccount(correlationId);
        }

        return new GetActiveVendorAccountQueryResult(correlationId, memberId, metaMemberId, apiSecretKey, apiPublicKey, isActivated);
    }

    public static UnknowVendorAccount Unknown(Guid correlationId)
    {
        return new UnknowVendorAccount(correlationId);
    }

    public override string ToString() => $"{MemberId} | {MetaMemberId} | {ApiSecretKey} | {ApiPublicKey} | {IsActivated}";

    public class UnknowVendorAccount : GetActiveVendorAccountQueryResult
    {
        public UnknowVendorAccount(Guid correlationId) : base(correlationId)
        {
        }
    }
}