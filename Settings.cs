namespace Summary.SMS.Gateway.Settings
{
    using Core.DisplayManagement.Entities;
    using Core.DisplayManagement.Handlers;
    using Core.DisplayManagement.Views;
    using Core.Entities;
    using Core.Environment.Shell;
    using Core.Settings;
    using Core.Workflows;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Services;
    using System.Threading.Tasks;

    public class SMSGatewaySettings
    {
    }

    public class SMSGatewaySettingsDisplayDriver : SectionDisplayDriver<ISite,
        SMSGatewaySettings>
    {
        private readonly IShellHost _host;
        private readonly ShellSettings _shell;
        private readonly IHttpContextAccessor _httpAccessor;
        private readonly IAuthorizationService _authorize;
        private readonly ISMSGatewayService _client;

        public SMSGatewaySettingsDisplayDriver(IShellHost host,
            ShellSettings settings,
            IHttpContextAccessor httpContext,
            IAuthorizationService authorize,
            ISMSGatewayService client)
        {
            _host = host;
            _shell = settings;
            _httpAccessor = httpContext;
            _authorize = authorize;
            _client = client;
        }

        public override async Task<IDisplayResult> EditAsync(SMSGatewaySettings settings,
            BuildEditorContext context)
        {
            var user = _httpAccessor.HttpContext?.User;
            if (user is null || !await _authorize.AuthorizeAsync(user, Permissions.ManageWorkflows)) return null;

            var init = Initialize<SMSGatewaySettings>("SMS_GatewaySettings_Edit", model =>
            {
                
            });

            return init.Location("Content:5").OnGroup("SMS.Gateway");
        }

        public override async Task<IDisplayResult> UpdateAsync(SMSGatewaySettings settings,
            BuildEditorContext context)
        {
            var user = _httpAccessor.HttpContext?.User;
            if (user is null || !await _authorize.AuthorizeAsync(user, Permissions.ManageWorkflows)) return null;

            if (context.GroupId == "SMS.Gateway")
            {
                await context.Updater.TryUpdateModelAsync(settings, Prefix);
                await _host.ReloadShellContextAsync(_shell);
            }

            return await EditAsync(settings, context);
        }
    }

    public class SMSGatewaySettingsConfiguration : IConfigureOptions<SMSGatewaySettings>
    {
        private readonly ISiteService _site;

        public SMSGatewaySettingsConfiguration(ISiteService site)
        {
            _site = site;
        }

        public void Configure(SMSGatewaySettings options)
        {
            var settings = _site.GetSiteSettingsAsync().GetAwaiter().GetResult().As<SMSGatewaySettings>();
        }
    }
}