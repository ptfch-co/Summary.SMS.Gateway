namespace Summary.SMS.Gateway.Services
{
    using Core.Mvc.Utilities;
    using Core.Workflows;
    using Microsoft.Extensions.Options;
    using Summary.SMS.Gateway.Settings;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ISMSGatewayService
    {
        Task SendSmsAsync(string message,
            string phone_number,
            int sim_slot);
    }

    public class SMSGatewayService : ISMSGatewayService
    {
        private readonly SMSGatewaySettings _options;
        private readonly HttpRequestClient _client;

        public SMSGatewayService(IOptions<SMSGatewaySettings> options,
            HttpRequestClient client)
        {
            _options = options.Value;
            _client = client;
        }

        public async Task SendSmsAsync(string message,
            string phone_number,
            int sim_slot)
        {
            message = message.ConvertHtmlToSmsFormat();

            var data = new
            {
                DeviceId = _options.Device_Id,
                Id = Guid.NewGuid().ToString(),
                IsEncrypted = false,
                Message = message,
                PhoneNumbers = new [] { phone_number.RemoveMobilePrefixNo("+98") },
                Priority = 0,
                SimNumber = sim_slot,
                TTL = 86400,
                WithDeliveryReport = true
            };

            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_options.Username}:{_options.Password}"));

            await _client.SendPostRequestAsync<object>(
                "https://sms.gateway.powerautomate.ir/api/3rdparty/v1/messages",
                data,
                new KeyValuePair<string, string>("Authorization", $"Basic {auth}"),
                "application/json"
            );
        }
    }
}