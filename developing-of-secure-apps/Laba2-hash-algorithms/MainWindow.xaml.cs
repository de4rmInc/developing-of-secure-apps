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
using Microsoft.Win32;
using Laba2_hash_algorithms.Cryptography.HashAlgorithms;
using Laba2_hash_algorithms.ViewModels;
using Laba2_hash_algorithms.Models;

namespace Laba2_hash_algorithms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly OpenFileDialog _fileDialog;
        private CryptographyViewModel _viewmodel;

        public MainWindow()
        {
            InitializeComponent();

            _fileDialog = new OpenFileDialog();
            _fileDialog.FileOk += fileDialog_FileOk;
            _viewmodel = new CryptographyViewModel();
            this.DataContext = _viewmodel;
        }

        private void OpenFileMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _fileDialog.ShowDialog();
        }

        private void fileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var fileDialog = sender as OpenFileDialog;
            if (fileDialog == null || e.Cancel)
            {
                return;
            }

            var model = new CryptographyModel(fileDialog.FileName);
            model.CalculateFileHashCode();
            model.Encrypt();

            _viewmodel.SetModel(model);

        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SwitchView_Click(object sender, RoutedEventArgs e)
        {
            _viewmodel.IsViewModeBytes = !_viewmodel.IsViewModeBytes;
        }
    }
}
