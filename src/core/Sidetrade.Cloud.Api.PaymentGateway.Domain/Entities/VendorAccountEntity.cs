namespace Sidetrade.Cloud.Api.PaymentGateway.Domain.Entities;

public class VendorAccountEntity
{
    private VendorAccountEntity(int memberId,
        int metaMemberId, string apiSecretKey, string apiPublicKey, bool isActivated)
    {
        MetaMemberId = metaMemberId;
        ApiSecretKey = apiSecretKey;
        ApiPublicKey = apiPublicKey;
        IsActivated = isActivated;
        MemberId = memberId;
    }

    public int MemberId { get; private set; }
    public int MetaMemberId { get; private set; }
    public string ApiPublicKey { get; private set; }
    public string ApiSecretKey { get; private set; }
    public bool IsActivated { get; private set; }

    public static VendorAccountEntity Create(int memberId,
        int metaMemberId, string apiSecretKey, string apiPublicKey, bool isActivated)
         => new(memberId, metaMemberId, apiSecretKey, apiPublicKey, isActivated);
}