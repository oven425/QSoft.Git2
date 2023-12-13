﻿using CommunityToolkit.Mvvm.DependencyInjection;
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
        public App()
        {
            var services = new ServiceCollection();
            services.AddSingleton(new GitObject());
            services.AddTransient<BlobPage>();
            services.AddTransient<BlobViewModel>();

            var host = Host.CreateApplicationBuilder();
            host.Services.AddTransient<BlobPage>()
                .AddTransient<BlobViewModel>();

            var builder = host.Build();
            





        }
    }

}
