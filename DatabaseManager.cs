using System;
using System.Data.SQLite;
using System.IO;

namespace PasswordGridTemplate
{
    internal class DatabaseManager 
    {

        SQLiteConnection dbConnection;
        SQLiteCommand command;
        private string sqlCommand;
        string dbPath = Environment.CurrentDirectory + "\\DB";
        string dbFilePath;

        public void CreateDbFile()
        {
            if (!string.IsNullOrEmpty(dbPath) && !Directory.Exists(dbPath))
            {
                Directory.CreateDirectory(dbPath);
            }

            dbFilePath = dbPath + "\\Template.db";

            if (!File.Exists(dbFilePath))
            {
                SQLiteConnection.CreateFile(dbFilePath);
            }
        }

        public string CreateDbConnection()
        {
            dbFilePath = dbPath + "\\Template.db";
            string strCon = string.Format("Data Source={0}", dbFilePath);
            dbConnection = new SQLiteConnection(strCon);
            dbConnection.Open();
            command = dbConnection.CreateCommand();
            return strCon;
        }

        public SQLiteConnection Connection()
        {
            dbFilePath = dbPath + "\\Template.db";
            string strCon = string.Format("Data Source={0}", dbFilePath);
            dbConnection = new SQLiteConnection(strCon);
            dbConnection.Open();
            command = dbConnection.CreateCommand();
            return dbConnection;
        }

        public void CloseDbConnection()
        {
            dbConnection.Close();
        }

        public void CreateTables()
        {
            
            if (!CheckIfExist("Passwords"))
            {
                sqlCommand = "CREATE TABLE Passwords("
                             + "id INTEGER PRIMARY KEY AUTOINCREMENT,"
                             + "toDelete INTEGER,"
                             + "destination NVARCHAR(100) NOT NULL,"
                             + "identifiant NVARCHAR(100) NOT NULL,"
                             + "pass NVARCHAR(100) NOT NULL"
                             + ");";
                ExecuteQuery(sqlCommand);
            }
        }

        public bool CheckIfExist(string tableName)
        {
            
            command.CommandText = "SELECT name FROM sqlite_master WHERE name='" + tableName + "'";
            object result = command.ExecuteScalar();

            return result != null && result.ToString() == tableName;
        }

        public void ExecuteQuery(string sqlCommand)
        {
            SQLiteCommand triggerCommand = dbConnection.CreateCommand();
            triggerCommand.CommandText = sqlCommand;
            triggerCommand.ExecuteNonQuery();
        }

        public bool CheckIfTableContainsData(string tableName)
        {
            CreateDbConnection();
            command.CommandText = "SELECT count(*) FROM " + tableName;
            var result = command.ExecuteScalar();
            CloseDbConnection();

            return Convert.ToInt32(result) > 0;
        }

        public int ExecuteQueryWithIntReturn(string query)
        {
            SQLiteCommand triggerCommand = dbConnection.CreateCommand();
            triggerCommand.CommandText = query;
            var result = triggerCommand.ExecuteScalar();

            return Convert.ToInt32(result);
        }

        public SQLiteDataReader ExecuteQueryWithReturn(string select)
        {
            SQLiteCommand triggerCommand = dbConnection.CreateCommand();
            triggerCommand.CommandText = select;
            SQLiteDataReader reader = triggerCommand.ExecuteReader();
            return reader;
        }

    }
}
