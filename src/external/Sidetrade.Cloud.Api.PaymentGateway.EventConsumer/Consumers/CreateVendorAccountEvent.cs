namespace Sidetrade.Cloud.Api.PaymentGateway.EventConsumer.Consumers
{
    public record CreateVendorAccountEvent(int MemberId, 
        int MetaMemberId, string ApiPublicKey, string ApiSecretKey, bool IsActivated);
}
