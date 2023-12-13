using CommunityToolkit.Mvvm.ComponentModel;
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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ObjectsViewModel vm = new ObjectsViewModel();
            this.DataContext = vm;
        }

        private void listview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listview = sender as ListView;
            var obj = listview?.SelectedItem as GitObject;
            
        }
    }


    public partial class ObjectsViewModel:ObservableObject
    {
        public ObservableCollection<GitObject> GitObjects { get; set; } = new ObservableCollection<GitObject>();
        public ObjectsViewModel()
        {
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
    }

    public class GitObject
    {
        public (string type, long offset, int size, string filename) Data { set; get; }
        public string Type { set; get; }
        public string FileName { set; get; }
        public int Length { set; get; }
    }
}