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

    public class BlobViewModel
    {
        public BlobViewModel(string type) 
        {
        }
    }
}
