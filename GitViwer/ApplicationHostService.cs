using GitViwer.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitViwer
{
    public class ApplicationHostService : IHostedService
    {
        IServiceProvider m_ServiceProvider;
        INavigationService m_NavigationService;
        public ApplicationHostService(IServiceProvider sp, INavigationService navigationService)
        {
            this.m_ServiceProvider = sp;
            this.m_NavigationService = navigationService;
        }

        Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            if (App.Current.Windows.Count == 0)
            {
                var window = this.m_ServiceProvider.GetService<MainWindow>();
                var ishellwin = window as IShellWindow;
                this.m_NavigationService.Initialize(ishellwin.GetNavigationFrame());
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
