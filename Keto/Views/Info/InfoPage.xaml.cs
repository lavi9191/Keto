using System.Windows;
using Keto.Models;
using Keto.Data;
using Keto.Views.Main;

namespace Keto.Views.Info
{
    public partial class InfoPage : Window
    {
        public InfoPage()
        {
            InitializeComponent();
        }

        private void GoToMainPage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

