using NovoCollegePracticeExample.Entity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Windows;
using Npgsql;
using System.Data;

namespace NovoCollegePracticeExample.Service
{
    public static class UserService
    {
        static readonly NpgsqlConnection? con = new(ApplicationContext.ConnectionString);
        static NpgsqlCommand? command;

        public static async Task RegUser(UserEntity entity)
        {
            try
            {
                con!.Open();
                command = new($"INSERT INTO users (username, password, createddate) VALUES ('{entity.UserName}', '{entity.Password}', '{entity.CreatedDate}')", con);
                await command.ExecuteNonQueryAsync();
                con.Close();
                await SessionService.SetSessionUser(entity.UserName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        public static async Task<bool> AuthUser(UserEntity entity)
        {
            try
            {
                List<UserEntity> users = new();
                con!.Open();
                command = new("SELECT * FROM users", con);
                var reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    users.Add(new()
                    {
                        UserName = (string)reader["username"],
                        Password = (string)reader["password"],
                        CreatedDate = (DateTime)reader["createddate"]
                    });
                }
                con.Close();
                foreach (var user in users)
                {
                    if(user.UserName == entity.UserName)
                        if(user.Password == entity.Password)
                        {
                            await SessionService.SetSessionUser(user.UserName);
                            
                            return true;
                        }
                   
                }
                return false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
                return false;
            }
        }

        public static async Task LogOut()
        {
           SessionService.ClearSessionUser();
        }
    }
}
