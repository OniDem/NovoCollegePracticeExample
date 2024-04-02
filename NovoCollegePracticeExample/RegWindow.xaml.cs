using NovoCollegePracticeExample.Service;
using System.Windows;

namespace NovoCollegePracticeExample
{
    /// <summary>
    /// Логика взаимодействия для RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {

        public RegWindow()
        {
            InitializeComponent();

        }

        private async void RegButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserNameTextBox.Text.Length > 0 && PassTextBox.Text.Length > 0)
            {
                await UserService.RegUser(new() { UserName = UserNameTextBox.Text, Password = PassTextBox.Text, CreatedDate = DateTime.Now });
                await SessionService.SetSessionUser(UserNameTextBox.Text);

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("При вводе логина и пароля допущена ошибка!");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new();
            authWindow.Show();
            Close();
        }
    }
}
