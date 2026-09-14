namespace Summary.SMS.Gateway.Services
{
    using Microsoft.Extensions.Options;
    using Summary.SMS.Gateway.Settings;

    public interface ISMSGatewayService
    {
        
    }

    public class SMSGatewayService : ISMSGatewayService
    {
        private readonly SMSGatewaySettings _options;

        public SMSGatewayService(IOptions<SMSGatewaySettings> options)
        {
            _options = options.Value;
        }
    }
}