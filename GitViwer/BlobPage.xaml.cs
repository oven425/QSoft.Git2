using CommunityToolkit.Mvvm.ComponentModel;
using QSoft.Git.Object;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// BlobPage.xaml 的互動邏輯
    /// </summary>
    public partial class BlobPage : Page
    {
        BlobViewModel m_ViewModel;
        public BlobPage(BlobViewModel viewmodel)
        {
            InitializeComponent();
            this.DataContext = this.m_ViewModel = viewmodel;
        }

    }

    public partial class BlobViewModel: ObservableObject, INavigationAware
    {
        [ObservableProperty]
        private string data;
        public BlobViewModel() 
        {
           
        }

        public void OnNavigatedFrom()
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(object parameter)
        {
            var gitobj = parameter as GitObject;
            this.Data = gitobj.Data.ReadBlob();
            //using (var file = File.OpenRead(gitobj.Data.filename))
            //using(var br = new BinaryReader(file))
            //{
            //    file.Position = gitobj.Data.offset;
            //    this.Data = br.ReadString();
            //}

                //this.Data = File.ReadAllText(gitobj?.Data.filename);
            //throw new NotImplementedException();
        }
    }

    public interface INavigationAware
    {
        void OnNavigatedTo(object parameter);

        void OnNavigatedFrom();
    }
}
