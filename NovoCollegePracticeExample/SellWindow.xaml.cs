using NovoCollegePracticeExample.Service;
using System.Windows;

namespace NovoCollegePracticeExample
{
    /// <summary>
    /// Логика взаимодействия для SellWindow.xaml
    /// </summary>
    public partial class SellWindow : Window
    {
        public SellWindow()
        {
            InitializeComponent();
            Dispatcher.Invoke(async () =>
            {
                SellDataGrid.ItemsSource = await SellService.ShowAll();
            });
        }

        private async void SearchBarcodeButton_Click(object sender, RoutedEventArgs e)
        {
            if (SearchBarcodeTextBox.Text.Length > 0)
            {
                try
                {
                    var result = await ProductService.FindByBarCode(SearchBarcodeTextBox.Text);
                    SellNameTextBox.Text = result.Name;
                    SellPriceTextBox.Text = result.Price.ToString();
                    if (SellNameTextBox.Text.Length < 1 && SellPriceTextBox.Text.Length < 1)
                        MessageBox.Show("Товара с данным штрихкодом не существует");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show("Введите штрихкод");
        }

        private async void SellButton_Click(object sender, RoutedEventArgs e)
        {
            if(SellCountTextBox.Text.Length > 0)
            {
                try
                {
                    await SellService.Sell(new()
                    {
                        Barcode = SearchBarcodeTextBox.Text,
                        Name = SellNameTextBox.Text,
                        Price = Convert.ToInt32(SellPriceTextBox.Text),
                        Count = Convert.ToInt32(SellCountTextBox.Text),
                        Sum = Convert.ToInt32(SellPriceTextBox.Text) * Convert.ToInt32(SellCountTextBox.Text),
                        Seller = await SessionService.GetSessionUser()
                    });
                    SellDataGrid.ItemsSource = await SellService.ShowAll();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();
            Close();
        }
    }
}
