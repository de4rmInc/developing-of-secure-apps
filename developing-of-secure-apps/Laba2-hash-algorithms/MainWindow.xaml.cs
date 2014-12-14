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
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(CryptographyViewModel viewModel)
        {
            InitializeComponent();

            this.DataContext = viewModel;
        }

        private void MainWindowName_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            switch (this.WindowState)
            {
                case System.Windows.WindowState.Maximized:
                    this.WindowState = System.Windows.WindowState.Normal;
                    break;
                case System.Windows.WindowState.Normal:
                    this.WindowState = System.Windows.WindowState.Maximized;
                    break;
                default:
                    break;
            }
        }

        private void MainWindowName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();

        }
    }
}
