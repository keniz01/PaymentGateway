namespace Sidetrade.Cloud.Api.PaymentGateway.Domain.Entities;

public class VendorAccountEntity
{
    private VendorAccountEntity(int memberId, int metaMemberId, string apiPublicKey, 
        string apiSecretKey, bool isActivated, DateTimeOffset dateCreated, DateTimeOffset dateUpdated)
        :this(memberId, metaMemberId, apiPublicKey, apiSecretKey, isActivated, dateUpdated)
    {
        DateCreated = dateCreated;
    }

    private VendorAccountEntity(int memberId, int metaMemberId, string apiPublicKey, 
        string apiSecretKey, bool isActivated, DateTimeOffset dateUpdated)
    {
        ApiPublicKey = apiPublicKey;
        ApiSecretKey = apiSecretKey;
        IsActivated = isActivated;
        MemberId = memberId;
        MetaMemberId = metaMemberId;
        DateUpdated = dateUpdated;
    }

    public int MemberId { get; private set; }
    public int MetaMemberId { get; private set; }
    public string ApiPublicKey { get; private set; } = string.Empty;
    public string ApiSecretKey { get; private set; } = string.Empty;
    public bool IsActivated { get; private set; }
    public DateTimeOffset DateCreated { get; private set; }
    public DateTimeOffset DateUpdated { get; private set; }

    public static VendorAccountEntity Create(int memberId, 
        int metaMemberId, string apiPublicKey, string apiSecretKey, bool isActivated)
         => new VendorAccountEntity(
            memberId, 
            metaMemberId, 
            apiPublicKey, 
            apiSecretKey, 
            isActivated, 
            DateTimeOffset.UtcNow, 
            DateTimeOffset.UtcNow);

    public static VendorAccountEntity Update(int memberId, 
        int metaMemberId, string apiPublicKey, string apiSecretKey, bool isActivated)
         => new VendorAccountEntity(
            memberId, 
            metaMemberId,
            apiPublicKey, 
            apiSecretKey, 
            isActivated, 
            DateTimeOffset.UtcNow);         
}