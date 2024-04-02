using System.Windows;

namespace NovoCollegePracticeExample
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string session_user;
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

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            session_user = null;
            var result = MessageBox.Show("Вы уверены?", "Выход из учётной записи", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                AuthWindow authWindow = new();
                authWindow.Show();
                Close();
            }
            
        }
    }
}
