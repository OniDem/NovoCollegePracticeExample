using NovoCollegePracticeExample.Entity;
using Npgsql;
using System.Data;
using System.Windows;

namespace NovoCollegePracticeExample.Service
{
    public static class SessionService
    {
        static readonly NpgsqlConnection? con = new(ApplicationContext.ConnectionString);
        static NpgsqlCommand? command;

        public static async Task SetSessionUser(string UserName)
        {
            try
            {
                con!.Open();
                command = new($"INSERT INTO sessions (sessionusername) VALUES ('{UserName}')", con);
                await command.ExecuteNonQueryAsync();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        public static async Task<string?> GetSessionUser()
        {
            try
            {
                var sessions = await ShowAll();
                if (sessions.Count == 0)
                    return null;
                else
                    return sessions.First().SessionUserName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
                return null;
            }
        }

        private static async Task<List<SessionEntity>> ShowAll()
        {
            List<SessionEntity> serssions = new();
            try
            {
                con!.Open();
                command = new("SELECT * FROM sessions", con);
                var reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    serssions.Add(new()
                    {
                        SessionUserName = (string)reader["sessionusername"]                       
                    });
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
            return serssions;
        }

        public static async Task ClearSessionUser()
        {
            try
            {
                con!.Open();
                command = new($"DELETE FROM sessions", con);
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
