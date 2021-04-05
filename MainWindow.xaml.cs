using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using YoutubeMusic.ViewModel;

namespace YoutubeMusic
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ((MainWindowViewModel) DataContext).ParentWindow = this;

        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            (DataContext as MainWindowViewModel)?.MainWindow_OnClosing(sender, e);
        }

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            (DataContext as MainWindowViewModel)?.MainWindow_OnClosed(sender, e);
        }
    }
}
