using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ATM.Models;
using ATM.Repositories;

namespace ATM.Services
{
    public class SQLite
    {
        public SQLiteConnection Conn { get; set; }
        public SQLite()
        {
            Conn = CreateConnection();
            CreateTable(Conn);            
        }
        public SQLiteConnection CreateConnection()
        {
            
            Conn = new SQLiteConnection("Data Source = database.sqlite; version = 3; New = true; Compress = true;");
            try {Conn.Open();}
            catch { };
            return Conn;
        }
        public void CreateTable(SQLiteConnection conn)
        {
            SQLiteCommand command;
            command = conn.CreateCommand();
            command.CommandText = "CREATE TABLE IF NOT EXISTS BankData("+
                "Guid BLOB PRIMARY KEY," +
                "Name TEXT(20)," +
                "Last_Name TEXT(20)," +
                "Account_Number TEXT(20)," +
                "Balance REAL," +
                "Card_number TEXT(20)," +
                "Validity_Date BLOB," +
                "Blocked BLOB);";
            command.ExecuteNonQuery();
        }
        public bool CheckIfTableEmpty(SQLiteConnection conn, string table_name)
        {
            SQLiteCommand command = conn.CreateCommand();
            command.CommandText = $"SELECT COUNT(*) FROM {table_name}";
            bool empty = true;
            var size = command.ExecuteNonQuery() ;
            if (size > 0) empty = false;
            return empty;
        }
        public void DeleteData(SQLiteConnection conn, string table_name)
        {
            SQLiteCommand command = conn.CreateCommand();
            command.CommandText = $"DELETE FROM {table_name}";
            command.ExecuteNonQuery();
        }
        public void InsertData(SQLiteConnection conn, SQLentry e)
        {
            SQLiteCommand command = conn.CreateCommand();
            command.CommandText = $"INSERT INTO BankData(Guid, Name, Last_name, Account_number, Balance, Card_number, Validity_date, Blocked) " +
                $"VALUES ('{e.ClientID}','{e.Name}','{e.LastName}','{e.AccountNumber}','{e.Balance}','{e.CardNumber}','{e.ValidityDate}','{e.Blocked}');";
            command.ExecuteNonQuery();
        }
        public void ReadData(SQLiteConnection conn)
        {
            SQLiteDataReader reader;
            SQLiteCommand command = conn.CreateCommand();
            command.CommandText = "SELECT Guid, Name, Last_name, Account_number, Balance, Card_number, Validity_date, Blocked FROM BankData";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                string readerString = reader.GetString(0);
                Console.WriteLine(readerString);
            }

        }
    }
}
