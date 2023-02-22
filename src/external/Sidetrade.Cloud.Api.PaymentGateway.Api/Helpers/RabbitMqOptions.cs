namespace Sidetrade.Cloud.Api.PaymentGateway.Api.Helpers
{
    public sealed class RabbitMqOptions
    {
        public string HostName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int Port { get; set; }
        public string VirtualHost { get; set; } = string.Empty;
        public bool AutomaticRecoveryEnabled { get; set; }
        public string DeliveryMode { get; set; } = string.Empty;
        public int RequestedHeartbeat { get; set; }
    }
}
