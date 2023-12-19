using GitViwer.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GitViwer
{
    public class NavigationService : INavigationService
    {
        IServiceProvider m_ServiceProvider;
        public NavigationService(IServiceProvider sp)
        {
            m_ServiceProvider = sp;
        }
        public bool CanGoBack => throw new NotImplementedException();

        public event EventHandler<string> Navigated;

        public void CleanNavigation()
        {
            throw new NotImplementedException();
        }

        public void GoBack()
        {
            throw new NotImplementedException();
        }

        Frame m_Frame;
        public void Initialize(Frame shellFrame)
        {
            this.m_Frame = shellFrame;
            this.m_Frame.Navigated += M_Frame_Navigated;
        }

        private void M_Frame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            var frame = sender as Frame;
            var dataContext = GetDataContext(frame);
            if (dataContext is INavigationAware navigationAware)
            {
                navigationAware.OnNavigatedTo(e.ExtraData);
            }

        }

        public bool NavigateTo(string pageKey, object parameter = null, bool clearNavigation = false)
        {
            //this.m_ServiceProvider.GetKeyedService
            throw new NotImplementedException();
        }

        public void UnsubscribeNavigation()
        {
            throw new NotImplementedException();
        }

        public object GetDataContext(Frame frame)
        {
            if (frame.Content is FrameworkElement element)
            {
                return element.DataContext;
            }

            return null;
        }

        public bool NavigateTo<T>(object parameter = null)
        {
            var pp = this.m_ServiceProvider.GetService<T>();
            var bb = this.m_Frame.Navigate(pp, parameter);

            //if (this.m_Frame.Content is FrameworkElement element)
            //{
            //    return element.DataContext;
            ////}
            //var dataContext = GetDataContext(this.m_Frame);
            //if (dataContext is INavigationAware navigationAware)
            //{
            //    navigationAware.OnNavigatedFrom();
            //}
            return true;
        }
    }
}
