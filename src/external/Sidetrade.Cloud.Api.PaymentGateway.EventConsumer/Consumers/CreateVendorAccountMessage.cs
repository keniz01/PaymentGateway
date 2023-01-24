namespace Sidetrade.Cloud.Api.PaymentGateway.EventConsumer.Consumers
{
    public interface CreateVendorAccountMessage
    {
        public int MemberId { get; set; }
        public int MetaMemberId { get; set; }
        public string ApiPublicKey { get; set; }
        public string ApiSecretKey { get; set; }
        public bool IsActivated { get; set; }
    }
}
