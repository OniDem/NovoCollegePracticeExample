using NovoCollegePracticeExample.Service;
using System.Windows;

namespace NovoCollegePracticeExample
{
    /// <summary>
    /// Логика взаимодействия для ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        public ProductWindow()
        {
            InitializeComponent();
            Dispatcher.Invoke(async () =>
            {
                ProductDataGrid.ItemsSource = await ProductService.ShowAll();
            });
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();
            Close();
        }

        private async void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AddBarcodeTextBox.Text.Length > 0)
                {
                    if (AddNameTextBox.Text.Length > 0)
                    {
                        if (AddPriceTextBox.Text.Length > 0)
                        {
                            await ProductService.AddProduct(new()
                            {
                                Barcode = AddBarcodeTextBox.Text,
                                Name = AddNameTextBox.Text,
                                Price = Convert.ToInt32(AddPriceTextBox.Text),
                                AddedDate = DateTime.Now
                            });
                            ProductDataGrid.ItemsSource = await ProductService.ShowAll();
                        }
                        else
                            MessageBox.Show("Введите цену");
                    }
                    else
                        MessageBox.Show("Введите наименование");
                }
                else
                    MessageBox.Show("Введите штрихкод");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DeleteBarcodeTextBox.Text.Length > 0)
                {
                    await ProductService.Delete(DeleteBarcodeTextBox.Text);
                    ProductDataGrid.ItemsSource = await ProductService.ShowAll();
                }
                else
                    MessageBox.Show("Введите штрихкод");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
