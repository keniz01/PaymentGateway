namespace PaymentGateway.Application.Features.VendorAccountFeature.Queries;

/// <summary>
/// Request to get a vendor payment account by vendor id.
/// </summary>
public class GetVendorAccountQueryResult
{
    public GetVendorAccountQueryResult() { }

    private GetVendorAccountQueryResult(int memberId,
        int? metaMemberId, string apiSecretKey, string apiPublicKey, bool isActivated)
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

    public static GetVendorAccountQueryResult Create(int memberId,
        int metaMemberId, string apiSecretKey, string apiPublicKey, bool isActivated)
    {
        if (memberId < 1)
        {
            return new UnknownVendorAccount();
        }

        return new GetVendorAccountQueryResult(memberId, metaMemberId, apiSecretKey, apiPublicKey, isActivated);
    }

    public static UnknownVendorAccount Unknown()
    {
        return new UnknownVendorAccount();
    }

    public override string ToString() => $"{MemberId} | {MetaMemberId} | {ApiSecretKey} | {ApiPublicKey} | {IsActivated}";

    public class UnknownVendorAccount : GetVendorAccountQueryResult
    {
    }
}