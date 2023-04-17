using PasswordGridTemplate.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace PasswordGridTemplate.Tables
{
    public class PasswordsTable
    {
        public async void InsertPassword(Password password)
        {
            int toDeleteInt = 0;
            if (password.ToDelete)
            {
                toDeleteInt = 1;
            }
            DatabaseManager databaseManager = new DatabaseManager();
            databaseManager.CreateDbConnection();
            databaseManager.ExecuteQuery("INSERT INTO `Passwords`(`toDelete`, `destination`, `identifiant`, `pass`) VALUES ('" + toDeleteInt + "','" + password.Destination + "','" + password.Identifiant + "','" + password.Pass + "');");
            databaseManager.CloseDbConnection();
        }

        public async Task<List<Password>> GetAllPasswords()
        {
            DatabaseManager databaseManager = new DatabaseManager();
            databaseManager.CreateDbConnection();
            SQLiteDataReader reader = databaseManager.ExecuteQueryWithReturn("SELECT * FROM Passwords;");
            List <Password> passList = new();
            while (reader.Read())
            {
                int _Id = -1;
                bool _ToDelete = false;
                int _ToDeleteInt = 0;
                string _Destination = "";
                string _Identifiant = "";
                string _Pass = "";

                _Id = reader.GetInt32("id");
                _Destination = reader.GetString("destination");
                _Identifiant = reader.GetString("identifiant");
                _Pass = new string('•', reader.GetString("pass").Length);
                _ToDeleteInt = reader.GetInt32("toDelete");
                if (_ToDeleteInt == 1)
                {
                    _ToDelete = true;
                }

                Password password = new(_Id, _ToDelete, _Destination, _Identifiant, _Pass);
                passList.Add(password);
            }
            reader.Close();
            databaseManager.CloseDbConnection();
            return passList;
        }

        public string GetPasswordById(int id)
        {
            string pass = "";
            DatabaseManager databaseManager = new DatabaseManager();
            databaseManager.CreateDbConnection();
            SQLiteDataReader reader = databaseManager.ExecuteQueryWithReturn("SELECT pass FROM Passwords WHERE id = " + id + ";");
            while (reader.Read())
            {
                pass = reader.GetString("pass");
            }
                return pass;
        }

        public void UpdateToDelete(bool toDelete, int id)
        {
            int toDeleteInt = 0;
                if(toDelete)
            {
                toDeleteInt = 1;
            }
            DatabaseManager databaseManager = new DatabaseManager();
            databaseManager.CreateDbConnection();
            databaseManager.ExecuteQuery("Update Passwords SET toDelete = '" + toDeleteInt + "' WHERE id = " + id + ";");
            databaseManager.CloseDbConnection();
        }

        public void DeleteSelected()
        {
            DatabaseManager databaseManager = new DatabaseManager();
            databaseManager.CreateDbConnection();
            databaseManager.ExecuteQuery("DELETE FROM `Passwords` WHERE toDelete = 1;");
            databaseManager.CloseDbConnection();
        }

        public void DeleteAll()
        {
            DatabaseManager databaseManager = new DatabaseManager();
            databaseManager.CreateDbConnection();
            databaseManager.ExecuteQuery("DELETE FROM `Passwords`;");
            databaseManager.CloseDbConnection();
        }
    }
}
