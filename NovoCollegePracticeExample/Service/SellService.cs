using NovoCollegePracticeExample.Entity;
using Npgsql;
using System.Data;
using System.Windows;

namespace NovoCollegePracticeExample.Service
{
    public static class SellService
    {
        static readonly NpgsqlConnection? con = new(ApplicationContext.ConnectionString);
        static NpgsqlCommand? command;

        public static async Task Sell(SellEntity entity)
        {
            try
            {
                con!.Open();
                command = new($"INSERT INTO sells (barcode, name, price, count, sum, seller) VALUES ('{entity.Barcode}', '{entity.Name}', '{entity.Price}', '{entity.Count}', '{entity.Sum}', '{entity.Seller}')", con);
                await command.ExecuteNonQueryAsync();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        public static async Task<List<SellEntity>> ShowAll()
        {
            List<SellEntity> sells = new();
            try
            {
                con!.Open();
                command = new("SELECT * FROM sells", con);
                var reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    sells.Add(new()
                    {
                        Barcode = (string)reader["barcode"],
                        Name = (string)reader["name"],
                        Price = (int)reader["price"],
                        Count = (int)reader["count"],
                        Sum = (int)reader["sum"],
                        Seller = (string)reader["seller"]
                    });
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
            return sells;
        }
    }
}
