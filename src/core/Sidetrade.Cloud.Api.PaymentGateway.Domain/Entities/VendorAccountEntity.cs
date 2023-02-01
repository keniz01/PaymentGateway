namespace Sidetrade.Cloud.Api.PaymentGateway.Domain.Entities;

public class VendorAccountEntity
{
    private VendorAccountEntity(int memberId, int metaMemberId,
        string apiPublicKey, string apiSecretKey, bool isActivated)
    {
    }

    public int MemberId { get; private set; }
    public int MetaMemberId { get; private set; }
    public string ApiPublicKey { get; private set; } = string.Empty;
    public string ApiSecretKey { get; private set; } = string.Empty;
    public bool IsActivated { get; private set; }

    public static VendorAccountEntity Create(int memberId, 
        int metaMemberId, string apiPublicKey, string apiSecretKey, bool isActivated)
         => new VendorAccountEntity(memberId, metaMemberId, apiPublicKey, apiSecretKey, isActivated);        
}