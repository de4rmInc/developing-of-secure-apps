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
using Microsoft.Win32;

namespace HashAlgorithms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly OpenFileDialog _fileDialog;

        public MainWindow()
        {
            InitializeComponent();

            _fileDialog = new OpenFileDialog();
            _fileDialog.FileOk += fileDialog_FileOk;
        }

        private void OpenFileMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _fileDialog.ShowDialog();
        }

        void fileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBox.Show("opened");
            MessageBox.Show((sender as OpenFileDialog).FileName);
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
