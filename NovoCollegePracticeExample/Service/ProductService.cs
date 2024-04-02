using NovoCollegePracticeExample.Entity;
using Npgsql;
using System.Data;
using System.Windows;

namespace NovoCollegePracticeExample.Service
{
    public static class ProductService
    {
        static readonly NpgsqlConnection? con = new(ApplicationContext.ConnectionString);
        static NpgsqlCommand? command;

        public static async Task AddProduct(ProductEntity entity)
        {
            try
            {
                con!.Open();
                command = new($"INSERT INTO products (barcode, name, price, addeddate) VALUES ('{entity.Barcode}', '{entity.Name}', '{entity.Price}', '{entity.AddedDate}')", con);
                await command.ExecuteNonQueryAsync();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        public static async Task<ProductEntity?> FindByBarCode(string Barcode)
        {
            try
            {
                var products = await ShowAll();
                foreach (var product in products)
                {
                    if (product.Barcode == Barcode)
                        return product;
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
                return null;
            }
        }

        public static async Task<List<ProductEntity>> ShowAll()
        {
            List<ProductEntity> products = new();
            try
            {
                con!.Open();
                command = new("SELECT * FROM products", con);
                var reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    products.Add(new()
                    {
                        Barcode = (string)reader["barcode"],
                        Name = (string)reader["name"],
                        Price = (int)reader["price"],
                        AddedDate = (DateTime)reader["addeddate"]
                    });
                }
                con.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
            return products;
        }

        public static async Task Delete(string Barcode)
        {
            try
            {
                con!.Open();
                command = new($"DELETE FROM products WHERE Barcode = '{Barcode}'", con);
                await command.ExecuteNonQueryAsync();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

    }
}
