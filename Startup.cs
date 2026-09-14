namespace Summary.SMS.Gateway
{
    using Core.DisplayManagement.Handlers;
    using Core.Modules;
    using Core.Navigation;
    using Core.Settings;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Summary.SMS.Gateway.Settings;
    using Summary.SMS.Gateway.Services;

    [Feature(SMSGateway.Features.SMSGateway)]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<INavigationProvider, Menu>();
            services.AddScoped<IDisplayDriver<ISite>, SMSGatewaySettingsDisplayDriver>();

            services.AddSingleton<ISMSGatewayService, SMSGatewayService>();

            services.AddTransient<IConfigureOptions<SMSGatewaySettings>, SMSGatewaySettingsConfiguration>();
        }
    }
}