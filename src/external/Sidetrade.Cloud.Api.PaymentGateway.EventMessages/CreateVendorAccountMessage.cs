namespace Sidetrade.Cloud.Api.PaymentGateway.EventMessages
{
    public record CreateVendorAccountMessage
    {
        public int MemberId { get; init; }

        public int MetaMemberId { get; init; }

        public string ApiSecretKey { get; init; } = string.Empty;

        public string ApiPublicKey { get; init; } = string.Empty;

        public bool IsActivated { get; init; }
    }
}