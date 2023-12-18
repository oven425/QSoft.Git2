using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data;
using System.Security.Authentication.ExtendedProtection;
using System.Windows;

namespace GitViwer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        IHost m_Host;
        protected override async void OnStartup(StartupEventArgs e)
        {
            var builder = Host.CreateApplicationBuilder();
            this.ConfigService(builder.Services);
            //builder.Services.AddTransient<BlobPage>()
            //    .AddTransient<BlobViewModel>()
            //    .AddTransient<MainWindow>()
            //    .AddTransient<MainWindowViewModel>();
            //builder.Services.AddHostedService<ApplicationHostService>();

            m_Host = builder.Build();
            await m_Host.StartAsync();
            //host.StartAsync();
            //ActivatorUtilities.CreateInstance<BlobViewModel>(host.Services, "aaa");
        }

        void ConfigService(IServiceCollection services)
        {
            services.AddHostedService<ApplicationHostService>();
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<MainWindow>();
            services.AddTransient<BlobPage>();
            services.AddTransient<BlobViewModel>();
        }

        private async void Application_Exit(object sender, ExitEventArgs e)
        {
            await m_Host?.StopAsync();
        }
    }

    public class ApplicationHostService : IHostedService
    {
        IServiceProvider m_ServiceProvider;
        public ApplicationHostService(IServiceProvider sp)
        {
            this.m_ServiceProvider = sp;
        }

        Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            if(App.Current.Windows.Count == 0)
            {
                var window=this.m_ServiceProvider.GetService<MainWindow>();
                window?.Show();
            }
            return Task.CompletedTask;
        }

        Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

}
