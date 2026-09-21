namespace Summary.SMS.Gateway
{
    using Core.DisplayManagement.Handlers;
    using Core.Modules;
    using Core.Navigation;
    using Core.Settings;
    using Core.Workflows.Helpers;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Summary.SMS.Gateway.Settings;
    using Summary.SMS.Gateway.Services;
    using Summary.SMS.Gateway.Workflows.Task.SMS.Send;
    using Summary.SMS.Gateway.Workflows.Event.SMS.Receive;

    [Feature(SMSGateway.Features.SMSGateway)]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDisplayDriver<ISite>, SMSGatewaySettingsDisplayDriver>();
            services.AddScoped<INavigationProvider, Menu>();
            services.AddScoped<ISMSGatewayService, SMSGatewayService>();

            services.AddActivity<SendSmsBySMSGatewayTask, SendSmsBySMSGatewayDisplay>();
            services.AddActivity<ReceiveMessageInSmsGatewayEvent, ReceiveMessageInSmsGatewayDisplay>();

            services.AddTransient<IConfigureOptions<SMSGatewaySettings>, SMSGatewaySettingsConfiguration>();
        }
    }
}