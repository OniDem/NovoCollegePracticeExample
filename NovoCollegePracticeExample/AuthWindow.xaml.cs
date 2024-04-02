using NovoCollegePracticeExample.Service;
using System.Windows;

namespace NovoCollegePracticeExample
{
    /// <summary>
    /// Interaction logic for AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
            Dispatcher.Invoke(async () =>
            {
                if (await SessionService.GetSessionUser() != null)
                {
                    MainWindow mainWindow = new();
                    mainWindow.Show();
                    Close();
                }
            });
        }

        private async void AuthButton_Click(object sender, RoutedEventArgs e)
        {

            if (UserNameTextBox.Text.Length > 0 && PassTextBox.Text.Length > 0)
            {
                if (await UserService.AuthUser(new() { UserName = UserNameTextBox.Text, Password = PassTextBox.Text }))
                {
                    MainWindow mainWindow = new();
                    mainWindow.Show();
                    Close();
                }
                else
                    MessageBox.Show("Данной учётной записи не существует");
            }
            else
            {
                MessageBox.Show("При вводе логина и пароля допущена ошибка!");
            }
        }

        private void NavToRegButton_Click(object sender, RoutedEventArgs e)
        {
            RegWindow regWindow = new();
            regWindow.Show();
            Close();
        }
    }
}