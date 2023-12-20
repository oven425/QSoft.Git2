using CommunityToolkit.Mvvm.ComponentModel;
using GitViwer.Contracts;
using QSoft.Git.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GitViwer
{
    /// <summary>
    /// CommitPage.xaml 的互動邏輯
    /// </summary>
    public partial class CommitPage : Page
    {

        public CommitPage(CommitViewModel viewmodel)
        {
            InitializeComponent();
            this.DataContext = viewmodel;
        }
    }

    public partial class CommitViewModel : ObservableObject, INavigationAware
    {
        [ObservableProperty]
        private string data;
        public CommitViewModel()
        {

        }

        public void OnNavigatedFrom()
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(object parameter)
        {
            var gitobj = parameter as GitObject;
            var aa = gitobj.Data.ReadCommit();
        }
    }
}
