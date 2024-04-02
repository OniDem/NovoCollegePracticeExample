using NovoCollegePracticeExample.Service;
using System.Windows;

namespace NovoCollegePracticeExample
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SellButton_Click(object sender, RoutedEventArgs e)
        {
            SellWindow sellWindow = new();
            sellWindow.Show();
            Close();
        }

        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow productWindow = new();
            productWindow.Show();
            Close();
        }

        private async void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены?", "Выход из учётной записи", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                await UserService.LogOut();
                AuthWindow authWindow = new();
                authWindow.Show();
                Close();
            }
            
        }
    }
}
