using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GitViwer.Contracts;
using Microsoft.Extensions.DependencyInjection;
using QSoft.Git.Object;
using System.Collections.ObjectModel;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IShellWindow
    {
        MainWindowViewModel m_ViewModel;
        public MainWindow(MainWindowViewModel viewmodel)
        {
            InitializeComponent();
            this.DataContext = m_ViewModel = viewmodel;
        }

        public void CloseWindow()
        {
            throw new NotImplementedException();
        }

        public Frame GetNavigationFrame() => this.frame;

        public void ShowWindow()
        {
            throw new NotImplementedException();
        }

        private async void listview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listview = sender as ListView;
            var obj = listview?.SelectedItem as GitObject;
            if (obj != null)
            {
                if (obj.Type == "blob")
                {
                    var page = new BlobPage(new BlobViewModel());
                    this.frame.Navigate(page, obj);
                    var aware = page.DataContext as INavigationAware;
                    aware?.OnNavigatedTo(obj);
                }
            }
        }
    }

    public interface IShellWindow
    {
        Frame GetNavigationFrame();

        void ShowWindow();

        void CloseWindow();
    }


    public partial class MainWindowViewModel : ObservableObject
    {
        public ObservableCollection<GitObject> GitObjects { get; set; } = new ObservableCollection<GitObject>();
        INavigationService m_NavigationService;
        public MainWindowViewModel(INavigationService navigateservice)
        {
            this.m_NavigationService = navigateservice;
            var objectfolder = @"C:\Users\oven4\source\repos\QSoft.Git2\.git\objects";
            var objs = objectfolder.EnumbleObject();
            foreach (var oo in objs)
            {
                this.GitObjects.Add(new GitObject()
                {
                    Data = oo,
                    Type = oo.type,
                    Length = oo.size,
                    FileName = System.IO.Path.GetFileName(oo.filename)
                });
            }

        }
        [ObservableProperty]
        GitObject gitobj;
        [RelayCommand]
        public void SelectGitObject(SelectionChangedEventArgs? parameter)
        {
            var dd = this.Gitobj.Data.ReadBlob();
            if (this.Gitobj.Type == "blob")
            {
                this.m_NavigationService.NavigateTo<BlobPage>(this.Gitobj);
                //var page = new BlobPage(new BlobViewModel());
                ////this.frame.Navigate(page);
                //m_NavigationService.NavigateTo("", this.gitobj);
                //var aware = page.DataContext as INavigationAware;
                //aware?.OnNavigatedTo(this.Gitobj);
            }
        }
    }

    public class GitObject
    {
        public (string type, long offset, int size, string filename) Data { set; get; }
        public string Type { set; get; }
        public string FileName { set; get; }
        public int Length { set; get; }
    }
}