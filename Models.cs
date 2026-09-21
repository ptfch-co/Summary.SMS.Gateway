namespace Summary.SMS.Gateway
{
    using System;

    public class SmsInboundWebhookReceiver
    {
        public string DeviceId { get; set; }
        public string Event { get; set; }
        public string Id { get; set; }
        public SmsInboundWebhookPayloadReceiver Payload { get; set; }
        public string WebhookId { get; set; }
    }

    public class SmsInboundWebhookPayloadReceiver
    {
        public string Message { get; set; }
        public DateTime ReceivedAt { get; set; }
        public string MessageId { get; set; }
        public string PhoneNumber { get; set; }
        public string Sender { get; set; }
        public int SimNumber { get; set; }
    }
}