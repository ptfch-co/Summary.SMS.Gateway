namespace Summary.SMS.Gateway
{
    using Microsoft.Extensions.Localization;
    using Core.Navigation;
    using System;
    using System.Threading.Tasks;

    public class Menu : INavigationProvider
    {
        private readonly IStringLocalizer<Menu> _localizer;

        public Menu(IStringLocalizer<Menu> localizer)
        {
            _localizer = localizer;
        }

        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (!String.Equals(name, "admin", StringComparison.OrdinalIgnoreCase)) return Task.CompletedTask;

            builder.Add(_localizer["Configuration"], configuration =>
            {
                configuration.Add(_localizer["Settings"], settings =>
                {
                    settings.Add(_localizer["درگاه سیم کارت"], _localizer["درگاه سیم کارت"], itemBuilder =>
                    {
                        itemBuilder.Action("Index", "Admin", new { area = "Core.Settings", groupId = "SMS.Gateway" }).LocalNav();
                    });
                });
            });

            return Task.CompletedTask;
        }
    }
}